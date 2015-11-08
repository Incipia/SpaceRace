using UnityEngine;
using System.Collections;

public class PlayerTouchInput : MonoBehaviour 
{
	public TouchInputSide side;
	public Collider2D touchArea;	
	public MovePlayer movePlayer;

	private JumpDirection jumpDirection;

	void Start()
	{
		jumpDirection = jumpDirectionForTouchInputSide(side);
	}
	
	void Update()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touch.position);
				Vector2 touchPosition = new Vector2(worldPosition.x, worldPosition.y);
				Collider2D[] touchingColliders = Physics2D.OverlapPointAll(touchPosition);

				foreach (Collider2D touchingCollider in touchingColliders)
				{
					if (touchingCollider == touchArea && movePlayer != null)
					{
						movePlayer.jumpWithDirection(jumpDirection);
						break;
					}
				}
			}
		}
	}
	
	private JumpDirection jumpDirectionForTouchInputSide(TouchInputSide side)
	{
		JumpDirection direction = JumpDirection.Invalid;
		switch (side)
		{
		case TouchInputSide.Left:
			direction = JumpDirection.Left;
			break;
		case TouchInputSide.Right:
			direction = JumpDirection.Right;
			break;
		}
		return direction;
	}
}
