using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour {

	public int asteroidAmount;

	public GameObject asteroidLarge;
	public GameObject asteroidSmall;
	public GameObject powerup;

	public float rangeX;
	public float rangeZ;

	public float radius;

	public int minRandom;
	public int maxRandom;

	private int powerupCounter;

	private int asteroidSpawnCounter;

	// Use this for initialization
	void Start () {
		asteroidSpawnCounter = 1;
		powerupCounter = Random.Range (minRandom, maxRandom);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AsteroidBreak(GameObject asteroid, bool isLarge) {
		Vector3 position = asteroid.transform.position;
		Vector3 randomPosition = new Vector3 (Random.Range (-radius, radius), 0, Random.Range (-radius, radius));
		Destroy (asteroid);
		if (isLarge == true) {
			Instantiate (asteroidSmall, position + randomPosition, Quaternion.identity, transform);
			Instantiate (asteroidSmall, position - randomPosition, Quaternion.identity, transform);
		} else {
			if (asteroidSpawnCounter < 1) {
				asteroidSpawnCounter = 1;
				Instantiate (asteroidLarge, new Vector3 (Random.Range (-rangeX, rangeX), 0, rangeZ), Quaternion.identity, transform);
			} else {
				asteroidSpawnCounter--;
			}
			if (powerupCounter < 1) {
				powerupCounter = Random.Range (minRandom, maxRandom);
				Instantiate (powerup, position, Quaternion.identity);
			} else {
				powerupCounter--;
			}
		}


	}
}
