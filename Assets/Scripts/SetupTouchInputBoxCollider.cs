using UnityEngine;
using System.Collections;

public enum TouchInputSide {
	Left,
	Right
}

public class SetupTouchInputBoxCollider : MonoBehaviour 
{
	public TouchInputSide side;
	public BoxCollider2D boxCollider;

	void Start() 
	{
		Vector3 pixelDimensions = new Vector3(Screen.width, Screen.height, 0);
		setupBoxColliderWithPixelDimensions(pixelDimensions);
	}

	private void setupBoxColliderWithPixelDimensions(Vector3 pixelDimensions)
	{
		float width = Camera.main.ScreenToWorldPoint(pixelDimensions).x;
		float height = Camera.main.ScreenToWorldPoint(pixelDimensions).y;
		float xPosMultiplier = side == TouchInputSide.Left ? -1 : 1;

		boxCollider.size = new Vector2(width, height * 2);
		boxCollider.offset = new Vector3(width * .5f * xPosMultiplier, 0);
	}
}
