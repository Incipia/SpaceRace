using UnityEngine;

public class ButtonHighlighter : MonoBehaviour 
{
	public SpriteRenderer spriteRenderer;
	public Color selectedColor;

	private Color _startColor;

	private bool _hasFocus;

	void Start()
	{
		_startColor = spriteRenderer.color;
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
		spriteRenderer.color = selectedColor;
	}

	void returnToNormalColor()
	{
		spriteRenderer.color = _startColor;
	}
}
