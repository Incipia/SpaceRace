using UnityEngine;
using System.Collections;

public class NetworkPlayerVisibilitySetup : Photon.MonoBehaviour 
{
	public SpriteRenderer outerRingRenderer;
	public SpriteRenderer innerCircleRenderer;
	public SpriteRenderer playerNumberSpriteRenderer;
	public ParticleSystem particleTrail;

	// Use this for initialization
	void Start() 
	{
		if (photonView.isMine)
		{
			makeEverythingVisible(false);
		}
	}

	public void makeEverythingVisible(bool visible)
	{
		makeOuterRingVisible(visible);
		makeInnerCircleVisible(visible);
		makePlayerNumberVisible(visible);
		makeParticleTrailVisible(visible);
	}
	
	[PunRPC] void makeOuterRingVisible(bool visible)
	{
		outerRingRenderer.enabled = visible;
		if (photonView.isMine)
		{
			photonView.RPC("makeOuterRingVisible", PhotonTargets.OthersBuffered, visible);
		}
	}
	
	[PunRPC] void makeInnerCircleVisible(bool visible)
	{
		innerCircleRenderer.enabled = visible;
		if (photonView.isMine)
		{
			photonView.RPC("makeInnerCircleVisible", PhotonTargets.OthersBuffered, visible);
		}
	}
	
	[PunRPC] void makePlayerNumberVisible(bool visible)
	{
		playerNumberSpriteRenderer.enabled = visible;
		if (photonView.isMine)
		{
			photonView.RPC("makePlayerNumberVisible", PhotonTargets.OthersBuffered, visible);
		}
	}
	
	[PunRPC] void makeParticleTrailVisible(bool visible)
	{
		particleTrail.GetComponent<ParticleSystem>().enableEmission = visible;
		if (photonView.isMine)
		{
			photonView.RPC("makeParticleTrailVisible", PhotonTargets.OthersBuffered, visible);
		}
	}
}
