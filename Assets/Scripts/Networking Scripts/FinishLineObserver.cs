using Hashtable = ExitGames.Client.Photon.Hashtable;

public class FinishLineObserver : Photon.PunBehaviour
{
	public delegate void AllPlayersFinishedHandler();
	public AllPlayersFinishedHandler allPlayersFinishedHandler;
	private bool _allPlayersCrossedFinishLine { get { return PhotonNetwork.room.allPlayersCrossedFinishLine(); }}

	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		if (PhotonNetwork.isMasterClient)
		{
			Hashtable props = playerAndUpdatedProps[1] as Hashtable;
			if (props.ContainsKey(PlayerConstants.crossedFinishLineKey) && _allPlayersCrossedFinishLine)
			{
				allPlayersFinishedHandler();
			}
		}
	}
}
