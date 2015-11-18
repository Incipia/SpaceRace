using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public enum JumpDirection {
	Left,
	Right,
	Down,
	Invalid
}

public class MovePlayer : Photon.MonoBehaviour
{
	public Vector2 baseMaxVelocity = new Vector2(1, 2);
	public float returnToMaxVelocitySpeed = 0.08f;
	public float maxFallVelocity = -30.0f;
	public float jumpAngle = 50; // the vertical offset angle
	public float jumpForce = 100;
	public Rigidbody2D playerRigidBody;
	public GameObject trailParticles;

	private JumpDirection jumpDirection = JumpDirection.Invalid;

	private Vector2 environmentForceToAdd;
	private Vector2 environmentImpulseToAdd;
	private Vector2 maxVelocity;
	private Vector2 previousVelocity;

	private float initialJumpAngle;
	private bool controlsActive;

	void Start()
	{
		initialJumpAngle = jumpAngle;
		environmentForceToAdd = Vector2.zero;
		environmentImpulseToAdd = Vector2.zero;
		maxVelocity = baseMaxVelocity;
		previousVelocity = Vector2.zero;
		controlsActive = true;
		
		if (photonView == null || photonView.isMine)
		{
			GameObject.Find("Left Input").GetComponent<PlayerTouchInput>().movePlayer = this;
			GameObject.Find("Right Input").GetComponent<PlayerTouchInput>().movePlayer = this;
		}
	}

	void Update()
	{
		if (photonView == null || photonView.isMine)
		{
			if (jumpDirection != JumpDirection.Invalid && controlsActive)
			{
				// sanity check?
//				if (playerRigidBody.gravityScale != 0.1f)
//				{
//					playerRigidBody.gravityScale = 0.1f;
//				}

				applyForceWithDirection(jumpDirection);
				resetJumpDirection();
			}
			
			addEnvironmentalForces();
			resetEnvironmentalForces();
			
			capVelocity();
			capFallSpeed();
			ReturnToMaxVelocity();
			velocityDetectionForGravity();
		}
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
		if (enabled && controlsActive)
		{
			jumpDirection = direction;
		}
	}

	public void Stun(float duration, bool deactivateParticles)
	{
		if(controlsActive)
		{
			StartCoroutine(ActualStun(duration, deactivateParticles));
		}
	}

	public void SetMaxVelocity(Vector2 newMaxVelocity)
	{
		maxVelocity = newMaxVelocity;
	}

	public void ReturnToMaxVelocity()
	{
		maxVelocity = Vector2.Lerp(maxVelocity, baseMaxVelocity, returnToMaxVelocitySpeed);
	}

	public void resetToPosition(Vector3 position)
	{
		playerRigidBody.velocity = Vector3.zero;
		transform.position = position;
	}

	private IEnumerator ActualStun(float duration, bool deactivateParticles)
	{
		controlsActive = false;
		playerRigidBody.gravityScale = 0;
		if (deactivateParticles)
		{
			trailParticles.SetActive(false);
		}

		yield return new WaitForSeconds(duration);

		if (deactivateParticles)
		{
			trailParticles.SetActive(true);
		}
		playerRigidBody.gravityScale = 1;
		controlsActive = true;
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

		float xVelocityMultiplier = velocity.x < 0.0f ? -1.0f : 1.0f;

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
		case JumpDirection.Down:
			angle = -90;
			break;
		case JumpDirection.Invalid:
			break;
		}
		return angle;
	}
	
	private void velocityDetectionForGravity()
	{
		Vector2 velocity = playerRigidBody.velocity;
		if(velocity.y > 1.7f && previousVelocity.y <= 1.7f)
		{
			playerRigidBody.gravityScale = 0;
			playerRigidBody.drag = 0;
			Debug.Log ("Gravity Scale = 0");
		}
		else if(velocity.y < 1.7f && previousVelocity.y >= 1.7f)
		{
			playerRigidBody.gravityScale = 0.1f;
			playerRigidBody.drag = 0.25f;
			Debug.Log ("Gravity Scale = 0.1");
		}
		previousVelocity = velocity;
	}
	
//	private void velocityDetectionForGravity()
//	{
//		Vector2 velocity = playerRigidBody.velocity;
//		if(velocity.y < 0)
//		{
//			playerRigidBody.gravityScale = 0;
//			Debug.Log ("Gravity Scale = 0");
//		}
//	}
	
	
	
	
	
	
	
}
