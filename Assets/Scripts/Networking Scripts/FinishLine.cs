using UnityEngine;

public class FinishLine : Photon.MonoBehaviour
{
	public GameObject finishLineText;
	public CounterUI finishLineCounter;
	public EdgeCollider2D edgeCollider;
	private bool _someoneCrossedFinishLine;

	void Start()
	{
		finishLineText.SetActive(false);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		float edgeTransformY = edgeCollider.transform.position.y + edgeCollider.offset.y;
		if (other.transform.position.y > edgeTransformY)
		{
			PhotonView playerPhotonView = PhotonView.Get(other.transform.root.gameObject);
			if (playerPhotonView != null && playerPhotonView.isLocal())
			{
				playerPhotonView.setCrossedFinishLine(true);
                if (!_someoneCrossedFinishLine)
				{
					int playerNumber = playerPhotonView.playerNumber();
					photonView.RPC("activateAndUpdateFinishLineText", PhotonTargets.All, playerNumber);
				}
			}
		}
	}

	[PunRPC] void activateAndUpdateFinishLineText(int playerNumber)
	{
		_someoneCrossedFinishLine = true;
		finishLineCounter.updateSpritesWithNumber(playerNumber);
		finishLineText.SetActive(true);
	}
}
