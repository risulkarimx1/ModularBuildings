using System;
using UnityEngine;

namespace ModularBuildings
{
	public class RoofCreator
	{
		private readonly BuildingBlockContainer _buildingBlockContainer;
		private readonly BuildingBlock[,,] _building;
		private readonly GameObject _buildingObject;

		public RoofCreator(BuildingBlockContainer buildingBlockContainer, BuildingBlock[,,] building,
			GameObject buildingObject)
		{
			_buildingBlockContainer = buildingBlockContainer;
			_building = building;
			_buildingObject = buildingObject;
		}

		public void CreateRoof(int i, int j, int k, Vector3 blockPosition)
		{
			var isDepthBlock = false;
			var roofBlock = _buildingBlockContainer.GetBlock(_building[i, j, k].BlockType.GetRoofName(), _buildingObject.transform);
			// Depth and Depth Double
			if (_building[i, j, k].BlockType.Contains(BlockSymbol.DepthLeft) || _building[i, j, k].BlockType.Contains(BlockSymbol.DepthDouble)) // For DepthLeft
			{
				isDepthBlock = true;
			}

			if (roofBlock == null)
			{
				throw new NotImplementedException("The Block is not set for roof.. Add roof book in BlockSymbol class");
			}

			roofBlock.CalculateDimension();
			var position = blockPosition;
			var scale = roofBlock.transform.localScale;

			position.y += roofBlock.Dimension.Height;
			if (isDepthBlock) // Extra roof placement calculation for Depth Roof Blocks.. can it be fixed later ?? TODO: Show Tom- Risul
			{
				position.x += roofBlock.Dimension.Width / 2;
				position.z -= roofBlock.Dimension.Width / 2;
			}

			if (_building[i, j, k].VerticalMirror)
			{
				position.z -= roofBlock.Dimension.Depth;
				scale.z = -1;
			}

			if (_building[i, j, k].HorizontalMirror)
			{
				if (isDepthBlock
				) // Calculate Horizontal mirror differently // Extra roof placement calculation for Depth Roof Blocks.. can it be fixed later ?? TODO: Show Tom- Risul
				{
					position.x -= roofBlock.Dimension.Width;
					scale.z = -1;
				}
				else
					scale.x = -1;
			}

			roofBlock.transform.localScale = scale;
			roofBlock.transform.position = position;
		}
	}
}
