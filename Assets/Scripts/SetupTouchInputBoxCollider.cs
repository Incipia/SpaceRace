using UnityEngine;
using System.Collections;

public enum TouchInputSide {
	Left,
	Right
}

public class SetupTouchInputBoxCollider : MonoBehaviour 
{
	public TouchInputSide side;
	private BoxCollider2D boxCollider;
	private Camera mainCamera;

	// Use this for initialization
	void Start() 
	{
		getMainCamera();
		getBoxCollider();

		Vector3 pixelDimensions = new Vector3(Screen.width, Screen.height, 0);
		setupBoxColliderWithPixelDimensions(pixelDimensions);
	}

	private void getMainCamera()
	{
		GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
		if (cameraObject)
		{
			mainCamera = cameraObject.GetComponent<Camera>();
		}
		else
		{
			Debug.Log("could not find main camera");
		}
	}

	private void getBoxCollider()
	{
		boxCollider = GetComponent<BoxCollider2D>();
		if (!boxCollider)
		{
			Debug.Log("could not find BoxCollider2D");
		}
	}

	private void setupBoxColliderWithPixelDimensions(Vector3 pixelDimensions)
	{
		float width = mainCamera.ScreenToWorldPoint(pixelDimensions).x;
		float height = mainCamera.ScreenToWorldPoint(pixelDimensions).y;

		boxCollider.size = new Vector2(width, height * 2);

		float xPosMultiplier = side == TouchInputSide.Left ? -1 : 1;
		boxCollider.offset = new Vector3(width * .5f * xPosMultiplier, 0);
	}
}
