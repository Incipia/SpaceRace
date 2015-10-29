﻿using UnityEngine;
using System.Collections;

public class NetworkPlayerNumberSetup : Photon.MonoBehaviour
{
	public CounterUI playerNumberDisplay;
	public int playerNumber;

	void Start()
	{
		if (photonView.isMine)
		{
			int playerNumber = PhotonNetwork.room.playerCount;
			updatePlayerDisplayNumber(playerNumber);
		}
	}

	[PunRPC] void updatePlayerDisplayNumber(int number)
	{
		playerNumber = number;
		playerNumberDisplay.updateSpritesWithNumber(number);
		if (photonView.isMine)
		{
			photonView.RPC("updatePlayerDisplayNumber", PhotonTargets.OthersBuffered, number);
		}
	}
}