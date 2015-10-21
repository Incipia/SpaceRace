using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class NetworkPlayerColorSetup : Photon.MonoBehaviour 
{
	public SpriteRenderer outerRingRenderer;
	public SpriteRenderer innerCircleRenderer;
	public ParticleSystem particleSystem;

	void Start() 
	{
		if (photonView.isMine)
		{
			int playerNumber = photonView.CreatorActorNr;

			Color outerRingColor = PlayerColorProvider.colorForPlayerNumber(playerNumber, PlayerColoredComponentType.OuterRing);
			Vector3 outerRingColorVector = new Vector3(outerRingColor.r, outerRingColor.g, outerRingColor.b);
			updateOuterRingColorWithVector(outerRingColorVector);
			
			
			Color innerCircleColor = PlayerColorProvider.colorForPlayerNumber(playerNumber, PlayerColoredComponentType.InnerCircle);
			Vector3 innerCircleColorVector = new Vector3(innerCircleColor.r, innerCircleColor.g, innerCircleColor.b);
			updateInnerCircleColorWithVector(innerCircleColorVector);
			
			
			Color particleSystemColor = PlayerColorProvider.colorForPlayerNumber(playerNumber, PlayerColoredComponentType.ParticleTrail);
			Vector3 particleSystemColorVector = new Vector3(particleSystemColor.r, particleSystemColor.g, particleSystemColor.b);
			updateParticleSystemColorWithVector(particleSystemColorVector);
		}
	}
	
	[PunRPC] void updateOuterRingColorWithVector(Vector3 colorVector)
	{
		float r = colorVector.x;
		float g = colorVector.y;
		float b = colorVector.z;
		
		Color innerCircleColor = new Color(r, g, b);
		
		outerRingRenderer.color = innerCircleColor;
		
		if (photonView.isMine)
		{
			photonView.RPC ("updateOuterRingColorWithVector", PhotonTargets.OthersBuffered, colorVector);
		}
	}

	[PunRPC] void updateInnerCircleColorWithVector(Vector3 colorVector)
	{
		float r = colorVector.x;
		float g = colorVector.y;
		float b = colorVector.z;

		Color innerCircleColor = new Color(r, g, b);

		innerCircleRenderer.color = innerCircleColor;

		if (photonView.isMine)
		{
			photonView.RPC ("updateInnerCircleColorWithVector", PhotonTargets.OthersBuffered, colorVector);
		}
	}
	
	[PunRPC] void updateParticleSystemColorWithVector(Vector3 colorVector)
	{
		float r = colorVector.x;
		float g = colorVector.y;
		float b = colorVector.z;
		
		Color innerCircleColor = new Color(r, g, b);
		
		particleSystem.startColor = innerCircleColor;
		
		if (photonView.isMine)
		{
			photonView.RPC ("updateParticleSystemColorWithVector", PhotonTargets.OthersBuffered, colorVector);
		}
	}
}
