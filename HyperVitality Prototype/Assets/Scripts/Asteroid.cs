﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	//tracks whether an asteroid is large or small
	public GameObject asteroidControl;

	public bool isLarge;

	public float speed;
	public float rotationSpeed;

	public float rangeX;
	public float rangeZ;


	private float x;
	private float y;
	private float z;

	private Vector3 direction;
	private Vector3 rotationDirection;

	// Use this for initialization
	void Start () {
		x = Random.Range (-100, 100);
		y = Random.Range (-100, 100);
		z = Random.Range (-100, 100);
		Debug.Log (x + " " + y + " " + z);
		x = x / 100;
		y = y / 100;
		z = z / 100;
		Debug.Log (x + " " + y + " " + z);
		direction = new Vector3 (x, 0, z);
		rotationDirection = new Vector3 (z, y, z);

	}
	
	// Update is called once per frame
	void Update () {
		Move ();

		if (Input.GetKeyDown (KeyCode.A)) {
			Break ();
		}

	}

	//Moves the asteroid. Called once every update.
	private void Move(){
		transform.position += direction.normalized * speed;
		transform.Rotate (rotationDirection.normalized * rotationSpeed);

		if (transform.position.x < -rangeX) {
			transform.position = new Vector3 (rangeX, 0, -transform.position.z);
		} else if (transform.position.x > rangeX) {
			transform.position=new Vector3(-rangeX,0,-transform.position.z);
		}

		if (transform.position.z < -rangeZ) {
			transform.position=new Vector3(-transform.position.x,0,rangeZ);
		} else if (transform.position.z > rangeZ) {
			transform.position=new Vector3(-transform.position.x,0,-rangeZ);
		}
	}

	private void Break() {
		asteroidControl.GetComponent<AsteroidControl> ().AsteroidBreak (this.gameObject, isLarge);
		Destroy (this.gameObject);

	}

}
