using UnityEngine;

namespace ModularBuildings
{
	[CreateAssetMenu(fileName = "BuildingSetup", menuName = "AAI/Modular Buildings/BuildingSetup", order = 1)]
	public class BuildingBlockSetup : ScriptableObject
	{
		[Header("-------------------Ground Floor Prefabs---------------------------")]

		#region Ground Floor Prefabs

		[Space(30)]
		public BuildingBlock[] GroundWindowPrefab;

		[Header("Corner Prefabs")]
		[Space(10)]
		public BuildingBlock[] GroundCornerLeftPrefab;

		[Header("Depth Prefabs")]
		[Space(10)]
		public BuildingBlock[] GroundDepthLeftPrefab;

		[Header("Gap Prefabs")]
		[Space(10)]
		public BuildingBlock[] GroundGap;

		// Double and Full faced
		[Header("Double Prefabs - Depth")]
		[Space(10)]
		public BuildingBlock[] GrounddepthCornerLeftDouble;

		public BuildingBlock[] GrounddepthDouble;

		[Header("Double Prefabs - Straight")]
		[Space(10)]
		public BuildingBlock[] GroundstraightCornerDouble;

		public BuildingBlock[] GroundwindowDouble;

		[Header("Full Faced Block")]
		[Space(10)]
		public BuildingBlock[] GroundfullFacedBlock;

		#endregion

		[Header("-----------------Mid floor Prefabs--------------------")]

		#region MidFloor Prefabs

		[Space(30)]
		public BuildingBlock[] WindowPrefab;

		[Header("Corner Prefabs")]
		[Space(10)]
		public BuildingBlock[] CornerLeftPrefab;

		[Header("Depth Prefabs")]
		[Space(10)]
		public BuildingBlock[] DepthLeftPrefab;

		[Header("Gap Prefabs")]
		[Space(10)]
		public BuildingBlock GapPrefab;

		// Double and Full faced
		[Header("Double Prefabs - Depth")]
		[Space(10)]
		public BuildingBlock[] DepthCornerLeftDouble;

		public BuildingBlock[] DepthDouble;

		[Header("Double Prefabs - Straight")]
		[Space(10)]
		public BuildingBlock[] StraightCornerDouble;

		public BuildingBlock[] WindowDouble;

		[Header("Full Faced Block")]
		[Space(10)]
		public BuildingBlock[] FullFacedBlock;

		#endregion

		[Header("-------------------Roof Prefabs---------------------------")]

		#region Roof Prefabs

		[Space(30)]
		public BuildingBlock[] RoofTop;

		public BuildingBlock[] GroundGapRooftop;

		public BuildingBlock[] RoofCornerLeft;
		public BuildingBlock[] RoofFront;
		public BuildingBlock[] RoofDepthLeft;

		[Header("Double Roof blocks")]
		public BuildingBlock[] RoofDepthCornerDoubleLeft;

		public BuildingBlock[] RoofDepthDouble;

		public BuildingBlock[] RoofStraightCornerDouble;
		public BuildingBlock[] RoofWindowDouble;

		public BuildingBlock[] RoofFullBlock;

		#endregion
	}
}
