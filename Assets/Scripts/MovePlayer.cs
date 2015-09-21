using UnityEngine;
using System.Collections;

public enum JumpDirection {
	Left,
	Right,
	Invalid
}

public class MovePlayer : MonoBehaviour 
{
	public float jumpAngle = 25; // the vertical offset angle
	public float jumpForce = 35;
	public Rigidbody2D playerRigidBody;

	private JumpDirection jumpDirection = JumpDirection.Invalid;

	void FixedUpdate()
	{
		if (jumpDirection != JumpDirection.Invalid)
		{
			applyForceWithDirection(jumpDirection);
			jumpDirection = JumpDirection.Invalid;
		}
	}

	public void jumpWithDirection(JumpDirection direction)
	{
		jumpDirection = direction;
	}

	public void jumpWithTouchInputSide(TouchInputSide side)
	{
		jumpDirection = side == TouchInputSide.Left ? JumpDirection.Left : JumpDirection.Right;
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
	
	private void applyForceWithDirection(JumpDirection direction)
	{
		float angle = angleForDirection(direction);
		Vector3 directionVector = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
		playerRigidBody.velocity = directionVector * jumpForce;
	}
}
