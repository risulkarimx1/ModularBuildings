using UnityEditor;
using UnityEngine;

namespace ModularBuildings
{
	[CustomEditor(typeof(ModularBuildingSystem))]
	public class ModularBuildingSystemEditor : Editor
	{
		private ModularBuildingSystem _modularBuildingSystem;

		private void OnEnable()
		{
			_modularBuildingSystem = (ModularBuildingSystem) target;
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			GUILayout.Label("Rules Generator");
			GUILayout.BeginHorizontal();

			if (GUILayout.Button("Visualize Block Rules", GUILayout.Height(30)))
			{
				_modularBuildingSystem.VisualizeBlocks();
			}

			GUILayout.EndHorizontal();
			GUILayout.Label("Building Creation");
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			if (GUILayout.Button("Generate Blocks"))
			{
				_modularBuildingSystem.GenerateBlocks();
			}

			if (GUILayout.Button("Place Blocks"))
			{
				_modularBuildingSystem.PlaceBlocks();
			}

			if (GUILayout.Button("Fix Pivot"))
			{
				_modularBuildingSystem.FixPivot();
			}

			if (GUILayout.Button("Clear Gap and Components"))
			{
				_modularBuildingSystem.ClearGapAndComponent();
			}

			if (GUILayout.Button("Add to Mesh Baker"))
			{
				_modularBuildingSystem.AddToMeshBaker();
			}

			GUILayout.EndVertical();
			GUILayout.BeginVertical();
			if (GUILayout.Button("Create Building", GUILayout.Height(100)))
			{
				_modularBuildingSystem.GenerateBlocks();
				_modularBuildingSystem.PlaceBlocks();
				_modularBuildingSystem.FixPivot();
				_modularBuildingSystem.ClearGapAndComponent();
				_modularBuildingSystem.AddToMeshBaker();
			}

			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}
	}
}
