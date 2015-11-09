using UnityEngine;
using System.Collections;

public class FinishLine : Photon.MonoBehaviour
{
	public EdgeCollider2D edgeCollider;
	public GameObject finishLineText;
	public CounterUI finishLineCounter;
	public int nextLevel = 1;

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
				int playerNumber = playerPhotonView.owner.playerNumber();
				activateAndUpdateFinishLineText(playerNumber);
				photonView.RPC("activateAndUpdateFinishLineText", PhotonTargets.OthersBuffered, playerNumber);
				
				StartCoroutine(loadNextLevelAfterDuration(1));
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
			PhotonNetwork.LoadLevel(nextLevel);
		}
	}

	[PunRPC] void activateAndUpdateFinishLineText(int playerNumber)
	{
		finishLineText.SetActive(true);
		finishLineCounter.updateSpritesWithNumber(playerNumber);
	}
}
