using UnityEngine;
using System.Collections;

public class HazardSpawnManager : MonoBehaviour
{
    public GameObject spawnedObstacle;
    public float hazardSpawnInterval;

    private bool shouldStopSpawning;
    private Vector3 pixelDimensions = new Vector3(Screen.width, Screen.height, 0);
	private Vector3 screenDimensions;

	void Update()
	{
		getScreenDimensions();
    }

	private void getScreenDimensions()
	{
		screenDimensions = Camera.main.ScreenToWorldPoint(pixelDimensions);
	}

	IEnumerator spawnPrefabs()
	{
		while (!shouldStopSpawning)
		{
			instantiateRandomHazard();
			yield return new WaitForSeconds(hazardSpawnInterval);
		}
	}

	private void instantiateRandomHazard()
	{
		float yPadding = 1f;
		Vector2 spawnPosition = new Vector2(Random.Range(-4f, 4f), screenDimensions.y + yPadding);
		Quaternion spawnRotation = Quaternion.identity;

        Instantiate(spawnedObstacle, spawnPosition, spawnRotation);
    }

	public void stopSpawning()
	{
		shouldStopSpawning = true;
	}

	public void startSpawning()
	{
        StartCoroutine(spawnPrefabs());
    }
}
