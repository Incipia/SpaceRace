using UnityEngine;
using System.Collections;

public class PlayerPointsDisplay : Photon.MonoBehaviour
{
    public CounterUI scoreDisplay;

    private PhotonPlayer _localPlayer { get { return PhotonNetwork.player; }}

	[PunRPC] void updatePlayerDisplayScore(int displayScore)
	{
        scoreDisplay.updateSpritesWithNumber(displayScore);
        if (photonView.isMine)
        {
            photonView.RPC("updatePlayerDisplayScore", PhotonTargets.OthersBuffered, displayScore);
        }
    }

    public void updateScore(int score)
    {
        updatePlayerDisplayScore(score);
    }
}
