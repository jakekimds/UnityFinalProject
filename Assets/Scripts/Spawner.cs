using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour {

	public float spawnDelay = 1;
	public int maxNum = 10;
	public GameObject SpawnObj;
	public float lookRadius;

	private float nextSpawn = 0;
	private int numSpawned = 0;
	private Transform target;
	private bool detectedPlayer;

	// Use this for initialization
	void Start () {
		target = PlayerTracker.playerTransform;
		detectedPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			if (!detectedPlayer) {
				float distance = Vector3.Distance(target.position, transform.position);
				if (distance <= lookRadius) {
					detectedPlayer = true;
				}
			}else{
				Spawn();
			}
		} else {
			detectedPlayer = false;
		}
	}

	void Spawn() {
		if (Time.time >= nextSpawn && numSpawned <= maxNum) {
			Instantiate(SpawnObj, transform.position, SpawnObj.transform.rotation);
			numSpawned++;
			nextSpawn = Time.time + spawnDelay;
		}
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
