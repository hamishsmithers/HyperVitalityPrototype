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
		powerupCounter = Random.Range (minRandom, maxRandom+1);
		for (int i = 0; i < asteroidAmount; i++) {
			SpawnAsteroid ();
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void AsteroidBreak(GameObject asteroid, bool isLarge, bool wasPlayer) {
		Vector3 position = asteroid.transform.position;
		Vector3 randomPosition = new Vector3 (Random.Range (-radius, radius), 0, Random.Range (-radius, radius));
//		Destroy (asteroid);
		if (isLarge == true) {
			GameObject GO1 = Instantiate (asteroidSmall, position + randomPosition, Quaternion.identity, transform) as GameObject;
			GameObject GO2 = Instantiate (asteroidSmall, position - randomPosition, Quaternion.identity, transform) as GameObject;
			GO1.GetComponent<Asteroid> ().direction = GO1.GetComponent<Asteroid> ().transform.position - GO2.GetComponent<Asteroid> ().transform.position;
			GO2.GetComponent<Asteroid> ().direction = GO2.GetComponent<Asteroid> ().transform.position - GO1.GetComponent<Asteroid> ().transform.position;
		} else {
			if (asteroidSpawnCounter < 1) {
				asteroidSpawnCounter = 1;
				SpawnAsteroid ();
			} else {
				asteroidSpawnCounter--;
			}
			if (wasPlayer) {
				if (powerupCounter < 1) {
					powerupCounter = Random.Range (minRandom, maxRandom+1);
					Instantiate (powerup, position, Quaternion.identity);
				} else {
					powerupCounter--;
				}
			}
		}
	}

	private void SpawnAsteroid(){
		if (Random.Range (0, 2) == 0) {
			GameObject GO =	Instantiate (asteroidLarge, new Vector3 (Random.Range (-rangeX, rangeX), 0, rangeZ), Quaternion.identity, transform) as GameObject;
			GO.name = Random.Range (0f, 1f).ToString();
		} else {
			GameObject GO =	Instantiate (asteroidLarge, new Vector3 (rangeX, 0, Random.Range(-rangeZ, rangeZ)), Quaternion.identity, transform) as GameObject;
			GO.name = Random.Range (0f, 1f).ToString();
		}

	}

}
