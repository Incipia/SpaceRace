using UnityEngine;
using System.Collections;

public enum JumpDirection {
	Left,
	Right,
	Invalid
}

public class MovePlayer : MonoBehaviour 
{
	public float maxYVelocity = 40;
	public float jumpAngle = 25; // the vertical offset angle
	public float jumpForce = 35;
	public Rigidbody2D playerRigidBody;

	private JumpDirection jumpDirection = JumpDirection.Invalid;

	void FixedUpdate()
	{
		if (jumpDirection != JumpDirection.Invalid)
		{
			// apply force from tap and cap it off at max velocity
			applyForceWithDirection(jumpDirection);
			jumpDirection = JumpDirection.Invalid;

			capVelocity();
			// apply forces from environment
		}
	}

	public void jumpWithDirection(JumpDirection direction)
	{
		jumpDirection = direction;
	}

	public void jumpWithTouchInputSide(TouchInputSide side)
	{
		jumpDirection = jumpDirectionForTouchInputSide(side);
	}

	private void applyForceWithDirection(JumpDirection direction)
	{
		float angle = angleForDirection(direction);
		Vector3 directionVector = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

		playerRigidBody.AddForce(directionVector * jumpForce, ForceMode2D.Impulse);
	}

	private void capVelocity()
	{
		Vector2 velocity = playerRigidBody.velocity;
		float yVelocity = Mathf.Min(maxYVelocity, velocity.y);
		playerRigidBody.velocity = new Vector2(velocity.x, yVelocity);
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

	private JumpDirection jumpDirectionForTouchInputSide(TouchInputSide side)
	{
		JumpDirection direction = JumpDirection.Invalid;
		switch (side)
		{
		case TouchInputSide.Left:
			direction = JumpDirection.Left;
			break;
		case TouchInputSide.Right:
			direction = JumpDirection.Right;
			break;
		}
		return direction;
	}
}
