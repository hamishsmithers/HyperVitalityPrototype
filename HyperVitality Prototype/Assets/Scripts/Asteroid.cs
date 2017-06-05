using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	//asteroidControl is reference to AsteroidControl gameobject
	public AsteroidControl asteroidControl;

	//isLarge holds whether an asteroid is large or not
	public bool isLarge;

	//speed is the speed at which the asteroid will move
	public float speed;

	//rotationSpeed is the speed at which the asteroid will rotate
	public float rotationSpeed;

	//rangeX is the distance from x=0 that the asteroid can move on the x axis to until resetting
	public float rangeX;
	//rangeZ is the distance from z=0 that the asteroid can move on the z axis to until resetting
	public float rangeZ;

	//direction is the direction the asteroid is moving in
	public Vector3 direction;

	//damage is the amount of damage the asteroid will inflict upon players
	public int damage;

	//rotationDirection is the 'direction' at which the asteroid will rotate
	private Vector3 rotationDirection;

	//isBreaking indicates whether or not the asteroid is breaking
	private bool isBreaking = false;

	//canBreak tracks whether the asteroid can break
	private bool canBreak =true;

	//----------------------------------------------------------------------------------------------------
	//Awake()
	//	Called on instantiation. Instantiate this instance.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	void Awake () {
		//x is a random number used to initialize movement and rotation should it not be set externally
		float x = Random.Range (-1f, 1f);
		//y is a random number used to initialize movement and rotation should it not be set externally
		float y = Random.Range (-1f, 1f);
		//z is a random number used to initialize movement and rotation should it not be set externally
		float z = Random.Range (-1f, 1f);
		direction = new Vector3 (x, 0, z);
		rotationDirection = new Vector3 (z, y, z);
		asteroidControl = GameObject.FindObjectOfType<AsteroidControl>();

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
		Move ();
	}

	//----------------------------------------------------------------------------------------------------
	//Move()
	//	Called every Update(). Moves the asteroid.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	private void Move(){
		transform.position += direction.normalized * speed * Time.deltaTime;
		transform.Rotate (rotationDirection.normalized * rotationSpeed * Time.deltaTime);

		if (transform.position.x < -rangeX) {
			transform.position = new Vector3 (rangeX, 0, transform.position.z);
		} else if (transform.position.x > rangeX) {
			transform.position = new Vector3 (-rangeX, 0, transform.position.z);
		}

		if (transform.position.z < -rangeZ) {
			transform.position=new Vector3(transform.position.x,0,rangeZ);
		} else if (transform.position.z > rangeZ) {
			transform.position=new Vector3(transform.position.x,0,-rangeZ);
		}
	}

	//----------------------------------------------------------------------------------------------------
	//Break()
	//	Called from OnCollisionEnter(). Destroys this asteroid and calls apropriate controller scrips.
	// 
	//	Param:
	//		bool wasPlayer - whether the collision was caused by a player or bullet
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void Break(bool wasPlayer) {
		if (canBreak == true) {
			//isBreaking = true;
			//asteroidControl.GetComponent<AsteroidControl> ().AsteroidBreak (this.gameObject, isLarge);
			if (!asteroidControl) {
				Debug.Log (gameObject.name);
				GetComponent<BoxCollider> ().enabled = false;
				return;
			} else {
				canBreak = false;
				asteroidControl.AsteroidBreak (this.gameObject, isLarge, wasPlayer);
				Destroy (this.gameObject);
			}
		}
	}

	//----------------------------------------------------------------------------------------------------
	//OnCollisionEnter()
	//	Called from collisions.
	// 
	//	Param:
	//		Collision other - other object collided with.
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	void OnCollisionEnter(Collision other){
		if (isBreaking == false) {
			if (other.gameObject.tag == "Asteroid") {
				direction = transform.position - other.transform.position;
				direction += new Vector3 (Random.Range (-1f, 1f), 0, Random.Range (-1f, 1f)) / 5;
				//Break (false);
			} else if (other.gameObject.tag == "Player") {
				other.gameObject.GetComponent<PlayerMesh> ().TakeDamage (damage);
				isBreaking = true;
				Break (true);
			} 
		}
	}
}
