using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Collider2D))]
public class SpeedBoostArea : MonoBehaviour 
{
	public Vector2 forceToApply;

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
			player.addImpulse(forceToApply);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		MovePlayer movePlayer = other.gameObject.GetComponent<MovePlayer>();
		if(movePlayer != null)
		{
			playersToBoost.Add(movePlayer);
			movePlayer.temporarilyDisableMaxVelocity();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		MovePlayer movePlayer = other.gameObject.GetComponent<MovePlayer>();
		if(movePlayer != null)
		{
			playersToBoost.Remove(movePlayer);
			movePlayer.enableMaxVelocity();
		}
	}
}
