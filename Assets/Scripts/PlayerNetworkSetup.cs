using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour
{
	private UnityStandardAssets._2D.Camera2DFollow cameraFollow;
	private EZParallax parallax;
	private PlayerKeyboardInput playerKeyboardInput;

	// Use this for initialization
	void Start() 
	{
		if (isLocalPlayer)
		{
			if (getCameraFollowScript())
			{
				cameraFollow.assignTarget(gameObject);
			}
			
			if (getParallaxScript())
			{
				parallax.AssignPlayer(gameObject, true);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
	}

	private bool getCameraFollowScript()
	{
		cameraFollow = GameObject.Find("Main Camera").GetComponent<UnityStandardAssets._2D.Camera2DFollow>();
		if (cameraFollow == null)
		{
			Debug.Log("Could not find camera follow script");
		}

		return cameraFollow != null;
	}

	private bool getParallaxScript()
	{
		parallax = GameObject.Find("Parallax").GetComponent<EZParallax>();
		if (parallax == null)
		{
			Debug.Log("Could not find EZParallax script");
		}

		return parallax != null;
	}
	
	private bool getPlayerKeyboardInputScript()
	{
		playerKeyboardInput = GetComponent<PlayerKeyboardInput>();
		if (playerKeyboardInput == null)
		{
			Debug.Log("Could not find PlayerKeyboardInput script");
		}
		
		return playerKeyboardInput != null;
	}
}
