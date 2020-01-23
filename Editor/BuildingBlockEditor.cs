using UnityEditor;
using UnityEngine;

namespace ModularBuildings
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(BuildingBlock))]
	public class BuildingBlockEditor : Editor
	{
		private BuildingBlock _buildingBlock;
		private Vector3 _overrideDimension;

		private void OnEnable()
		{
			_buildingBlock = (BuildingBlock) target;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			DrawDefaultInspector();
			GUILayout.Label($"Block Type: {_buildingBlock.BlockType}");
			//if (GUILayout.Button($"Override Value: {_buildingBlock._hasOverridingDimensionValue}"))
			//{
			//    _buildingBlock._hasOverridingDimensionValue=!_buildingBlock._hasOverridingDimensionValue;
			//}
			//if (_buildingBlock._hasOverridingDimensionValue)
			//{
			//    _buildingBlock._overridenBlockDimension = EditorGUILayout.Vector3Field("Dimension", _buildingBlock._overridenBlockDimension);
			//}
			if (GUILayout.Button("Calculate Dimension"))
			{
				_buildingBlock.CalculateDimension();
			}

			GUILayout.Label($"Width x Height x Depth: " +
							$"{_buildingBlock.Dimension.Width}" +
							$"x{_buildingBlock.Dimension.Height}" +
							$"x{_buildingBlock.Dimension.Depth}");


			serializedObject.ApplyModifiedProperties();
		}
	}
}
