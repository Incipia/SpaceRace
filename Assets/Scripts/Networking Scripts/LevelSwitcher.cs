using UnityEngine;
using System.Collections;

public class LevelSwitcher : Photon.PunBehaviour 
{	
	public FinishLineObserver finishLineObserver;
	public string nextLevelName;
	private bool _isLoadingLevel = false;

	void Start()
	{
		PhotonNetwork.automaticallySyncScene = true;
		finishLineObserver.allPlayersFinishedHandler = loadNextLevel;
	}

	private void loadNextLevel()
	{
		if (!_isLoadingLevel)
		{
			photonView.RPC("blockFromLoadingLevel", PhotonTargets.All);
			StartCoroutine(loadAfterDuration(1));
		}
	}

	private IEnumerator loadAfterDuration(float duration)
	{
		Debug.Log("loading next level after " + duration + " seconds...");
		yield return new WaitForSeconds(duration);
		photonView.RPC("actuallyLoadNextLevel", PhotonTargets.MasterClient);
	}

	[PunRPC] void blockFromLoadingLevel()
	{
		Debug.Log("blocking others from calling loadNextLevel()");
		_isLoadingLevel = true;
	}
	
	[PunRPC] void actuallyLoadNextLevel()
	{
		// double check isMasterClient
		if (PhotonNetwork.isMasterClient)
		{
			if (nextLevelName != "")
			{
				Debug.Log ("loading level: " + nextLevelName);
				PhotonNetwork.LoadLevel(nextLevelName);
         	}
      	}
   	}
}
