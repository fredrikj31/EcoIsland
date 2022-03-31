using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EcoIsland
{
	[CustomEditor(typeof(SiloStorage))]
	class SiloEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			SiloStorage myTarget = (SiloStorage)target;
			DrawDefaultInspector();

			if (GUILayout.Button("Save Item to Silo"))
			{
				myTarget.addCrop(CropTypes.Wheat);
			}
			if (GUILayout.Button("Remove Item from Silo"))
			{
				myTarget.removeCrop(CropTypes.Wheat);
			}
		}
	}
}