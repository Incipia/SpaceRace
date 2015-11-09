using UnityEngine;
using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;

public class PlayerColorSetup : Photon.MonoBehaviour 
{
	public SpriteRenderer outerRingRenderer;
	public SpriteRenderer innerCircleRenderer;
	public ParticleSystem particleTrail;

	private PhotonPlayer _localPlayer { get { return PhotonNetwork.player; }}

	void Awake() 
	{
		Vector3 outerRingColorVector = PlayerColorProvider.colorVectorForPlayer(_localPlayer, PlayerColoredComponentType.OuterRing);
		Vector3 innerCircleColorVector = PlayerColorProvider.colorVectorForPlayer(_localPlayer, PlayerColoredComponentType.InnerCircle);
		Vector3 particleTrailColorVector = PlayerColorProvider.colorVectorForPlayer(_localPlayer, PlayerColoredComponentType.ParticleTrail);

		updateColors(outerRingColorVector, innerCircleColorVector, particleTrailColorVector);
	}

	[PunRPC] void updateColors(Vector3 outerRingColorVector, Vector3 innerCircleColorVector, Vector3 particleTrailColorVector)
	{
		updateOuterRingColorWithVector(outerRingColorVector);
		updateInnerCircleColorWithVector(innerCircleColorVector);
		updateParticleTrailColorWithVector(particleTrailColorVector);

		if (photonView.isMine)
		{
			photonView.RPC("updateColors", PhotonTargets.OthersBuffered, outerRingColorVector, innerCircleColorVector, particleTrailColorVector);
		}
	}
	
	private void updateOuterRingColorWithVector(Vector3 colorVector)
	{
		float r = colorVector.x;
		float g = colorVector.y;
		float b = colorVector.z;

		outerRingRenderer.color = new Color(r, g, b);
	}

	private void updateInnerCircleColorWithVector(Vector3 colorVector)
	{
		float r = colorVector.x;
		float g = colorVector.y;
		float b = colorVector.z;

		innerCircleRenderer.color = new Color(r, g, b);
	}
	
	private void updateParticleTrailColorWithVector(Vector3 colorVector)
	{
		float r = colorVector.x;
		float g = colorVector.y;
		float b = colorVector.z;

		particleTrail.startColor = new Color(r, g, b);
	}
}
