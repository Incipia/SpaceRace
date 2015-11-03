using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public enum JumpDirection {
	Left,
	Right,
	Invalid
}

public class MovePlayer : MonoBehaviour 
{
	public Vector2 maxVelocity = new Vector2(15, 30);
	public float maxFallVelocity = -30.0f;
	public float jumpAngle = 50; // the vertical offset angle
	public float jumpForce = 100;
	public Rigidbody2D playerRigidBody;
	public GameObject trailParticles;

	private JumpDirection jumpDirection = JumpDirection.Invalid;

	private Vector2 environmentForceToAdd;
	private Vector2 environmentImpulseToAdd;

	private float initialJumpAngle;
	private bool maxVelocityDisabled;
	private bool readyToEnableMaxVelocity;
	private bool controlsActive;

	void Start()
	{
		initialJumpAngle = jumpAngle;
		environmentForceToAdd = Vector2.zero;
		environmentImpulseToAdd = Vector2.zero;
		controlsActive = true;
	}

	void FixedUpdate()
	{
		if (jumpDirection != JumpDirection.Invalid && controlsActive)
		{
			applyForceWithDirection(jumpDirection);
			resetJumpDirection();
			capVelocity();
		}

		addEnvironmentalForces();
		resetEnvironmentalForces();
		capFallSpeed();

		if (maxVelocityDisabled && readyToEnableMaxVelocity)
		{
			enableMaxVelocityIfNecessary();
		}
	}

	public void enableMaxVelocity()
	{
		readyToEnableMaxVelocity = true;
	}

	public void disableMaxVelocity()
	{
		playerRigidBody.gravityScale = 2;
		maxVelocityDisabled = true;
		disableAngularMovementFromTouchInput();
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

	public void Stun(float duration, bool deactivateParticles)
	{
		StartCoroutine(ActualStun(duration, deactivateParticles));
	}

	private IEnumerator ActualStun(float duration, bool deactivateParticles)
	{
		controlsActive = false;
		if (deactivateParticles)
		{
			trailParticles.SetActive(false);
		}

		float timer = 0.0f;
		while(timer < duration)
		{
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		controlsActive = true;
		if (deactivateParticles)
		{
			trailParticles.SetActive(true);
		}
	}

	private void enableMaxVelocityIfNecessary()
	{
		if (playerRigidBody.velocity.y <= maxVelocity.y)
		{
			enableAngularMovementFromTouchInput();
			maxVelocityDisabled = false;
			readyToEnableMaxVelocity = false;

			playerRigidBody.gravityScale = 1;
		}
	}

	private void disableAngularMovementFromTouchInput()
	{
		jumpAngle = 90;
	}

	private void enableAngularMovementFromTouchInput()
	{
		jumpAngle = initialJumpAngle;
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
		environmentForceToAdd = Vector2.zero;
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
		float yVelocity = maxVelocityDisabled ? velocity.y : Mathf.Min(maxVelocity.y, velocity.y);

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
}
