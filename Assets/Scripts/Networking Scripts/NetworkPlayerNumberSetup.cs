using UnityEngine;
using System.Collections;

public class NetworkPlayerNumberSetup : Photon.MonoBehaviour
{
	public CounterUI playerNumberDisplay;
	public int playerNumber;

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
		playerNumber = number;
		playerNumberDisplay.updateSpritesWithNumber(number);

		if (photonView.isMine)
		{
			photonView.RPC("updatePlayerDisplayNumber", PhotonTargets.OthersBuffered, number);
		}
	}
}
