using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float SecondsToDestroy = 10;

	void Start ()
	{
		StartCoroutine(WaitToDestroy());
	}

	IEnumerator WaitToDestroy()
	{
		yield return new WaitForSeconds(SecondsToDestroy);
		destroyObject();
	}

	public void destroyObject()
	{
		Destroy(gameObject);
	}
}
