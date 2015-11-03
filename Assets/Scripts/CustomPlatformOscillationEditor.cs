#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

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
		if (GUILayout.Button ("Invert X Positions"))
		{
			myScript.invertXPositions();
		}
		if (GUILayout.Button ("Reverse All Positions"))
		{
			myScript.reverseAllPositions();
		}
		if (GUILayout.Button("Reset"))
		{
			myScript.reset();
		}
	}
}
#endif
