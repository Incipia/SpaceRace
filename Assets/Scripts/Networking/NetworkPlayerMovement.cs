using UnityEngine;
using System.Collections;

public class NetworkPlayerMovement : Photon.MonoBehaviour 
{
	public Rigidbody2D rigidBody;

	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	
	private Vector2 syncStartPosition = Vector2.zero;
	private Vector2 syncEndPosition = Vector2.zero;

	private Vector2 syncStartVelocity = Vector2.zero;
	private Vector2 syncEndVelocity = Vector2.zero;

	void Start()
	{
		PhotonNetwork.sendRate = 20;
		PhotonNetwork.sendRateOnSerialize = 20;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!photonView.isMine)
		{
			SyncedMovement();
		}
	}

	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;

		//		rigidBody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
//		rigidBody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, 30);
		rigidBody.velocity = Vector2.Lerp(syncStartVelocity, syncEndVelocity, 10);
//		rigidBody.velocity = Vector2.MoveTowards(syncStartVelocity, syncEndVelocity, 10);
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
//			stream.SendNext(rigidBody.position);
			stream.SendNext(rigidBody.velocity);
		}
		else
		{
//			syncEndPosition = (Vector2)stream.ReceiveNext();
//			syncStartPosition = rigidBody.position;

			syncEndVelocity = (Vector2)stream.ReceiveNext();
			syncStartVelocity = rigidBody.velocity;
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
		}
	}
}
