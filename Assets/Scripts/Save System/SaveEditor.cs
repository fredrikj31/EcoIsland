using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveController))]
class SaveEditor : Editor
{
	public override void OnInspectorGUI()
	{
		SaveController myTarget = (SaveController)target;
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
