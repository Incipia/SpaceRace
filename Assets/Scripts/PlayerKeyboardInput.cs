using UnityEngine;
using System.Collections;

public class PlayerKeyboardInput : MonoBehaviour 
{
	public bool player1 = true;
	public MovePlayer movePlayer;

	void Update()
	{
		KeyCode rightMovementCode = player1 ? KeyCode.RightArrow : KeyCode.D;
		KeyCode leftMovementCode = player1 ? KeyCode.LeftArrow : KeyCode.A;

		if (Input.GetKeyDown(leftMovementCode))
		{
			movePlayer.jumpWithDirection(JumpDirection.Left);
		}
		if(Input.GetKeyDown(rightMovementCode))
		{
			movePlayer.jumpWithDirection(JumpDirection.Right);
		}
	}
}
