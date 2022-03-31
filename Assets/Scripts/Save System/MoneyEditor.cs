using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EcoIsland
{
	[CustomEditor(typeof(SaveMoney))]
	class MoneyEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			SaveMoney myTarget = (SaveMoney)target;
			DrawDefaultInspector();

			if (GUILayout.Button("Give Money"))
			{
				myTarget.addMoney(50);
			}

			if (GUILayout.Button("Take Money"))
			{
				myTarget.removeMoney(50);
			}
		}
	}
}
