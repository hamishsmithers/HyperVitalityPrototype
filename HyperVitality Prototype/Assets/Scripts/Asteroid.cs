using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	//reference to AsteroidControl / Parent
	public AsteroidControl asteroidControl;

	//tracks whether an asteroid is large or small
	public bool isLarge;

	//speed at which the asteroid moves
	public float speed;

	//speed at which the asteroid rotates
	public float rotationSpeed;

	//Half the length/width of the play area
	public float rangeX;
	public float rangeZ;

	//direction the asteroid moves in
	public Vector3 direction;

	//direction the asteroid rotates in
	private Vector3 rotationDirection;

	private bool isBreaking = false;

	// Use this for initialization
	void Awake () {
		float x = Random.Range (-1f, 1f);
		float y = Random.Range (-1f, 1f);
		float z = Random.Range (-1f, 1f);
		direction = new Vector3 (x, 0, z);
		rotationDirection = new Vector3 (z, y, z);
		asteroidControl = GameObject.FindObjectOfType<AsteroidControl>();

	}
	
	// Update is called once per frame
	void Update () {
		Move ();

	}

	//Moves the asteroid. Called once every update.
	private void Move(){
		transform.position += direction.normalized * speed;
		transform.Rotate (rotationDirection.normalized * rotationSpeed);

		if (transform.position.x < -rangeX) {
			transform.position = new Vector3 (rangeX, 0, transform.position.z);
		} else if (transform.position.x > rangeX) {
			transform.position=new Vector3(-rangeX,0,transform.position.z);
		}

		if (transform.position.z < -rangeZ) {
			transform.position=new Vector3(transform.position.x,0,rangeZ);
		} else if (transform.position.z > rangeZ) {
			transform.position=new Vector3(transform.position.x,0,-rangeZ);
		}
	}

	//Calls function in controller to break this asteroid and spawn apropriate asteroids or powerups.
	public void Break(bool wasPlayer) {
		isBreaking = true;
		//asteroidControl.GetComponent<AsteroidControl> ().AsteroidBreak (this.gameObject, isLarge);
		if (!asteroidControl) {
			Debug.Log (gameObject.name);
			GetComponent<BoxCollider> ().enabled = false;
			return;
		} else {
			asteroidControl.AsteroidBreak (this.gameObject, isLarge, wasPlayer);
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision other){
		if (isBreaking == false) {
			if (other.gameObject.tag == "Asteroid") {
				direction = transform.position - other.transform.position;
				direction += new Vector3 (Random.Range (-1f, 1f), 0, Random.Range (-1f, 1f)) / 5;
				//Break (false);
			} else if (other.gameObject.tag == "Player") {
				other.gameObject.GetComponent<PlayerMesh>().TakeDamage(5);
				Break (true);
			}
		}
	}
}
