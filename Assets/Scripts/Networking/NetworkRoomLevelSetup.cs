using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkRoomLevelSetup : Photon.PunBehaviour
{
	public GameObject topLevelComponent;
	private List<PlatformOscillation> platformOscillationScripts = new List<PlatformOscillation>();
	private List<PlatformRotator> platformRotatorScripts = new List<PlatformRotator>();

	void Start()
	{
		getPlatformRotatorAndOscillationScripts();
	}

	private void getPlatformRotatorAndOscillationScripts()
	{
		foreach (PlatformOscillation oscillation in topLevelComponent.transform.GetComponentsInChildren<PlatformOscillation>(true))
		{
			platformOscillationScripts.Add(oscillation);
		}
		foreach (PlatformRotator rotator in topLevelComponent.transform.GetComponentsInChildren<PlatformRotator>(true))
		{
			platformRotatorScripts.Add(rotator);
		}
	}
	
	private void setPlatformOscillationAndRotatorsEnabled(bool enabled)
	{
		foreach (PlatformOscillation oscillation in platformOscillationScripts)
		{
			oscillation.enabled = enabled;
		}
		foreach (PlatformRotator rotator in platformRotatorScripts)
		{
			rotator.enabled = enabled;
		}
	}

	public void deactivateMovingLevelComponents()
	{
		setPlatformOscillationAndRotatorsEnabled(false);
	}

	public void activateMovingLevelComponents()
	{
		setPlatformOscillationAndRotatorsEnabled(true);
	}
}
