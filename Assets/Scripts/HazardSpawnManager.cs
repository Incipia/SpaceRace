﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HazardSpawnManager : MonoBehaviour 
{
	public GameObject spawnedObstacle;

	public float initialHazardSpawnInterval;
	public float minimumHazardSpawnInterval;
	public float initialWaitBeforeSpawn;

	private bool shouldStopSpawning;

	private bool spawningGeneralPrefabs;
	private Vector3 screenDimensions;

	private float currentHazardSpawnInterval;
	
	private bool shouldSpawnPuzzle;
	private bool firstHazardDidSpawn;
	private GameObject player;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		currentHazardSpawnInterval = initialHazardSpawnInterval;

		StartCoroutine(spawnPrefabs());
	}
	
	void Update()
	{
		getScreenDimensions();
	}
	
	
	private void getScreenDimensions()
	{
		Vector3 pixelDimensions = new Vector3(Screen.width, Screen.height, 0);
		screenDimensions = Camera.main.ScreenToWorldPoint(pixelDimensions);
	}
	
	IEnumerator spawnPrefabs()
	{
		if (firstHazardDidSpawn == false)
		{
			firstHazardDidSpawn = true;
			yield return new WaitForSeconds(initialWaitBeforeSpawn);
		}
		while (shouldSpawnPuzzle == false && player != null && !shouldStopSpawning)
		{
			instantiateRandomHazard();
			yield return new WaitForSeconds(currentHazardSpawnInterval);
		}
	}

	private void instantiateRandomHazard()
	{
		float yPadding = 1f;
		Vector2 spawnPosition = new Vector2(Random.Range(-4f, 4f), screenDimensions.y + yPadding);
		Quaternion spawnRotation = Quaternion.identity;

		GameObject currentPrefab = spawnedObstacle;

		Instantiate(currentPrefab, spawnPosition, spawnRotation);
		Debug.Log ("Spawned Rain");
    }

	public void continueSpawningRandomHazards()
	{
		shouldSpawnPuzzle = false;
		StartCoroutine(spawnPrefabs());
	}

	public float getCurrentHazardSpawnInterval()
	{
		return currentHazardSpawnInterval;
	}

	public void stopSpawning()
	{
		shouldStopSpawning = true;
	}
}
