using UnityEngine;
using System.Collections;

public class NetworkRoomManager : Photon.PunBehaviour
{
	private string _gameVersion = "0.1.1";
	private bool _requestedToJoinRoom = false;
	
	private Room _currentRoom { get { return PhotonNetwork.room; } }
	private bool _roomIsFull { get { return _currentRoom.maxPlayers == _currentRoom.playerCount; }}

	// Use this for initialization
	void Start() 
	{	
		Debug.Log("Connecting to Photon...");
		PhotonNetwork.ConnectUsingSettings(_gameVersion);
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (PhotonNetwork.connectedAndReady && _requestedToJoinRoom == false)
		{
			Debug.Log("Connected! Trying to create/join a room...");

			PhotonNetwork.automaticallySyncScene = true;
			RoomOptions roomOptions = new RoomOptions() {
				maxPlayers = 2,
				isVisible = false
			};
			PhotonNetwork.JoinOrCreateRoom("default2", roomOptions, TypedLobby.Default);
			_requestedToJoinRoom = true;
		}
	}

	void OnPhotonPlayerConnected (PhotonPlayer newPlayer)
	{
		Debug.Log("Player joined the room!");
	}

	void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom()");
		if (_roomIsFull)
		{
			Debug.Log("Room is full!  Loading the next level");

			photonView.RPC("loadFirstLevel", PhotonTargets.MasterClient);
		}
	}

	[PunRPC] void loadFirstLevel()
	{
		if (PhotonNetwork.isMasterClient)
		{
			PhotonNetwork.LoadLevel(1);
		}
	}
}
