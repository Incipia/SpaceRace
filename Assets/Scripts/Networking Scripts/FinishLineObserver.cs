using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class FinishLineObserver : Photon.MonoBehaviour
{
	public delegate void AllPlayersFinishedHandler();
	public AllPlayersFinishedHandler allPlayersFinishedHandler;

	private bool _allPlayersCrossedFinishLine { get { return PhotonNetwork.room.allPlayersCrossedFinishLine(); }}
    private int _numberOfPlayersCrossed;

    void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		// We only want the master client to observe the finish line related properties -- if each
		// client was observing properties that changed based on then finish line, and actually
		// doing things based on those changes, then things could get out of hand!
		if (PhotonNetwork.isMasterClient)
		{
			Hashtable props = playerAndUpdatedProps[1] as Hashtable;
			if (props.ContainsKey(PlayerConstants.crossedFinishLineKey))
			{
				PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;
				if (player.crossedFinishLine())
				{
					int placement = PhotonNetwork.room.totalPlayersThatCrossedFinishLine();
					int score = scoreForPlacement(placement);
					player.incrementTotalPoints(score);
				}

				if (_allPlayersCrossedFinishLine)
				{
					allPlayersFinishedHandler();
				}
			}
		}
	}

	private int scoreForPlacement(int placement)
	{
        int score = 0;
        if (placement == 1)
		{
            score = 3;
        }
		else if (placement == 2)
		{
            score = 2;
        }
		else if (placement == 3)
		{
            score = 1;
        }
        return score;
    }
}
