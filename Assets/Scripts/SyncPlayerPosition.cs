using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SyncPlayerPosition : NetworkBehaviour 
{
	[SyncVar]
	private Vector3 syncPosition;

	public Transform playerTransform;
	public float lerpRate = 15f;

	void Update()
	{
		transmitPosition();
		lerpPosition();
	}

	void lerpPosition()
	{
		if (isLocalPlayer)
		{
			playerTransform.position = Vector3.Lerp(playerTransform.position, syncPosition, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdProvidePositionToServer(Vector3 position)
	{
		syncPosition = position;
	}

	[ClientCallback]
	void transmitPosition()
	{
		if (isLocalPlayer)
		{
			CmdProvidePositionToServer(playerTransform.position);
		}
	}
}
