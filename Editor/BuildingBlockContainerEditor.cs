using UnityEditor;
using UnityEngine;

namespace ModularBuildings
{
	[CustomEditor(typeof(BuildingBlockContainer))]
	public class BuildingBlockContainerEditor : Editor
	{
		private BuildingBlockContainer _buildingBlockContainer;

		private void OnEnable()
		{
			_buildingBlockContainer = (BuildingBlockContainer) target;
		}

		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Prepare Block"))
			{
				//_buildingBlockContainer.PrepareBlock();
			}

			DrawDefaultInspector();
		}
	}
}
