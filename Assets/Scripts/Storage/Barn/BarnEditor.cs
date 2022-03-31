using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EcoIsland
{
	[CustomEditor(typeof(BarnStorage))]
	class BarnEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			BarnStorage myTarget = (BarnStorage)target;
			DrawDefaultInspector();

			if (GUILayout.Button("Save Item to Barn"))
			{
				myTarget.addItem(ItemTypes.Bread);
			}
			if (GUILayout.Button("Remove Item from Barn"))
			{
				myTarget.removeItem(ItemTypes.Bread);
			}
		}
	}
}
