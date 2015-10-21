using UnityEngine;
using System.Collections;

public class NetworkPlayerColorSetup : Photon.MonoBehaviour 
{
	public SpriteRenderer outerRingRenderer;
	public SpriteRenderer innerCircleRenderer;

	void Start() 
	{
		if (photonView.isMine)
		{
			Vector3 randomColorVector = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			updateColorWithVector(randomColorVector);
		}
	}

	[PunRPC] void updateColorWithVector(Vector3 colorVector)
	{
		float r = colorVector.x;
		float g = colorVector.y;
		float b = colorVector.z;

		Color innerCircleColor = new Color(r, g, b, 1);
		Color outerRingColor = new Color(r, g, b, .8f);

		innerCircleRenderer.color = innerCircleColor;
		outerRingRenderer.color = outerRingColor;

		if (photonView.isMine)
		{
			photonView.RPC ("updateColorWithVector", PhotonTargets.OthersBuffered, colorVector);
		}
	}
}
