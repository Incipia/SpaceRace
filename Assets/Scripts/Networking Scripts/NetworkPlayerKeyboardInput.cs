﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkPlayerKeyboardInput : Photon.MonoBehaviour
{
	public bool player1 = true;
	public MovePlayerPhoton movePlayer;

	void Update()
	{
		if (photonView.isMine)
		{
			KeyCode rightMovementCode = player1 ? KeyCode.RightArrow : KeyCode.Period;
			KeyCode leftMovementCode = player1 ? KeyCode.LeftArrow : KeyCode.Comma;
			
			if (Input.GetKeyDown(leftMovementCode))
			{
				movePlayer.jumpWithDirection(JumpDirection.Left);
			}
			if (Input.GetKeyDown(rightMovementCode))
			{
				movePlayer.jumpWithDirection(JumpDirection.Right);
			}
		}
	}
}