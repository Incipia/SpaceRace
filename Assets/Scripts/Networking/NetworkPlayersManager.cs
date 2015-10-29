using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class NetworkPlayersManager : MonoBehaviour 
{
	private List<GameObject> currentPlayersInRoom = new List<GameObject>();
	private List<MovePlayerPhoton> movePlayerScripts = new List<MovePlayerPhoton>();
	
	public GameObject playerPrefab;
	public bool autoDisablePlayers;

	public void createNewPlayer()
	{
		Room currentRoom = PhotonNetwork.room;
		List<Vector3> startPositions = PlayerStartPositionProvider.startPositionsForRoomSize(currentRoom.maxPlayers);
		
		Vector3 position = startPositions[currentRoom.playerCount-1];
		GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity, 0);
		currentPlayersInRoom.Add(player);

		MovePlayerPhoton movePlayer = player.GetComponent<MovePlayerPhoton>();
		if (movePlayer != null)
		{
			movePlayer.enabled = !autoDisablePlayers;
			movePlayerScripts.Add(movePlayer);
		}
	}

	public void disableTrackedPlayers()
	{
		setMovePlayerScriptsEnabled(false);
	}

	public void enableTrackedPlayers()
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
