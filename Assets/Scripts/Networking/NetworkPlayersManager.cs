using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class NetworkPlayersManager : MonoBehaviour 
{
	private List<GameObject> currentPlayersInRoom = new List<GameObject>();
	private List<MovePlayerPhoton> movePlayerScripts = new List<MovePlayerPhoton>();
	
	public GameObject playerPrefab;
	public bool autoDisablePlayerMovementOnCreate;

	public GameObject createAndTrackPlayer(Vector3 startPos)
	{
		GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, startPos, Quaternion.identity, 0);
		currentPlayersInRoom.Add(player);

		MovePlayerPhoton movePlayer = player.GetComponent<MovePlayerPhoton>();
		if (movePlayer != null)
		{
			movePlayer.enabled = !autoDisablePlayerMovementOnCreate;
			movePlayerScripts.Add(movePlayer);
		}

		return player;
	}

	public void disableTrackedPlayerMovement()
	{
		setMovePlayerScriptsEnabled(false);
	}

	public void enableTrackedPlayerMovement()
	{
		setMovePlayerScriptsEnabled(true);
	}

	private void setMovePlayerScriptsEnabled(bool enabled)
	{
		foreach(MovePlayerPhoton movePlayer in movePlayerScripts)
		{
			movePlayer.enabled = enabled;
		}
	}
}
