using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class NetworkPlayerMovement : Photon.MonoBehaviour 
{
	public Rigidbody2D playerRigidBody;
	
	private Vector3 syncLastPosition;
	private Vector3 syncTargetPosition;

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

			syncLastPosition = transform.position;
			syncTargetPosition = transform.position;
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
		else
		{
			syncTargetPosition = (Vector3)stream.ReceiveNext();
			syncLastPosition = transform.position;

			interpolationProgress = 0;
		}
	}
}
