using UnityEngine;
using System.Collections;

public class NetworkPlayerMovement : Photon.MonoBehaviour 
{
	public Rigidbody2D playerRigidBody;
	
	private Vector3 syncLastPosition = Vector3.zero;
	private Vector3 syncTargetPosition = Vector3.zero;

	private int photonSendRate = 15;
	private int photonSendRateOnSerialize = 15;

	private float interpolationProgress = 0;

	void Start()
	{
		PhotonNetwork.sendRate = photonSendRate;
		PhotonNetwork.sendRateOnSerialize = photonSendRateOnSerialize;

		if (!photonView.isMine)
		{
			playerRigidBody.isKinematic = true;
			playerRigidBody.interpolation = RigidbodyInterpolation2D.None;
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
		interpolationProgress += photonSendRateOnSerialize * Time.fixedDeltaTime;
		transform.position = Vector3.Lerp(syncLastPosition, syncTargetPosition, interpolationProgress);
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

			interpolationProgress = 0;
		}
	}
}
