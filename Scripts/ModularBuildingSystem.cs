using System;
using UnityEngine;

namespace ModularBuildings
{
	[Serializable]
	public struct ModularBuildingInput
	{
		[Header("Dimension configuration")]
		public int BuildingLength;

		public int BuildingHeight;
		public int BuildingDepth;

		[Space(5)]
		[Header("Facade Shape")]
		public AnimationCurve HeightCurve;
	}

	/// <summary>
	///     Custom editor <see cref="ModularBuildingSystemEditor" />
	/// </summary>
	public class ModularBuildingSystem : MonoBehaviour
	{
		public ModularBuildingInput modularBuildingInput;

		private int _buildingLength;
		private int _buildingHeight;
		private int _buildingDepth;
		private AnimationCurve _heightCurve;


		[Space(5)]
		[Header("Components")]
		[SerializeField]
		private ModularRulesGenerator _modularRulesGenerator;

		[SerializeField] private ModularFootprintGenerator _modularFootprintGenerator;
		[SerializeField] private BuildingBlockContainer _buildingBlockContainer;

		//[SerializeField] private MB3_MeshBaker _meshbaker;
		private BuildingBlock[,,] _building;
		private GameObject _buildingObject; // TODO: Remove it while finalizing- Risul

		public void GenerateBlocks() //
		{
			_buildingLength = modularBuildingInput.BuildingLength;
			_buildingHeight = modularBuildingInput.BuildingHeight;
			_buildingDepth = modularBuildingInput.BuildingDepth;
			_heightCurve = modularBuildingInput.HeightCurve;

			// Get the blocks
			var blockSymbols = _modularRulesGenerator.GetBlockSymbols(_buildingLength, _buildingHeight, _buildingDepth, _heightCurve, _modularFootprintGenerator);
			_buildingObject = new GameObject($"Building X_{DateTime.UtcNow}");
			_buildingObject.transform.position = Vector3.zero;

			_building = new BuildingBlock[_buildingLength, _buildingHeight, _buildingDepth];
			for (var j = 0; j < _buildingHeight; j++)
			{
				for (var i = 0; i < _buildingLength; i++)
				{
					for (var k = 0; k < _buildingDepth; k++)
					{
						var buildingBlock = _buildingBlockContainer.GetBlock(blockSymbols[i, j, k], _buildingObject.transform);
						buildingBlock.CalculateDimension();
						_building[i, j, k] = buildingBlock;
						_building[i, j, k].BlockName = $"{j}-{i}-{k}";
					}
				}
			}
		}

		public void PlaceBlocks() // TODO: Make it private- Risul
		{
			// Putting roofs
			var roofCreator = new RoofCreator(_buildingBlockContainer, _building, _buildingObject);
			for (var j = 0; j < _buildingHeight; j++)
			{
				for (var i = 0; i < _buildingLength; i++)
				{
					for (var k = 0; k < _buildingDepth; k++)
					{
						var newPosition = Vector3.zero;
						if (i > 0) // Get position & Dimension of the Block on the left side
							newPosition.x = _building[i - 1, j, k].transform.position.x
											- _building[i, j, k].Dimension.Width / 2
											- _building[i - 1, j, k].Dimension.Width / 2;

						if (j > 0) // Get position & Dimension of the block from down
							newPosition.y = _building[i, j - 1, k].transform.position.y
											+ _building[i, j - 1, k].Dimension.Height;
						if (k > 0) // Get position & Dimension from front
						{
							var scale = _building[i, j, k - 1].transform.localScale;
							if (scale.z != -1) // if its not flipped
							{
								newPosition.z = _building[i, j, k - 1].transform.position.z
												- _building[i, j, k - 1].Dimension.Depth;
							}
							else
							{
								newPosition.z = _building[i, j, k - 1].transform.position.z;
							}
						}

						// Place the roof block in the right place on top of Gap blocks
						if (_building[i, j, k].BlockType == BlockSymbol.RoofTop)
						{
							newPosition.y += _building[i, j, k].Dimension.Height;
						}

						// for rest of the block with IsTop true
						else if (_building[i, j, k].IsTop)
						{
							roofCreator.CreateRoof(i, j, k, newPosition);
						}

						if (_building[i, j, k].VerticalMirror)
						{
							newPosition.z -= _building[i, j, k].Dimension.Depth;
							var scale = _building[i, j, k].transform.localScale;
							scale.z = -1;
							_building[i, j, k].transform.localScale = scale;
						}

						if (_building[i, j, k].HorizontalMirror)
						{
							var scale = _building[i, j, k].transform.localScale;
							scale.x = -1;
							_building[i, j, k].transform.localScale = scale;
						}

						_building[i, j, k].transform.position = newPosition;
					}
				}
			}
		}

		public void FixPivot() // TODO: Make it private- Risul
		{
			var totalWidth = 0.0f;
			for (var i = 0; i < _buildingLength; i++)
				totalWidth += _building[i, 0, 0].Dimension.Width;

			var shiftValue = totalWidth / 2 - _building[_buildingLength / 2, 0, 0].Dimension.Width / 2;
			_buildingObject.transform.position = new Vector3(shiftValue, 0, 0);

			// Copy all to new Game object
			var tempParent = new GameObject("Temporary Parent");
			tempParent.transform.position = Vector3.zero;
			var allChildren = _buildingObject.gameObject.GetComponentsInChildren<Transform>();
			foreach (var child in allChildren)
			{
				if (child.transform != _buildingObject.transform)
					child.parent = tempParent.transform;
			}

			// Give it back to _building object
			_buildingObject.transform.position = Vector3.zero;
			allChildren = tempParent.GetComponentsInChildren<Transform>();
			foreach (var child in allChildren)
			{
				if (child.transform != tempParent.transform)
					child.parent = _buildingObject.transform;
			}

			DestroyImmediate(tempParent);
		}

		public void ClearGapAndComponent() // TODO: Make it private- Risul
		{
			if (_building == null)
				return;
			for (var j = 0; j < _buildingHeight; j++)
			{
				for (var i = 0; i < _buildingLength; i++)
				{
					for (var k = 0; k < _buildingDepth; k++)
					{
						if (_building[i, j, k].BlockType == BlockSymbol.Gap)
						{
							_building[i, j, k].DestroyBlock();
						}
						else
						{
							DestroyImmediate(_building[i, j, k]);
						}
					}
				}
			}

			_building = null; // Clear ram
		}

		public void VisualizeBlocks() // TODO: Make it private- Risul
		{
			if (_modularRulesGenerator.ShowData)
			{
				_modularRulesGenerator.ShowData = false;
			}
			else
			{
				_modularRulesGenerator.GetBlockSymbols(_buildingLength, _buildingHeight, _buildingDepth, _heightCurve, _modularFootprintGenerator);
				_modularRulesGenerator.ShowData = true;
			}
		}


		public void AddToMeshBaker()
		{
			Debug.LogWarning($"Add Mesh baker to the project and Uncomment the code");
			/*
			_meshbaker.objsToMesh.Clear();
			foreach (Transform child in _buildingObject.transform)
			{
				if (child != _buildingObject.transform)
				{
					_meshbaker.objsToMesh.Add(child.gameObject);
				}

			}*/
		}
	}
}
