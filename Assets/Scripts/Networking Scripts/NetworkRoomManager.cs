using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkRoomManager : Photon.PunBehaviour
{
	public ConnectButton connectButton;
	public NetworkRoomConnectionInfo roomConnectionInfo;
	public GameObject timeToPlayText;
	public GameObject roomSelector;
	public string nameOfFirstLevel;

	public List<GameObject> objectsToHideBeforePlaying = new List<GameObject>();

	private string _gameVersion = "0.0.1";
	private bool _requestedToJoinRoom;
	
	private PhotonPlayer _localPlayer { get { return PhotonNetwork.player; }}
	private Room _currentRoom { get { return PhotonNetwork.room; } }
	private bool _roomIsFull { get { return _currentRoom.maxPlayers == _currentRoom.playerCount; }}

	void Start() 
	{
		timeToPlayText.SetActive(false);
		setObjectActive(roomConnectionInfo, false);
		connectButton.setReadyToConnect(false);
		
		Debug.Log("Connecting to Photon...");
		PhotonNetwork.ConnectUsingSettings(_gameVersion);
	}

	void OnConnectedToPhoton()
	{
		Debug.Log("Connected! Ready to create/join a room...");
		connectButton.setReadyToConnect(true);
	}
	
	void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		Debug.LogError("Failed to connect to Photon: " + cause);
	}

	void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{	
		updateConnectionInfoText();

		setObjectActive(connectButton, false);
		setObjectActive(roomConnectionInfo, true);
	}

	void OnJoinedRoom()
	{
		_localPlayer.setPlayerNumber(_currentRoom.playerCount);
		_localPlayer.setReadyToRace(false);

		updateConnectionInfoText();

		setObjectActive(connectButton, false);
		roomSelector.SetActive(false);
		setObjectActive(roomConnectionInfo, true);

		Debug.Log("Player " + _localPlayer.playerNumber() + " joined room: " + PhotonNetwork.room.name);
		if (_roomIsFull)
		{	
			Debug.Log("Room is full!  Loading the first level.");
			StartCoroutine(startSequenceBeforeMatch());
		}
	}
	
	private IEnumerator startSequenceBeforeMatch()
	{
		yield return new WaitForSeconds(1.5f);
		photonView.RPC("showTimeToPlayText", PhotonTargets.AllViaServer);
		
		yield return new WaitForSeconds(1f);
		photonView.RPC("loadFirstLevel", PhotonTargets.MasterClient);
	}

	private void updateConnectionInfoText()
	{
		int spotsLeft = _currentRoom.maxPlayers - _currentRoom.playerCount;
		roomConnectionInfo.updateOpenSpotsLeft(spotsLeft);
		roomConnectionInfo.updatePlayerNumber(_localPlayer.playerNumber());

		if (spotsLeft == 0)
		{
			roomConnectionInfo.hideOpenSpotsLeftText();
		}
	}

	private void hideObjectsBeforePlaying()
	{
		foreach (GameObject obj in objectsToHideBeforePlaying)
		{
			obj.SetActive(false);
		}
	}

	private void setObjectActive(MonoBehaviour script, bool hidden)
	{
		script.transform.root.gameObject.SetActive(hidden);
	}

	public void connectToRoomWithSize(int size)
	{
		if (PhotonNetwork.connectedAndReady && _requestedToJoinRoom == false)
		{
			Debug.Log("Trying to connect to a room with size: " + size + "...");

			_requestedToJoinRoom = true;
			PhotonNetwork.automaticallySyncScene = true;
			RoomOptions roomOptions = new RoomOptions() {
				maxPlayers = (byte)size,
				isVisible = false
			};
			PhotonNetwork.JoinOrCreateRoom("default" + size, roomOptions, TypedLobby.Default);
		}
	}

	void OnPhotonJoinRoomFailed(object[] codeAndMsg)
	{
		Debug.Log("Failed to join room: " + codeAndMsg[1] + "(" + codeAndMsg[0] + ")");
	}

	void OnPhotonCreateRoomFailed(object[] codeAndMsg)
	{
		Debug.Log("Failed to create room: " + codeAndMsg[1] + "(" + codeAndMsg[0] + ")");
	}

	[PunRPC] void showTimeToPlayText()
	{
		hideObjectsBeforePlaying();
		timeToPlayText.SetActive(true);
	}
	
	[PunRPC] void loadFirstLevel()
	{
		if (PhotonNetwork.isMasterClient)
		{
			if (nameOfFirstLevel != "")
			{
				PhotonNetwork.LoadLevel(nameOfFirstLevel);	
			}
		}
	}
}
