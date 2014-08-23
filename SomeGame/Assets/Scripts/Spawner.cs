﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject[] enemiesToSpawn;
    public GameObject[] powerupsToSpawn;
    float minSpawnTime = 1f;
    float maxSpawnTime = 3f;
    bool canSpawn = false;
    float powerupChance = 0.20f;

    IEnumerator delaySpawn(float delay) {
        yield return new WaitForSeconds(delay + 1f);
        canSpawn = true;
        updateSpawner();
    }

	// Use this for initialization
	void Start () {
        updateSpawner();
        StartCoroutine(delaySpawn(Random.Range(minSpawnTime, maxSpawnTime)));
	}
	
	// Update is called once per frame
	void Update () {
        
	    // pick object to spawn
        if (canSpawn) {
            if (Random.value < powerupChance) {
                GameObject powerup = (GameObject)Instantiate(powerupsToSpawn[(int)Mathf.Round(Random.Range(0, enemiesToSpawn.Length - 1))],
                    (this.transform.position + (new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f))),
                    Quaternion.AngleAxis(0f, Vector3.forward));
                powerup.layer = (int)Random.Range(8, 8 + GameController.control.level); //this.gameObject.layer;
                //spawn power up
            } else {
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[(int)Mathf.Round(Random.Range(0, enemiesToSpawn.Length - 1))], 
                    (this.transform.position + (new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f))), 
                    Quaternion.AngleAxis(0f, Vector3.forward));
                enemy.layer = (int)Random.Range(8, 8 + GameController.control.level); //this.gameObject.layer;

                // spawn enemy
            }
            canSpawn = false;
            StartCoroutine(delaySpawn(Random.Range(minSpawnTime, maxSpawnTime)));
        }
	}

    void updateSpawner() {
        minSpawnTime = GameController.control.minSpawnTime;
        maxSpawnTime = GameController.control.maxSpawnTime;
        powerupChance = GameController.control.powerupChance;
    }
}
