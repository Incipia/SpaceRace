using UnityEngine;
using AssemblyCSharp;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerPropertiesObserver : Photon.PunBehaviour
{
	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;
		Hashtable props = playerAndUpdatedProps[1] as Hashtable;

		if (photonView.isPlayer(player))
		{
			foreach (string propertyKey in props.Keys)
			{
				if (propertyKey == PlayerConstants.movementEnabledKey)
				{
					photonView.GetComponent<MovePlayer>().enabled = player.movementEnabled();
				}
				if (propertyKey == PlayerConstants.needsToResetPositionKey)
				{
					if (player.needsToResetPosition())
					{
						Vector3 startPosition = PlayerStartPositionProvider.startPositionForPlayer(player);
						photonView.GetComponent<MovePlayer>().resetToPosition(startPosition);

						player.setNeedsToResetPosition(false);
					}
				}
			}
		}
	}
}
