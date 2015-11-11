using UnityEngine;
using System.Collections;

public class ConnectButton : MonoBehaviour 
{
	public NetworkRoomManager roomManager;
	public RoomSizeSelector roomSizeSelector;
	public SpriteRenderer spriteRenderer;

	private Color _startColor;
	private bool _canConnect = false;
	private const float _disconnectedButtonAlpha = 0.3f;

	void Start()
	{
		_startColor = spriteRenderer.color;
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
		float r = _startColor.r;
		float g = _startColor.g;
		float b = _startColor.b;

		Color color = new Color(r, g, b, canConnect ? 1 : _disconnectedButtonAlpha);
		spriteRenderer.color = color;
	}

	public void setReadyToConnect(bool canConnect)
	{
		_canConnect = canConnect;
		updateButtonAlphaForConnectedState(canConnect);
	}
}
