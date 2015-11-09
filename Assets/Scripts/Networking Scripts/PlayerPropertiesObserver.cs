using UnityEngine;
using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerPropertiesObserver : Photon.PunBehaviour
{
	public MovePlayer movePlayer;

	void Start()
	{
		// This probably isn't the best place to do this, but for now it's OK
		DontDestroyOnLoad(gameObject);
	}

	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;
		Hashtable props = playerAndUpdatedProps[1] as Hashtable;

		if (player == photonView.owner)
		{
			foreach (string propertyKey in props.Keys)
			{
				if (propertyKey == PlayerPropertiesManager.needsToAttachCameraKey)
				{
					if (photonView.isMine && player.needsToAttachCamera())
					{
						Camera.main.GetComponent<CameraFollow>().objectToFollow = transform;
					}
				}
				if (propertyKey == PlayerPropertiesManager.movementEnabledKey)
				{
					if (photonView.isMine)
					{
						movePlayer.enabled = player.movementEnabled();
					}
				}
				if (propertyKey == PlayerPropertiesManager.needsToResetPositionKey)
				{
					if (player.needsToResetPosition())
					{
						Vector3 startPosition = PlayerStartPositionProvider.startPositionForPlayer(player);
						movePlayer.resetToPosition(startPosition);

						player.setNeedsToResetPosition(false);
					}
				}
			}
		}
	}
}
