using UnityEngine;
using System.Collections;

public class ResultsScreenSetup : MonoBehaviour
{
    public GameObject firstPlaceGameObject;
    public GameObject secondPlaceGameObject;
    public GameObject thirdPlaceGameObject;
    public GameObject fourthPlaceGameObject;

	private PhotonPlayer _localPlayer { get { return PhotonNetwork.player; }}

    void Start()
	{
		if (PhotonNetwork.connectedAndReady)
		{
            Vector3 position = firstPlaceGameObject.transform.position;
            _localPlayer.updatePosition(position);
        }
	}
}
