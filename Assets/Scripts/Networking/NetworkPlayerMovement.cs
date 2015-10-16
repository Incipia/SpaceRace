using UnityEngine;
using System.Collections;

public class NetworkPlayerMovement : Photon.MonoBehaviour 
{
	public Rigidbody2D rigidBody;

	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	
	private Vector3 syncLastPosition = Vector3.zero;
	private Vector3 syncTargetPosition = Vector3.zero;

	private Vector2 syncLastVelocity = Vector2.zero;
	private Vector2 syncTargetVelocity = Vector2.zero;

	private int photonSendRate = 20;
	private float interpolationProgress = 0;

	void Start()
	{
		PhotonNetwork.sendRate = photonSendRate;
		PhotonNetwork.sendRateOnSerialize = photonSendRate;

		if (!photonView.isMine)
		{
			rigidBody.isKinematic = true;
		}
	}

	void FixedUpdate()
	{
		if (!photonView.isMine)
		{
			SyncedMovement();
		}
	}

	private void SyncedMovement()
	{
		// increment = photonCallsPerSecond / fixedUpdateCallsPerSecond
		interpolationProgress += photonSendRate * Time.fixedDeltaTime;

		transform.position = Vector3.Lerp(syncLastPosition, syncTargetPosition, interpolationProgress);
//		rigidBody.velocity = Vector3.Lerp(syncLastVelocity, syncTargetVelocity, interpolationProgress);
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(rigidBody.velocity);
		}
		else if (stream.isReading && !photonView.isMine)
		{
			syncTargetPosition = (Vector3)stream.ReceiveNext();
			syncLastPosition = transform.position;

			syncTargetVelocity = (Vector2)stream.ReceiveNext();
			syncLastVelocity = rigidBody.velocity;

			interpolationProgress = 0;
		}
	}
}
