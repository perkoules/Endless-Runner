using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGroundSpawnerScript : MonoBehaviour {

	public GameObject bridgeToSpawn, boostersToSpawn;
	public GameObject[] obstaclesToSpawn;

	private GameObject[] obstacleTag, coinTag;
	private Transform playerTransform;
	private float spawnZ = -10f, tileLength = 10f, safeZone = 15f;
	private int amnTileOnScreen = 7;
	private List<GameObject> activeTiles;

	// Use this for initialization
	void Start () {
		activeTiles =  new List<GameObject>();
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		for (int i=0; i < amnTileOnScreen; i++){
			SpawnTiles();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerTransform.position.z - safeZone > (spawnZ - amnTileOnScreen * tileLength)){
			SpawnTiles();
			SpawnObstacles();
			DeleteTiles();
		}
	}

	void SpawnTiles(int prefabIndex = -1)
	{
		GameObject go;
		go = Instantiate(bridgeToSpawn) as GameObject;
		go.transform.SetParent(transform);
		go.transform.position = Vector3.forward * spawnZ;
		spawnZ += tileLength;
		activeTiles.Add(go);
	}

	void DeleteTiles()
	{
		Destroy(activeTiles[0]);
		activeTiles.RemoveAt(0);
	}

	void SpawnObstacles()
	{
		
		foreach (GameObject obs in obstaclesToSpawn){
			GameObject obstacle = Instantiate(obs, ObstaclePosition(),Quaternion.identity) as GameObject;
			GameObject coin = Instantiate(boostersToSpawn, ObstaclePosition(),Quaternion.Euler(0,90,0)) as GameObject;
			obstacle.transform.SetParent(transform);
			coin.transform.SetParent(transform);
		}
		obstacleTag = GameObject.FindGameObjectsWithTag("Obstacle");
		coinTag = GameObject.FindGameObjectsWithTag("Coin");
		for (int i = 0; i <25;i++){
			if (Vector3.Distance(playerTransform.position, obstacleTag[i].transform.position) > 60 && 
			Vector3.Distance(playerTransform.position, coinTag[i].transform.position) > 60){
				Destroy(obstacleTag[i]);
				Destroy(coinTag[i]);
			}
		}
	}

	Vector3 ObstaclePosition()
	{
		float x = Random.Range(-4.5f,4.5f);
		float y = playerTransform.position.y;
		float z = Random.Range(playerTransform.position.z + 45, playerTransform.position.z + 55);

		return new Vector3 (x,y,z);
	}

}
