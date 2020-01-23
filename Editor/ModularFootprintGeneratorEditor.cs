using UnityEditor;
using UnityEngine;

namespace ModularBuildings
{
	[CustomEditor(typeof(ModularFootprintGenerator))]
	public class ModularFootprintGeneratorEditor : Editor
	{
		private ModularFootprintGenerator _modularFootprintGenerator;

		public void OnEnable()
		{
			_modularFootprintGenerator = (ModularFootprintGenerator) target;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			DrawDefaultInspector();

			if (GUILayout.Button("Create Footprint"))
			{
				_modularFootprintGenerator.CreateFootprint();
			}

			if (_modularFootprintGenerator.FootprintArray == null)
				return;

			for (var j = 0; j < _modularFootprintGenerator.FootprintArray.GetLength(1); j++)
			{
				GUILayout.BeginHorizontal();
				for (var i = 0; i < _modularFootprintGenerator.FootprintArray.GetLength(0); i++)
				{
					GUI.color = _modularFootprintGenerator.FootprintArray[i, j] ? Color.green : Color.red;

					if (GUILayout.Button("", GUILayout.Width(20)))
					{
						_modularFootprintGenerator.FootprintArray[i, j] = !_modularFootprintGenerator.FootprintArray[i, j];
					}
				}

				GUILayout.EndHorizontal();
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}
