using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour {

	public GameObject asteroidLarge;
	public GameObject asteroidSmall;
	public GameObject powerup;

	public float radius;

	// Use this for initialization
	void Start () {
		
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
		}


	}
}
