using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveIsland))]
class SaveEditor : Editor
{
	public override void OnInspectorGUI()
	{
		SaveIsland myTarget = (SaveIsland)target;
		DrawDefaultInspector();

		if (GUILayout.Button("Save World"))
		{
			myTarget.saveTilemaps();
		}

		if (GUILayout.Button("Load World"))
		{
			myTarget.loadTilemaps();
		}

		if (GUILayout.Button("Save Objects"))
		{
			myTarget.saveObjects();
		}
		if (GUILayout.Button("Load Objects"))
		{
			myTarget.loadObjects();
		}
	}
}
