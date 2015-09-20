using UnityEngine;
using System.Collections;

public enum JumpDirection {
	Left,
	Right,
	Invalid
}

public class MovePlayer : MonoBehaviour 
{
	public float jumpAngle; // the vertical offset angle
	public float jumpForce;

	private Rigidbody2D playerRigidBody;
	
	// Use this for initialization
	void Start() 
	{
		playerRigidBody = GetComponent<Rigidbody2D>();
		if (!playerRigidBody)
		{
			Debug.Log("rigid body on player not found");
		}
	}

	public void jumpWithDirection(JumpDirection direction)
	{
		if (direction != JumpDirection.Invalid)
		{
			float angle = angleForDirection(direction);
			Vector3 jumpDirection = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
			applyForceWithDirection(jumpDirection);
		}
	}

	public void jumpWithTouchInputSide(TouchInputSide side)
	{
		JumpDirection direction = side == TouchInputSide.Left ? JumpDirection.Left : JumpDirection.Right;
		jumpWithDirection(direction);
	}

	private float angleForDirection(JumpDirection direction)
	{
		float angle = 0;
		switch (direction)
		{
		case JumpDirection.Left:
			angle = 90 + jumpAngle;
			break;
		case JumpDirection.Right:
			angle = 90 - jumpAngle;
			break;
		case JumpDirection.Invalid:
			break;
		}
		return angle;
	}
	
	private void applyForceWithDirection(Vector2 direction)
	{
		resetRigidBody();
		playerRigidBody.AddForce(direction * jumpForce, ForceMode2D.Impulse);
	}
	
	private void resetRigidBody()
	{
		playerRigidBody.isKinematic = true;
		playerRigidBody.isKinematic = false;
	}

	void OnMouseDown()
	{
		jumpWithDirection(JumpDirection.Right);
	}
}
