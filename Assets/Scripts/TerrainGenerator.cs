﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

	public GameObject terrain;
	public Transform generationPoint;
	public float distance;

	public float distanceMin;
	public float distanceMax;

	private float terrainWidth;

	// public GameObject[] terrainArray;
	private int terrainSelector;
	private float[] terrainWidths;

	public ObjectPooler[] objectPools;

	private float minHeight;
	public Transform maxHeightPoint;
	private float maxHeight;
	public float maxHeightChange;
	private float heightChange;

	public PlayerController playerController;

	public float powerupHeight;
	public ObjectPooler[] powerupPools;
	private int powerupSelector;
	public float[] powerupThresholds;

	// Use this for initialization
	void Start () {
		// terrainWidth = terrain.GetComponent<BoxCollider2D>().size.x;
		terrainWidths = new float[objectPools.Length];

		for(int i = 0; i < objectPools.Length; i++) {
			terrainWidths[i] = objectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
		}

		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < generationPoint.position.x) {
			distance = Random.Range (distanceMin, distanceMax);
			terrainSelector = Random.Range(0, objectPools.Length);
			
			heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

			if(heightChange > maxHeight) {
				heightChange = maxHeight;
			} else if (heightChange < minHeight) {
				heightChange = minHeight;
			}

			if(playerController.hasJetPack) {
				distance = 0;
				heightChange = minHeight;
				terrainSelector = 0;
			}

			powerupSelector = Random.Range(0, powerupPools.Length);

			if(Random.Range(0f, 100f) < powerupThresholds[powerupSelector]) {
				GameObject newPowerup = powerupPools[powerupSelector].GetPooledObject();
				newPowerup.transform.position = transform.position + new Vector3(distance / 2f, Random.Range(powerupHeight / 2f, powerupHeight), 0f);
				newPowerup.SetActive(true);
			}

			transform.position = new Vector3(transform.position.x + (terrainWidths[terrainSelector] / 2) + distance, heightChange, transform.position.z);

			// Instantiate (terrainArray[terrainSelector], transform.position, transform.rotation);
			GameObject newTerrain = objectPools[terrainSelector].GetPooledObject();

			newTerrain.transform.position = transform.position;
			newTerrain.transform.rotation = transform.rotation;
			newTerrain.SetActive(true);

			transform.position = new Vector3(transform.position.x + (terrainWidths[terrainSelector] / 2), transform.position.y, transform.position.z);
		}
	}
}
