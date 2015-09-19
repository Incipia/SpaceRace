using UnityEngine;
using System.Collections;

public class PlayerKeyboardInput : MonoBehaviour 
{
	public bool player1 = true;
	private MovePlayer movePlayer;

	void Start()
	{
		movePlayer = GetComponent<MovePlayer>();
		if (!movePlayer)
		{
			Debug.Log("could not fine MovePlayer script");
		}
	}

	void FixedUpdate()
	{
		KeyCode rightMovementCode = player1 ? KeyCode.RightArrow : KeyCode.D;
		KeyCode leftMovementCode = player1 ? KeyCode.LeftArrow : KeyCode.A;

		JumpDirection direction = JumpDirection.Invalid;
		if (Input.GetKey(leftMovementCode))
		{
			direction = JumpDirection.Left;
		}
		else if(Input.GetKey (rightMovementCode))
		{
			direction = JumpDirection.Right;
		}
		movePlayerWithDirection(direction);
	}

	private void movePlayerWithDirection(JumpDirection direction)
	{
		if (movePlayer)
		{
			movePlayer.jumpWithDirection(direction);
		}
	}
}
