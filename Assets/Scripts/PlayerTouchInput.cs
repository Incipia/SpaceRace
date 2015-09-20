using UnityEngine;
using System.Collections;

public class PlayerTouchInput : MonoBehaviour 
{
	public GameObject player;
	public TouchInputSide side;
	public Collider2D touchArea;

	private MovePlayer movePlayer;

	// Use this for initialization
	void Start () 
	{
		getMovePlayerScript();
	}

	void Update()
	{
		foreach(Touch touch in Input.touches)
		{
			if(touch.phase == TouchPhase.Began)
			{
				Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
				Vector2 touchPosition = new Vector2(worldPoint.x, worldPoint.y);
				Collider2D[] touchingColliders = Physics2D.OverlapPointAll(touchPosition);
				foreach(Collider2D touchingCollider in touchingColliders)
				{
					if(touchingCollider == touchArea)
					{
						movePlayer.jumpWithTouchInputSide(side);
					}
				}
			}
		}
	}

	private void getMovePlayerScript()
	{
		movePlayer = player.GetComponent<MovePlayer>();
		if (!movePlayer)
		{
			Debug.Log("move player script not found");
		}
	}
}
