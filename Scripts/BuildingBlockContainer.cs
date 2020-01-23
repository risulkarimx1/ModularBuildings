using System;
using UnityEngine;

namespace ModularBuildings
{
	/// <summary>
	///     Custom Inspector <see cref="BuildingBlockGeneratorEditor" />
	/// </summary>
	public class BuildingBlockContainer : MonoBehaviour
	{
		[SerializeField] private BuildingBlockSetup _bulildingSetup;

		#region Block Creation Switch cases

		public BuildingBlock GetBlock(string blockSymbol, Transform buildingObjectTransform)
		{
			var isVerticallyMirrored = blockSymbol.Contains(BlockSymbol.VerticalMirror);
			var isHorizontallyMirrored = blockSymbol.Contains(BlockSymbol.HorizontalMirror);
			var isTop = blockSymbol.Contains(BlockSymbol.Top);
			if (isVerticallyMirrored || isHorizontallyMirrored || isTop)
			{
				blockSymbol = blockSymbol.Split(',')[0];
			}

			Transform newBlock = null;
			// ************** Ground Floor ****************
			if (blockSymbol == BlockSymbol.GroundWindow)
				newBlock = Instantiate(_bulildingSetup.GroundWindowPrefab[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.GroundCornerLeft)
				newBlock = Instantiate(_bulildingSetup.GroundCornerLeftPrefab[0].transform, buildingObjectTransform);
			// Depths Ground
			else if (blockSymbol == BlockSymbol.GroundDepthLeft)
				newBlock = Instantiate(_bulildingSetup.GroundDepthLeftPrefab[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.GroundGap)
				newBlock = Instantiate(_bulildingSetup.GroundGap[0].transform, buildingObjectTransform);

			// Ground Floor - Double Blocks
			// depth - Double
			else if (blockSymbol == BlockSymbol.GroundDepthCornerDoubleLeft) // Depth Type
				newBlock = Instantiate(_bulildingSetup.GrounddepthCornerLeftDouble[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.GroundDepthDouble)
				newBlock = Instantiate(_bulildingSetup.GrounddepthDouble[0].transform, buildingObjectTransform);
			// straight - Double
			else if (blockSymbol == BlockSymbol.GroundStraightCornerDouble)
				newBlock = Instantiate(_bulildingSetup.GroundstraightCornerDouble[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.GroundWindowDouble)
				newBlock = Instantiate(_bulildingSetup.GroundwindowDouble[0].transform, buildingObjectTransform);

			// Full Faced Block
			else if (blockSymbol == BlockSymbol.GroundFullFacedBlock)
				newBlock = Instantiate(_bulildingSetup.GroundfullFacedBlock[0].transform, buildingObjectTransform);

			// ************** Mid Floor ****************
			else if (blockSymbol == BlockSymbol.Window)
				newBlock = Instantiate(_bulildingSetup.WindowPrefab[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.CornerLeft)
				newBlock = Instantiate(_bulildingSetup.CornerLeftPrefab[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.DepthLeft) // Depth type
				newBlock = Instantiate(_bulildingSetup.DepthLeftPrefab[0].transform, buildingObjectTransform);

			// Mid floor Double Blocks
			// depth - Double
			else if (blockSymbol == BlockSymbol.DepthCornerDoubleLeft) // Depth Type
				newBlock = Instantiate(_bulildingSetup.DepthCornerLeftDouble[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.DepthDouble)
				newBlock = Instantiate(_bulildingSetup.DepthDouble[0].transform, buildingObjectTransform);
			// straight - Double
			else if (blockSymbol == BlockSymbol.StraightCornerDouble)
				newBlock = Instantiate(_bulildingSetup.StraightCornerDouble[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.WindowDouble)
				newBlock = Instantiate(_bulildingSetup.WindowDouble[0].transform, buildingObjectTransform);

			// Full Faced Block
			else if (blockSymbol == BlockSymbol.FullFacedBlock)
				newBlock = Instantiate(_bulildingSetup.FullFacedBlock[0].transform, buildingObjectTransform);

			// ************** Roof Blocks ****************
			// Roof Top blocks
			else if (blockSymbol == BlockSymbol.RoofTop)
				newBlock = Instantiate(_bulildingSetup.RoofTop[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.RoofTop.MakeGround())
				newBlock = Instantiate(_bulildingSetup.GroundGapRooftop[0].transform, buildingObjectTransform);

			// Roof pieces
			else if (blockSymbol == BlockSymbol.RoofCornerLeft)
				newBlock = Instantiate(_bulildingSetup.RoofCornerLeft[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.RoofWindowFront)
				newBlock = Instantiate(_bulildingSetup.RoofFront[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.RoofDepthLeft)
				newBlock = Instantiate(_bulildingSetup.RoofDepthLeft[0].transform, buildingObjectTransform);

			// Roof Doubles
			else if (blockSymbol == BlockSymbol.RoofDepthCornerDoubleLeft)
				newBlock = Instantiate(_bulildingSetup.RoofDepthCornerDoubleLeft[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.RoofDepthDouble)
				newBlock = Instantiate(_bulildingSetup.RoofDepthDouble[0].transform, buildingObjectTransform);

			else if (blockSymbol == BlockSymbol.RoofStraightCornerDouble)
				newBlock = Instantiate(_bulildingSetup.RoofStraightCornerDouble[0].transform, buildingObjectTransform);
			else if (blockSymbol == BlockSymbol.RoofWindowDouble)
				newBlock = Instantiate(_bulildingSetup.RoofWindowDouble[0].transform, buildingObjectTransform);

			else if (blockSymbol == BlockSymbol.RoofFullFacedBlock)
				newBlock = Instantiate(_bulildingSetup.RoofFullBlock[0].transform, buildingObjectTransform);
			// Gap
			else if (blockSymbol == BlockSymbol.Gap)
				newBlock = Instantiate(_bulildingSetup.GapPrefab.transform, buildingObjectTransform);
			else // Default returns an Exception
			{
				throw new NullReferenceException($"The following blockSymbol name seems to be missing: '{blockSymbol}'" +
												 $" .Add them in ModularBlockContainer class");
			}

			var buildingBlock = newBlock.GetComponent<BuildingBlock>();
			buildingBlock.VerticalMirror = isVerticallyMirrored;
			buildingBlock.HorizontalMirror = isHorizontallyMirrored;
			buildingBlock.IsTop = isTop;
			buildingBlock.BlockType = blockSymbol;
			return buildingBlock;
		}

		#endregion
	}
}
