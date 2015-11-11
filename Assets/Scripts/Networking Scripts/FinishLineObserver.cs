using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class FinishLineObserver : Photon.PunBehaviour 
{
	public delegate void AllPlayersFinishedHandler();
	public AllPlayersFinishedHandler allPlayersFinishedHandler;

	private bool _shouldObserveCrossedFinishLine = true;
	private bool _allPlayersCrossedFinishLine { get { return PhotonNetwork.room.allPlayersCrossedFinishLine(); }}

	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		if (PhotonNetwork.isMasterClient)
		{
			PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;
			Hashtable props = playerAndUpdatedProps[1] as Hashtable;
			
			if (props.ContainsKey(PlayerConstants.crossedFinishLineKey) && _allPlayersCrossedFinishLine)
			{
				_shouldObserveCrossedFinishLine = false;
				allPlayersFinishedHandler();
			}
		}
	}
}
