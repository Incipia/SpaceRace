using UnityEngine;
using System.Collections;

public class ResultsScreenSetup : MonoBehaviour
{
    public GameObject firstPlaceGameObject;
    public GameObject secondPlaceGameObject;
    public GameObject thirdPlaceGameObject;
    public GameObject fourthPlaceGameObject;

	private PhotonPlayer _localPlayer { get { return PhotonNetwork.player; }}
	private int[] _scoresArray;

    void Start()
	{
		if (PhotonNetwork.connectedAndReady)
		{
			setupScoresArray();

			int placement = placementForPlayer(_localPlayer);
			GameObject placementObject = placeObjectForPlacement(placement);

			Vector3 position = placementObject.transform.position;
			
			_localPlayer.setParticlesEnabled(false);
            _localPlayer.setKinematic(true);
            _localPlayer.updatePosition(position);

			Vector3 newScale = placementObject.transform.localScale;
			_localPlayer.updateScale(newScale);
        }
	}

	private void setupScoresArray()
	{
		_scoresArray = new int[PhotonNetwork.playerList.Length];

		int scoreIndex = 0;
		foreach (PhotonPlayer player in PhotonNetwork.playerList)
		{
			_scoresArray[scoreIndex] = player.totalPoints();
			++scoreIndex;
		}
	}

	private int placementForPlayer(PhotonPlayer player)
	{
		int placement = 1;
		foreach (int score in _scoresArray)
		{
			if (player.totalPoints() < score)
			{
				++placement;
			}
		}
		return placement;
	}

	private GameObject placeObjectForPlacement(int placement)
	{
		GameObject placeObject = fourthPlaceGameObject;
		if (placement == 1)
		{
			placeObject = firstPlaceGameObject;
		}
		else if (placement == 2)
		{
			placeObject = secondPlaceGameObject;
		}
		else if (placement == 3)
		{
			placeObject = thirdPlaceGameObject;
		}
		return placeObject;
	}
}
