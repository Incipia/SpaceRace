using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMoveAlongCurve : MonoBehaviour 
{
	public float duration = 5.0f;
	public LeanTweenType easing = LeanTweenType.easeInOutCubic;
	public List<Transform> points;
	public CameraFollow cameraFollow;
	
	void Start () 
	{
		if(points.Count != 4)
		{
			Debug.LogError("You need 4 points to animate on a curve");
		}
		
		LTBezierPath ltPath = new LTBezierPath(new Vector3[]{ points[0].position, points[2].position, points[1].position, points[3].position });
		LeanTween.move(gameObject, ltPath.pts, duration).setEase(easing).setOnComplete(TurnOnCameraFollow);
	}
	
	void TurnOnCameraFollow()
	{
		cameraFollow.enabled = true;
	}
}
