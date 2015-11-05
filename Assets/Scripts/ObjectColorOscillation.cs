using UnityEngine;
using System.Collections;

public class ObjectColorOscillation : MonoBehaviour
 {
	public Color targetColor = new Color(1, 0, 0.8f, 0.6f);
	public float scaleDuration = 2;
	public float initialWaitDuration = 0;
	
	IEnumerator Start()
	{
		yield return new WaitForSeconds(initialWaitDuration);
		LeanTween.color(gameObject, targetColor, scaleDuration).setLoopPingPong();
	}
}
