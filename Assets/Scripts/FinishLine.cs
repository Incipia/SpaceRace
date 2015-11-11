using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class FinishLine : Photon.MonoBehaviour
{
	public EdgeCollider2D edgeCollider;
	public GameObject finishLineText;
	public CounterUI finishLineCounter;
	public string nameOfNextLevel;

	private PhotonPlayer _localPlayer { get { return PhotonNetwork.player; }}
	private bool _someoneCrossedFinishLine = false;

	void Start()
	{
		finishLineText.SetActive(false);
		PhotonNetwork.automaticallySyncScene = true;
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		float edgeTransformY = edgeCollider.transform.position.y + edgeCollider.offset.y;
		if (other.transform.position.y > edgeTransformY)
		{
			PhotonView playerPhotonView = PhotonView.Get(other.transform.root.gameObject);
			if (playerPhotonView != null)
			{
				playerPhotonView.owner.setCrossedFinishLine(true);
				if (_someoneCrossedFinishLine == false)
				{
					int playerNumber = playerPhotonView.owner.playerNumber();
					activateAndUpdateFinishLineText(playerNumber);
					photonView.RPC("activateAndUpdateFinishLineText", PhotonTargets.OthersBuffered, playerNumber);
					StartCoroutine(loadNextLevelAfterDuration(2));
				}
			}
		}
	}

	private IEnumerator loadNextLevelAfterDuration(float duration)
	{
		yield return new WaitForSeconds(duration);
		photonView.RPC("loadNextLevel", PhotonTargets.MasterClient);
	}

	[PunRPC] void loadNextLevel()
	{
		if (PhotonNetwork.isMasterClient)
		{
			if (nameOfNextLevel != null)
			{
				PhotonNetwork.LoadLevel(nameOfNextLevel);
			}
		}
	}

	[PunRPC] void activateAndUpdateFinishLineText(int playerNumber)
	{
		_someoneCrossedFinishLine = true;
		finishLineText.SetActive(true);
		finishLineCounter.updateSpritesWithNumber(playerNumber);
	}

//	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
//	{
//		PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;
//		Hashtable props = playerAndUpdatedProps[1] as Hashtable;
//
//		if (props.ContainsKey(PlayerPropertiesManager.crossedFinishLineKey))
//		{
//			if (PhotonNetwork.room.allPlayersCrossedFinishLine())
//			{
//				StartCoroutine(loadNextLevelAfterDuration(1));
//			}
//		}
//	}
}
