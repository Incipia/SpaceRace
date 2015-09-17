using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public bool player1 = true;

	public float maxVelocityY;
	public float verticalOffsetAngle;
	public float thrustForce;

	private Rigidbody2D rigidBody;

	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		KeyCode rightMovementCode = player1 ? KeyCode.RightArrow : KeyCode.D;
		KeyCode leftMovementCode = player1 ? KeyCode.LeftArrow : KeyCode.A;

		if (Input.GetKey(leftMovementCode))
		{
			Vector3 direction = directionForKeyCode(KeyCode.LeftArrow);
			applyForceWithDirection(direction);
		}
		else if(Input.GetKey (rightMovementCode))
		{
			Vector3 direction = directionForKeyCode(KeyCode.RightArrow);
			applyForceWithDirection(direction);
		}
	}

	private Vector3 directionForKeyCode(KeyCode code)
	{
		Vector3 direction = Vector2.zero;
		switch (code)
		{
		case KeyCode.LeftArrow:
			direction = Quaternion.AngleAxis(90 + verticalOffsetAngle, Vector3.forward) * Vector3.right;
			break;
		case KeyCode.RightArrow:
			direction = Quaternion.AngleAxis(90 - verticalOffsetAngle, Vector3.forward) * Vector3.right;
			break;
		}
		return direction;
	}

	private void applyForceWithDirection(Vector2 direction)
	{
		resetRigidBody();
		rigidBody.AddForce(direction * thrustForce, ForceMode2D.Impulse);
	}
	
	private void resetRigidBody()
	{
		rigidBody.isKinematic = true;
		rigidBody.isKinematic = false;
	}
}
