using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelComponentsManager : MonoBehaviour
{
	public GameObject topLevelComponent;
	private List<PlatformOscillation> platformOscillationScripts = new List<PlatformOscillation>();
	private List<PlatformRotator> platformRotatorScripts = new List<PlatformRotator>();
	private List<ObjectScaleOscillation> objectScaleScripts = new List<ObjectScaleOscillation>();

	void Awake()
	{
		getObjectMovementScripts();
		deactivateMovingLevelComponents();
	}

	private void getObjectMovementScripts()
	{
		foreach (PlatformOscillation oscillation in topLevelComponent.transform.GetComponentsInChildren<PlatformOscillation>(true))
		{
			platformOscillationScripts.Add(oscillation);
		}
		foreach (PlatformRotator rotator in topLevelComponent.transform.GetComponentsInChildren<PlatformRotator>(true))
		{
			platformRotatorScripts.Add(rotator);
		}
		foreach (ObjectScaleOscillation scale in topLevelComponent.transform.GetComponentsInChildren<ObjectScaleOscillation>(true))
		{
			objectScaleScripts.Add(scale);
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
		foreach (ObjectScaleOscillation scale in objectScaleScripts)
		{
			scale.enabled = enabled;
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
