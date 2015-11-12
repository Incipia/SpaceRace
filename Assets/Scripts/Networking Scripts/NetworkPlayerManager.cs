using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class NetworkPlayerManager : Photon.PunBehaviour
{
	public GameObject playerPrefab;

	public GameObject createPlayerAtPosition(Vector3 startPos)
	{
		return PhotonNetwork.Instantiate(playerPrefab.name, startPos, Quaternion.identity, 0);
	}
}
