using UnityEngine;
using System.Collections;

public class ConnectButton : MonoBehaviour 
{
	public NetworkRoomManager roomManager;
	public RoomSizeSelector roomSizeSelector;
	public SpriteRenderer spriteRenderer;

	private bool _canConnect = false;
	private const float _disconnectedButtonAlpha = 0.5f;

	void Start()
	{
		updateButtonAlphaForConnectedState(_canConnect);
	}

	void OnMouseDown()
	{
		if (_canConnect)
		{
			int roomSize = roomSizeSelector.currentRoomSize;
			roomManager.connectToRoomWithSize(roomSize);
		}
	}

	private void updateButtonAlphaForConnectedState(bool canConnect)
	{
		Color color = new Color(1, 1, 1, canConnect ? 1 : _disconnectedButtonAlpha);
		spriteRenderer.color = color;
	}

	public void setReadyToConnect(bool canConnect)
	{
		_canConnect = canConnect;
		updateButtonAlphaForConnectedState(canConnect);
	}
}
