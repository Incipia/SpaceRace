using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class NetworkPlayerColorSetup : Photon.MonoBehaviour 
{
	public SpriteRenderer outerRingRenderer;
	public SpriteRenderer innerCircleRenderer;
	public ParticleSystem particleTrail;

	void Start() 
	{
		if (photonView.isMine)
		{
			int playerNumber = PhotonNetwork.room.playerCount;

			Color outerRingColor = PlayerColorProvider.colorForPlayerNumber(playerNumber, PlayerColoredComponentType.OuterRing);
			Vector3 outerRingColorVector = new Vector3(outerRingColor.r, outerRingColor.g, outerRingColor.b);
			updateOuterRingColorWithVector(outerRingColorVector);
			
			
			Color innerCircleColor = PlayerColorProvider.colorForPlayerNumber(playerNumber, PlayerColoredComponentType.InnerCircle);
			Vector3 innerCircleColorVector = new Vector3(innerCircleColor.r, innerCircleColor.g, innerCircleColor.b);
			updateInnerCircleColorWithVector(innerCircleColorVector);
			
			
			Color particleTrailColor = PlayerColorProvider.colorForPlayerNumber(playerNumber, PlayerColoredComponentType.ParticleTrail);
			Vector3 particleTrailColorVector = new Vector3(particleTrailColor.r, particleTrailColor.g, particleTrailColor.b);
			updateParticleTrailColorWithVector(particleTrailColorVector);
		}
	}
	
	[PunRPC] void updateOuterRingColorWithVector(Vector3 colorVector)
	{
		float r = colorVector.x;
		float g = colorVector.y;
		float b = colorVector.z;

		outerRingRenderer.color = new Color(r, g, b);
		if (photonView.isMine)
		{
			photonView.RPC("updateOuterRingColorWithVector", PhotonTargets.OthersBuffered, colorVector);
		}
	}

	[PunRPC] void updateInnerCircleColorWithVector(Vector3 colorVector)
	{
		float r = colorVector.x;
		float g = colorVector.y;
		float b = colorVector.z;

		innerCircleRenderer.color = new Color(r, g, b);
		if (photonView.isMine)
		{
			photonView.RPC("updateInnerCircleColorWithVector", PhotonTargets.OthersBuffered, colorVector);
		}
	}
	
	[PunRPC] void updateParticleTrailColorWithVector(Vector3 colorVector)
	{
		float r = colorVector.x;
		float g = colorVector.y;
		float b = colorVector.z;

		particleTrail.startColor = new Color(r, g, b);
		if (photonView.isMine)
		{
			photonView.RPC("updateParticleTrailColorWithVector", PhotonTargets.OthersBuffered, colorVector);
		}
	}
}
