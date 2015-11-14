using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LocalPlayerPropertiesObserver : Photon.PunBehaviour
{
	public MovePlayer movePlayer;
	private PhotonPlayer _localPlayer { get { return PhotonNetwork.player; }}

	void Start()
	{
		// There is probably a better place to do this...
		DontDestroyOnLoad(gameObject);
	}

	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;
		Hashtable props = playerAndUpdatedProps[1] as Hashtable;

		// this script is only interested in properties that should only affect the local player
		// e.g. attaching the camera
		if (player.isLocal)
		{
			foreach (string propertyKey in props.Keys)
			{
				if (propertyKey == PlayerConstants.needsToAttachCameraKey && player.needsToAttachCamera())
				{
					Camera.main.GetComponent<CameraFollow>().objectToFollow = transform;

					GameObject.Find("Left Input").GetComponent<PlayerTouchInput>().movePlayer = movePlayer;
					GameObject.Find("Right Input").GetComponent<PlayerTouchInput>().movePlayer = movePlayer;
				}
			}
		}
	}
}
