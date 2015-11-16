using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkRoomManager : Photon.PunBehaviour
{
	public ConnectButton connectButton;
	public StartButton startButton;
	public NetworkRoomConnectionInfo roomConnectionInfo;
	public GameObject timeToPlayText;
	public GameObject roomSelector;
	public GameObject cupSelector;
	private string _nameOfFirstLevel;

	public List<GameObject> objectsToHideBeforePlaying = new List<GameObject>();

	private string _gameVersion = "0.0.1";
	private bool _requestedToJoinRoom;
	private bool _startedSequenceBeforeMatch;
	
	private PhotonPlayer _localPlayer { get { return PhotonNetwork.player; }}
	private Room _currentRoom { get { return PhotonNetwork.room; } }
	private bool _roomIsFull { get { return _currentRoom.maxPlayers == _currentRoom.playerCount; }}

	void Start() 
	{
		timeToPlayText.SetActive(false);
		cupSelector.SetActive(false);

		setObjectActive(startButton, false);
		setObjectActive(roomConnectionInfo, false);

		if (!PhotonNetwork.connectedAndReady)
		{
			Debug.Log("Connecting to Photon...");
			PhotonNetwork.ConnectUsingSettings(_gameVersion);
			connectButton.setReadyToConnect(false);
		}
		else
		{
			connectButton.setReadyToConnect(true);
		}
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
		_localPlayer.setTotalPoints(0);
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
			StartCoroutine(showCupSelectorForMasterClient());
		}
	}

	private IEnumerator showCupSelectorForMasterClient()
	{
		yield return new WaitForSeconds(1.5f);
		photonView.RPC("showCupSelector", PhotonTargets.MasterClient);
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

	private string firstLevelNameForCupNumber(int number)
	{
		string levelName = "";
		if (number == 1)
		{
			levelName = "L1";
		}
		else if (number == 2)
		{
			levelName = "L2";
		}
		return levelName;
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

			string roomName = "default" + size;
			if (size == 1)
			{
				System.Guid g = System.Guid.NewGuid();
			    string uniqueName = System.Convert.ToBase64String(g.ToByteArray());
			    uniqueName = uniqueName.Replace("=","");
			    roomName = uniqueName.Replace("+","");
			}
			PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
		}
	}

	public void startGameForCupNumber(int number)
	{
		if (!_startedSequenceBeforeMatch)
		{
			_startedSequenceBeforeMatch = true;
			_nameOfFirstLevel = firstLevelNameForCupNumber(number);
			StartCoroutine(startSequenceBeforeMatch());	
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

	[PunRPC] void showCupSelector()
	{
		if (PhotonNetwork.isMasterClient)
		{
			setObjectActive(roomConnectionInfo, false);
			setObjectActive(startButton, true);
			cupSelector.SetActive(true);
		}
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
			if (_nameOfFirstLevel != "")
			{
				PhotonNetwork.LoadLevel(_nameOfFirstLevel);
			}
		}
	}
}
