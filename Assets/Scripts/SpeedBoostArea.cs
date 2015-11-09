using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Collider2D))]
public class SpeedBoostArea : MonoBehaviour 
{
	public Vector2 forceToApply;
	public Vector2 newPlayerMaxVelocity = new Vector2(1, 10);

	private List<MovePlayer> playersToBoost;

	// Use this for initialization
	private void Start() 
	{
		playersToBoost = new List<MovePlayer>();
	}
	
	// Update is called once per frame
	private void FixedUpdate() 
	{
		foreach(MovePlayer player in playersToBoost)
		{
			player.SetMaxVelocity(newPlayerMaxVelocity);
			player.addImpulse(forceToApply);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		MovePlayer movePlayer = other.transform.root.gameObject.GetComponent<MovePlayer>();
		if(movePlayer != null)
		{
			playersToBoost.Add(movePlayer);
		}
		else
		{
			Debug.Log("move player not found!");
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		MovePlayer movePlayer = other.transform.root.gameObject.GetComponent<MovePlayer>();
		if(movePlayer != null)
		{
			playersToBoost.Remove(movePlayer);
		}
	}
}
