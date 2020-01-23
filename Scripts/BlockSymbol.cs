using UnityEngine;

namespace ModularBuildings
{
	public static class StringExtensionMirror
	{
		public static string HorizontalMirror(this string blockName)
		{
			return blockName + "," + BlockSymbol.HorizontalMirror;
		}

		public static string VerticalMirror(this string blockName)
		{
			return blockName + "," + BlockSymbol.VerticalMirror;
		}

		public static string AddTop(this string blockName)
		{
			return blockName + "," + BlockSymbol.Top;
		}

		public static string MakeGround(this string blockName)
		{
			return $"{BlockSymbol.Ground}-{blockName}";
		}

		public static string GetRoofName(this string blockName)
		{
			// The roof block is same fom Ground and Middle level blocks
			if (blockName.Contains(BlockSymbol.Ground))
			{
				blockName = blockName.Replace($"{BlockSymbol.Ground}-", "");
			}

			return $"{BlockSymbol.Roof}-{blockName}";
		}
	}

	public struct BlockSymbol
	{
		// Gap Symbols
		public static readonly string Gap = "_";

		// Mirror PostFix
		public static readonly string VerticalMirror = "VerticalMirror"; // Used to flip the direction for back
		public static readonly string HorizontalMirror = "HorizontalMirror"; // Used to flip the direction for back
		public static readonly string Top = "Top"; // Used to flip the direction for back
		public static readonly string Ground = "Ground"; // Used to add ground blocks
		public static readonly string Roof = "Roof"; // Used to add ground blocks

		// ************** Mid Floor ****************
		//  Window Symbols.....
		public static readonly string Window = "Window";

		// Corner Symbols.....
		public static readonly string CornerLeft = "C-Left";
		public static readonly string DepthLeft = "Depth-Left";

		// Double Symbols - Depth
		public static readonly string DepthCornerDoubleLeft = "Depth-Corner-Double-Left";
		public static readonly string DepthDouble = "Depth-Double";

		// Double Symbols - Straight Window
		public static readonly string StraightCornerDouble = "Straight-Corner-Double";
		public static readonly string WindowDouble = "Window-Double";

		// Full Faced block
		public static readonly string FullFacedBlock = "Full-Faced";

		// ************** Ground Floor ****************
		// Ground Entry window / Door
		public static readonly string GroundWindow = $"{Ground}-{Window}";

		// Corner Symbols
		public static readonly string GroundCornerLeft = $"{Ground}-{CornerLeft}";

		public static readonly string GroundDepthLeft = $"{Ground}-{DepthLeft}";

		//Ground Gap Symbols
		public static readonly string GroundGap = $"{Ground}-{Gap}";

		// Double Symbols - Depth
		public static readonly string GroundDepthCornerDoubleLeft = $"{Ground}-Depth-Corner-Double-Left";
		public static readonly string GroundDepthDouble = $"{Ground}-Depth-Double";

		// Double Symbols - Straight Window
		public static readonly string GroundStraightCornerDouble = $"{Ground}-Straight-Corner-Double";
		public static readonly string GroundWindowDouble = $"{Ground}-Window-Double";

		// Full Faced block
		public static readonly string GroundFullFacedBlock = $"{Ground}-Full-Faced";

		// ************** Roof Symbols ****************
		public static readonly string RoofTop = "Rooftop";

		public static readonly string RoofCornerLeft = $"{Roof}-{CornerLeft}";
		public static readonly string RoofWindowFront = $"{Roof}-{Window}";

		public static readonly string RoofDepthLeft = $"{Roof}-{DepthLeft}";

		// Roof double blocks
		public static readonly string RoofDepthCornerDoubleLeft = $"{Roof}-{DepthCornerDoubleLeft}";
		public static readonly string RoofDepthDouble = $"{Roof}-{DepthDouble}";

		// Double Symbols - Straight Window
		public static readonly string RoofStraightCornerDouble = $"{Roof}-{StraightCornerDouble}";
		public static readonly string RoofWindowDouble = $"{Roof}-{WindowDouble}";

		// Full Faced block
		public static readonly string RoofFullFacedBlock = $"{Roof}-{FullFacedBlock}";


		// TODO: Get rid of them when done debugging- Risul

		#region DebugArea 

		public static Color GetColor(string BlockType, float transparency)
		{
			// Gap
			if (BlockType == Gap)
				return Color.clear;
			return new Color(Random.Range(.1f, 1f), Random.Range(.1f, 1f), Random.Range(.1f, 1f), transparency);
		}

		public static string GetName(string BlockType)
		{
			if (BlockType == Gap)
				BlockType = "";
			return BlockType;
		}

		#endregion
	}
}
