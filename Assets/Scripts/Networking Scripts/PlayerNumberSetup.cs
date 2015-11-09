using UnityEngine;
using System.Collections;

public class PlayerNumberSetup : Photon.MonoBehaviour
{
	public CounterUI playerNumberDisplay;

	void Awake()
	{
		if (photonView.isMine)
		{
			int playerNumber = PhotonNetwork.player.playerNumber();
			updatePlayerDisplayNumber(playerNumber);
		}
	}

	[PunRPC] void updatePlayerDisplayNumber(int number)
	{
		playerNumberDisplay.updateSpritesWithNumber(number);
		if (photonView.isMine)
		{
			photonView.RPC("updatePlayerDisplayNumber", PhotonTargets.OthersBuffered, number);
		}
	}
}
