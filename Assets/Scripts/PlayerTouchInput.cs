using UnityEngine;
using System.Collections;

public class PlayerTouchInput : MonoBehaviour 
{
	public GameObject player;
	public TouchInputSide side;

	private MovePlayer movePlayer;

	// Use this for initialization
	void Start () 
	{
		getMovePlayerScript();
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
