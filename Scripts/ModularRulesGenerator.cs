using System;
using UnityEditor;
using UnityEngine;

namespace ModularBuildings
{
	public class ModularRulesGenerator : MonoBehaviour
	{
		private AnimationCurve _heightCurve;
		private int _facadeLength;
		private int _facadeHeight;
		private int _facadeDepth;

		private string[,,] _floors;
		private ModularFootprintGenerator _footprintGenerator;

		public string[,,] GetBlockSymbols(int length, int height, int depth, AnimationCurve heightCurve, ModularFootprintGenerator footprintGenerator)
		{
			if (footprintGenerator.FootprintArray == null)
			{
				throw new NotImplementedException("Footprint Generator is not Implement.. Create a footprint from ModularFootprintGenerator Object in Scene");
			}

			_facadeLength = length;
			_facadeHeight = height;
			_facadeDepth = depth;
			_heightCurve = heightCurve;
			_floors = new string[_facadeLength, _facadeHeight, _facadeDepth];
			_footprintGenerator = footprintGenerator;
			// Put window every where
			for (var j = 0; j < _facadeHeight; j++)
			{
				for (var i = 0; i < _facadeLength; i++)
				{
					for (var k = 0; k < _facadeDepth; k++)
					{
						_floors[i, j, k] = BlockSymbol.Window;
					}
				}
			}

			// Fit in the curve and create the gap
			for (var i = 0; i < _facadeLength; i++)
			{
				for (var k = 0; k < _facadeDepth; k++)
				{
					for (var j = 0; j < _facadeHeight; j++)
					{
						var widthFactor = i * 1.0f / _facadeLength;
						var curveValue = _heightCurve.Evaluate(widthFactor);
						curveValue = Mathf.Clamp(curveValue, 0, 1);
						var verticalIndexBasedOnFactor = _facadeHeight * curveValue;
						if (j > verticalIndexBasedOnFactor)
						{
							_floors[i, j, k] = BlockSymbol.Gap;
						}
					}
				}
			}


			// Fit in the footprint
			for (var k = 0; k < _facadeDepth; k++)
			{
				for (var i = 0; i < _facadeLength; i++)
				{
					if (!_footprintGenerator.FootprintArray[i, k])
					{
						for (var j = 0; j < _facadeHeight; j++)
						{
							_floors[i, j, k] = BlockSymbol.Gap;
						}
					}
				}
			}

			var blockSurrounding = new BlockSurrounding(_floors, _facadeLength, _facadeHeight, _facadeDepth);
			// Assign Corner and depths
			for (var k = 0; k < _facadeDepth; k++)
			{
				for (var j = 0; j < _facadeHeight; j++)
				{
					for (var i = 0; i < _facadeLength; i++)
					{
						if (_floors[i, j, k] != BlockSymbol.Gap)
						{
							blockSurrounding.ApplyCondition(i, j, k);
							// If there is gap in every side, its a Full Faced Block
							if (blockSurrounding.HasGapInLeft && blockSurrounding.HasGapInRight && blockSurrounding.HasGapInfront && blockSurrounding.HasGapInBack)
							{
								_floors[i, j, k] = BlockSymbol.FullFacedBlock;
							}
							// If there is gap in both LEFT and RIGHT... its a double Corner (U) or double depth (II)
							else if (blockSurrounding.HasGapInLeft && blockSurrounding.HasGapInRight) // Gap in both left and right
							{
								if (blockSurrounding.HasGapInBack)
									_floors[i, j, k] = BlockSymbol.DepthCornerDoubleLeft;
								else if (blockSurrounding.HasGapInfront) // Need to rotate Vertically
									_floors[i, j, k] = BlockSymbol.DepthCornerDoubleLeft.VerticalMirror();
								else
									_floors[i, j, k] = BlockSymbol.DepthDouble;
							}
							// there is gap in both FRONT and BACK, its a double Straight (C) or a double window (=)
							else if (blockSurrounding.HasGapInfront && blockSurrounding.HasGapInBack)
							{
								if (blockSurrounding.HasGapInLeft)
									_floors[i, j, k] = BlockSymbol.StraightCornerDouble;
								else if (blockSurrounding.HasGapInRight)
									_floors[i, j, k] = BlockSymbol.StraightCornerDouble.HorizontalMirror();
								else
									_floors[i, j, k] = BlockSymbol.WindowDouble;
							}

							// Detect the corners
							else if (blockSurrounding.HasGapInLeft && blockSurrounding.HasGapInBack)
							{
								_floors[i, j, k] = BlockSymbol.CornerLeft;
							}
							else if (blockSurrounding.HasGapInLeft && blockSurrounding.HasGapInfront)
							{
								_floors[i, j, k] = BlockSymbol.CornerLeft.VerticalMirror();
							}
							else if (blockSurrounding.HasGapInRight && blockSurrounding.HasGapInBack)
							{
								_floors[i, j, k] = BlockSymbol.CornerLeft.HorizontalMirror();
							}
							else if (blockSurrounding.HasGapInRight && blockSurrounding.HasGapInfront)
							{
								_floors[i, j, k] = BlockSymbol.CornerLeft.HorizontalMirror().VerticalMirror();
							}
							else if (blockSurrounding.HasGapInLeft && !blockSurrounding.HasGapInRight && !blockSurrounding.HasGapInfront)
							{
								_floors[i, j, k] = BlockSymbol.DepthLeft;
							}
							else if (blockSurrounding.HasGapInRight && !blockSurrounding.HasGapInLeft && !blockSurrounding.HasGapInfront)
							{
								_floors[i, j, k] = BlockSymbol.DepthLeft.HorizontalMirror();
							}
						}
					}
				}
			}

			// flip windows in the back
			for (var j = 0; j < _facadeHeight; j++)
			{
				for (var i = 0; i < _facadeLength; i++)
				{
					for (var k = 0; k < _facadeDepth; k++)
					{
						// if its a window and there is a gap in the front, it needs to Flip
						if (_floors[i, j, k] == BlockSymbol.Window)
						{
							blockSurrounding.ApplyCondition(i, j, k);
							if (blockSurrounding.HasGapInfront)
							{
								_floors[i, j, k] = BlockSymbol.Window.VerticalMirror();
							}
						}
					}
				}
			}

			// Add roofs
			for (var i = 0; i < _facadeLength; i++)
			{
				for (var k = 0; k < _facadeDepth; k++)
				{
					if (_floors[i, 0, k] != BlockSymbol.Gap)
					{
						// if its not a gap, go up till there is Gap.. mark that block and add top
						for (var j = 0; j < _facadeHeight; j++)
						{
							if (j == _facadeHeight - 1 || _floors[i, j + 1, k] == BlockSymbol.Gap)
							{
								_floors[i, j, k] = _floors[i, j, k].AddTop();
								break;
							}
						}
					}
				}
			}

			// Clear windows
			for (var j = 0; j < _facadeHeight; j++)
			{
				for (var i = 0; i < _facadeLength; i++)
				{
					for (var k = _facadeDepth - 1; k > 0; k--)
					{
						// if its a Window or WindowTop
						if (_floors[i, j, k].Contains(BlockSymbol.Window))
						{
							blockSurrounding.ApplyCondition(i, j, k);
							if (!blockSurrounding.HasGapInBack)
							{
								if (_floors[i, j, k] == BlockSymbol.Window)
									_floors[i, j, k] = BlockSymbol.Gap;
								else if (_floors[i, j, k] == BlockSymbol.Window.AddTop())
									_floors[i, j, k] = BlockSymbol.RoofTop;
							}
						}
					}
				}
			}

			// Add ground
			for (var i = 0; i < _facadeLength; i++)
			{
				for (var k = 0; k < _facadeDepth; k++)
				{
					if (footprintGenerator.FootprintArray[i, k])
						_floors[i, 0, k] = _floors[i, 0, k].MakeGround();
				}
			}

			return _floors;
		}

		#region Debug Area

		// TODO: Get rid of them when done - Risul
		public bool ShowData { get; set; }
		[SerializeField] private bool _showNames = false;
		[SerializeField] private bool _showSpeheres = false;
		[SerializeField] [Range(0, 1)] private float _sphereTransparency;

		private void OnDrawGizmos()
		{
			if (ShowData)
			{
				if (_floors == null)
					ShowData = false;
				for (var k = 0; k < _facadeDepth; k++)
				{
					for (var j = 0; j < _facadeHeight; j++)
					{
						for (var i = 0; i < _facadeLength; i++)
						{
#if UNITY_EDITOR
							var position = new Vector3(-i, j, -k);
							Gizmos.color = BlockSymbol.GetColor(_floors[i, j, k], _sphereTransparency);

							if (_showNames)
							{
								if (BlockSymbol.GetName(_floors[i, j, k]) != nameof(BlockSymbol.Gap))
								{
									Handles.Label(position, BlockSymbol.GetName(_floors[i, j, k]));
								}
							}

							if (_showSpeheres)
								Gizmos.DrawSphere(position, .2f);
#endif
						}
					}
				}
			}
		}

		#endregion
	}
}
