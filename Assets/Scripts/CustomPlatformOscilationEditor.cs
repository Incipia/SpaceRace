using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(PlatformOscilation))]
public class CustomPlatformOscilationEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		PlatformOscilation myScript = (PlatformOscilation)target;
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
