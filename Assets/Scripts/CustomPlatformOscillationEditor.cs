using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(PlatformOscillation))]
public class CustomPlatformOscillationEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		PlatformOscillation myScript = (PlatformOscillation)target;
		if (GUILayout.Button("Store Transform"))
		{
			myScript.storeTransform();
		}
		if (GUILayout.Button("Close Path"))
		{
			myScript.closePath();
		}
		if (GUILayout.Button("Set Object To Start"))
		{
			myScript.setObjectToFirstPoint();
		}
		if (GUILayout.Button("Reset"))
		{
			myScript.reset();
		}
	}
}
