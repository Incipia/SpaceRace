using UnityEngine;

public class ConnectButton : MonoBehaviour 
{
	public NetworkRoomManager roomManager;
	public RoomSizeSelector roomSizeSelector;
	public SpriteRenderer spriteRenderer;
	public Color selectedColor;

	private Color _startColor;
	private bool _canConnect = false;
	private const float _disconnectedButtonAlpha = 0.3f;

	private bool _hasFocus;

	void Start()
	{
		_startColor = spriteRenderer.color;
		updateButtonAlphaForConnectedState(_canConnect);
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

	void OnMouseDown()
	{
		_hasFocus = true;
		lightUp();
	}

	void OnMouseUp()
	{
		_hasFocus = false;
		returnToNormalColor();
	}

	void OnMouseUpAsButton()
	{
		_hasFocus = false;
		returnToNormalColor();

		if (_canConnect)
		{
			int roomSize = roomSizeSelector.currentRoomSize;
			roomManager.connectToRoomWithSize(roomSize);
		}
	}

	void OnMouseExit()
	{
		if (_hasFocus)
		{
			returnToNormalColor();
		}
	}

	void OnMouseEnter()
	{
		if (_hasFocus)
		{
			lightUp();
		}
	}

	void lightUp()
	{
		if (_canConnect)
		{
			spriteRenderer.color = selectedColor;	
		}
	}

	void returnToNormalColor()
	{
		spriteRenderer.color = _startColor;
		updateButtonAlphaForConnectedState(_canConnect);
	}
}
