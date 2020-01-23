using UnityEngine;

namespace ModularBuildings
{
	/// <summary>
	///     Custom editor <see cref="ModularFootprintGeneratorEditor" />
	/// </summary>
	public class ModularFootprintGenerator : MonoBehaviour
	{
		[SerializeField] private ModularBuildingSystem modularBuildingSystem;
		public bool[,] FootprintArray;

		private int _facadeDepth;
		private int _facadeLength;

		public void CreateFootprint()
		{
			_facadeLength = modularBuildingSystem.modularBuildingInput.BuildingLength;
			_facadeDepth = modularBuildingSystem.modularBuildingInput.BuildingDepth;

			FootprintArray = new bool[_facadeLength, _facadeDepth];
			for (var j = 0; j < FootprintArray.GetLength(1); j++)
			{
				for (var i = 0; i < FootprintArray.GetLength(0); i++)
				{
					FootprintArray[i, j] = true;
				}
			}
		}
	}
}
