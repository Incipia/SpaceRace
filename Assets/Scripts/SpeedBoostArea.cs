using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Collider2D))]
public class SpeedBoostArea : MonoBehaviour 
{
	public Vector2 forceToApply;

	private List<MovePlayer> playersToBoost;
	private List<MovePlayerPhoton> photonPlayersToBoost;

	// Use this for initialization
	private void Start() 
	{
		playersToBoost = new List<MovePlayer>();
		photonPlayersToBoost = new List<MovePlayerPhoton>();
	}
	
	// Update is called once per frame
	private void FixedUpdate() 
	{
		foreach(MovePlayer player in playersToBoost)
		{
			player.addImpulse(forceToApply);
		}

		foreach(MovePlayerPhoton player in photonPlayersToBoost)
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
			movePlayer.disableMaxVelocity();
		}

		MovePlayerPhoton movePlayerPhoton = other.gameObject.transform.root.GetComponentInChildren<MovePlayerPhoton>();
		if(movePlayerPhoton != null)
		{
			photonPlayersToBoost.Add(movePlayerPhoton);
			movePlayerPhoton.disableMaxVelocity();
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

		MovePlayerPhoton movePlayerPhoton = other.gameObject.transform.root.GetComponentInChildren<MovePlayerPhoton>();
		if(movePlayerPhoton != null)
		{
			photonPlayersToBoost.Remove(movePlayerPhoton);
			movePlayerPhoton.enableMaxVelocity();
		}
	}
}
