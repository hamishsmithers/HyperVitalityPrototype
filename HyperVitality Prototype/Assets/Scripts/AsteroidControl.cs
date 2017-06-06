using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour {

	//asteroidAmount is the amount of asteroids created at the start of the game
	public int asteroidAmount;

	//asteroidLarge is a reference to the large asteroid prefab
	public GameObject asteroidLarge;
	//asteroidSmall is a reference to the small asteroid prefab
	public GameObject asteroidSmall;
	//powerup is a reference to the powerup prefab
	public GameObject powerup;

	//rangeX is the distance from x=0 new asteroids will be created at on the x axis
	public float rangeX;
	//rangeZ is the distance from z=0 new asteroids will be created at on the z axis
	public float rangeZ;

	//radius is the distance within new asteroids will be created on largeAsteroid deaths on both the x and z axis
	public float radius;

	//minRandom is the minimum amount of asteroid deaths until a new powerup is spawned
	public int minRandom;
	//maxRandom is the maximum amount of asteroid deaths until a new powerup is spawned
	public int maxRandom;

	//powerupCounter tracks how many more asteroid deaths caused by the player are needed until a new powerup is spawned
	private int powerupCounter;

	//asteroidSpawnCounter tracks how many more asteroid deaths are needed until a new large asteroid is spawned
	private int asteroidSpawnCounter;

	//----------------------------------------------------------------------------------------------------
	//Start()
	//	Called on instantiation. Instantiate this instance.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	void Start () {
		asteroidSpawnCounter = 1;
		powerupCounter = Random.Range (minRandom, maxRandom+1);
		for (int i = 0; i < asteroidAmount; i++) {
			SpawnAsteroid ();
		}
	}
	
	//----------------------------------------------------------------------------------------------------
	//Update()
	//	Called every frame. Update this instance.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	void Update () {

	}

	//----------------------------------------------------------------------------------------------------
	//AsteroidBreak()
	//	Called from asteroid's Break(). Creates apropriate asteroids and powerups.
	// 
	//	Param:
	//		GameObject asteroid - the asteroid that called this
	//		bool isLarge - was the asteroid that called this large
	//		bool wasPlayer - was it the player that destroyed the asteroid
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void AsteroidBreak(GameObject asteroid, bool isLarge, bool wasPlayer) {
		//position obtains the position of the asteroid that was destroyed
		Vector3 position = asteroid.transform.position;
		//randomPosition is a random position that will be used to spawn two new small asteroids
		Vector3 randomPosition = new Vector3 (Random.Range (-radius, radius), 0, Random.Range (-radius, radius));
		if (isLarge == true) {
			//GO1 is the first of two small asteroids being created
			GameObject GO1 = Instantiate (asteroidSmall, position + randomPosition, Quaternion.identity, transform) as GameObject;
			//GO2 is the second of two small asteroids being created
			GameObject GO2 = Instantiate (asteroidSmall, position - randomPosition, Quaternion.identity, transform) as GameObject;
			GO1.GetComponent<Asteroid> ().direction = GO1.GetComponent<Asteroid> ().transform.position - GO2.GetComponent<Asteroid> ().transform.position;
			GO1.transform.Rotate (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360));
			GO2.GetComponent<Asteroid> ().direction = GO2.GetComponent<Asteroid> ().transform.position - GO1.GetComponent<Asteroid> ().transform.position;
			GO2.transform.Rotate (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360));
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

	//----------------------------------------------------------------------------------------------------
	//SpawnAsteroid()
	//	Called from AsteroidBreak() and Start(). Spawns new large asteroids.
	//
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	private void SpawnAsteroid(){
		if (Random.Range (0, 2) == 0) {
			//GO is a large asteroid being created with a random X and a set Z
			GameObject GO =	Instantiate (asteroidLarge, new Vector3 (Random.Range (-rangeX, rangeX), 0, rangeZ), Quaternion.identity, transform) as GameObject;
			GO.transform.Rotate (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360));
			GO.name = Random.Range (0f, 1f).ToString();
		} else {
			//GO is a large asteroid being created with a set X and a random
			GameObject GO =	Instantiate (asteroidLarge, new Vector3 (rangeX, 0, Random.Range(-rangeZ, rangeZ)), Quaternion.identity, transform) as GameObject;
			GO.name = Random.Range (0f, 1f).ToString();
			GO.transform.Rotate (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360));
		}

	}

}
