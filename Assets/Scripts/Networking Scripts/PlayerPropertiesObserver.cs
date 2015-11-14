using UnityEngine;
using AssemblyCSharp;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerPropertiesObserver : Photon.MonoBehaviour
{
	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;
		Hashtable props = playerAndUpdatedProps[1] as Hashtable;

		// check to see if THIS photon view corresponds to the player before using it
		if (photonView.attachedToPlayer(player))
		{
			foreach (string propertyKey in props.Keys)
			{
				if (propertyKey == PlayerConstants.movementEnabledKey)
				{
					photonView.GetComponent<MovePlayer>().enabled = player.movementEnabled();
				}
				if (propertyKey == PlayerConstants.totalPointsKey)
				{
                    int score = player.totalPoints();
                    photonView.GetComponent<PlayerPointsDisplay>().updateScore(score);
                }
				if (propertyKey == PlayerConstants.updatePositionKey)
				{
                    object updatedPosition;
					if (props.TryGetValue(propertyKey, out updatedPosition))
					{
						Vector3 pos = (Vector3)updatedPosition;
                        photonView.GetComponent<MovePlayer>().resetToPosition(pos);
					}
                }
				if (propertyKey == PlayerConstants.isKinematic)
				{
                    photonView.GetComponent<Rigidbody2D>().isKinematic = player.isKinematic();
                }
				if (propertyKey == PlayerConstants.updateScaleKey)
				{
                    object updatedScale;
					if (props.TryGetValue(propertyKey, out updatedScale))
					{
						Vector3 newScale = (Vector3)updatedScale;
                        photonView.transform.root.gameObject.transform.localScale = newScale;
					}
				}
				if (propertyKey == PlayerConstants.particlesEnabledKey)
				{
					bool visible = player.particlesEnabled();
					photonView.GetComponent<NetworkPlayerVisibilitySetup>().makeParticleTrailVisible(visible);
				}
			}
		}
	}
}
