using UnityEngine;
using System.Collections;

public enum JumpDirection {
	Left,
	Right,
	Invalid
}

public class MovePlayer : MonoBehaviour 
{
	public Vector2 maxVelocity = new Vector2(20, 35);
	public float maxFallVelocity = -25.0f;
	public float jumpAngle = 25; // the vertical offset angle
	public float jumpForce = 35;
	public Rigidbody2D playerRigidBody;

	private JumpDirection jumpDirection = JumpDirection.Invalid;

	private Vector2 environmentForceToAdd;
	private Vector2 environmentImpulseToAdd;

	void Start()
	{
		environmentForceToAdd = Vector2.zero;
		environmentImpulseToAdd = Vector2.zero;
	}

	void FixedUpdate()
	{
		if (jumpDirection != JumpDirection.Invalid)
		{
			applyForceWithDirection(jumpDirection);
			resetJumpDirection();
			capVelocity();
		}

		addEnvironmentalForces();
		resetEnvironmentalForces();
		capFallSpeed();
	}

	public void addForce(Vector2 forceVector)
	{
		environmentForceToAdd += forceVector;
	}

	public void addImpulse(Vector2 impulseVector)
	{
		environmentImpulseToAdd += impulseVector;
	}

	public void jumpWithDirection(JumpDirection direction)
	{
		jumpDirection = direction;
	}

	public void jumpWithTouchInputSide(TouchInputSide side)
	{
		jumpDirection = jumpDirectionForTouchInputSide(side);
	}

	private void addEnvironmentalForces()
	{
		if (environmentForceToAdd != Vector2.zero)
		{	
			playerRigidBody.AddForce(environmentForceToAdd);
		}

		if (environmentImpulseToAdd != Vector2.zero)
		{
			playerRigidBody.AddForce(environmentImpulseToAdd, ForceMode2D.Impulse);
		}
	}

	private void resetEnvironmentalForces()
	{
		environmentForceToAdd = Vector2.Lerp(environmentForceToAdd, Vector2.zero, 0.1f);
		environmentImpulseToAdd = Vector2.zero;
	}

	private void resetJumpDirection()
	{
		jumpDirection = JumpDirection.Invalid;
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

		float xVelocityMultiplier = velocity.x < 1 ? -1 : 1;

		float xVelocity = Mathf.Min(maxVelocity.x, Mathf.Abs(velocity.x)) * xVelocityMultiplier;
		float yVelocity = Mathf.Min(maxVelocity.y, velocity.y);

		playerRigidBody.velocity = new Vector2(xVelocity, yVelocity);
	}

	private void capFallSpeed()
	{
		Vector2 velocity = playerRigidBody.velocity;
		if(velocity.y < 0)
		{
			playerRigidBody.velocity = new Vector2(velocity.x, Mathf.Max(velocity.y, maxFallVelocity));
		}
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
