using UnityEngine;

public class BreakCollisionFeedback : MonoBehaviour
{
	public ParticleSystem _breakParticleSystem;

	public void showBreakFeedback()
	{
		if (_breakParticleSystem != null)
		{
			_breakParticleSystem.Play();
            Destroy(gameObject, _breakParticleSystem.duration);
        }
	}
}
