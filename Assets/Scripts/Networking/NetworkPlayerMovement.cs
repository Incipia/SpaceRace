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

	private int photonSendRate = 20;
	private float positionInterpolationProgress = 0;

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
		float fixedUpdateCallsPerSecond = 1 / Time.fixedDeltaTime;
		float photonCallsPerSecond = photonSendRate;

		// increment * fixedUpdateCallsPerSecond = photonCallsPerSecond
		// increment = photonCallsPerSecond / fixedUpdateCallsPerSecond

		float increment = photonCallsPerSecond / fixedUpdateCallsPerSecond;
		positionInterpolationProgress += increment;

		transform.position = Vector3.Lerp(syncLastPosition, syncTargetPosition, positionInterpolationProgress);
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(transform.position);
		}
		else if (stream.isReading && !photonView.isMine)
		{
			syncTargetPosition = (Vector3)stream.ReceiveNext();
			syncLastPosition = transform.position;

			positionInterpolationProgress = 0;
		}
	}
}
