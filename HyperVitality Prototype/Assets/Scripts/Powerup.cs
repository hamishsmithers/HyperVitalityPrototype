using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	//player1 holds a reference to the first player
	public GameObject player1;
	//player2 holds a reference to the second player
	public GameObject player2;

	//minSpeed is the slowest the powerup will move
	public float minSpeed;
	//maxSpeed is the fastest the powerup will move
	public float maxSpeed;

	//divisionAmount controls the distance that maxSpeed will activate at
	public float divisionAmount;

	//lifetime is how long the powerup lasts for
	public float lifetime;

	//currentSpeed holds the current speed of the powerup
	private float currentSpeed;

	//direction is the direction the powerup will move in
	private Vector3 direction;

	//attackSpeedDuration is the duration the attackSpeedPowerup lasts for
	public float attackSpeedDuration;

	//tripleShotDuration is the duration the tripleShotPowerup lasts for
	public float tripleShotDuration;

	//shieldDuration is the duration of the shieldPowerup lasts for
	public float shieldDuration;

	//selector selects which powerup this will be
	private int selector;

	//duration is how long this powerups effect lasts
	private float duration;

	//tripleShotMaterial is a reference to the material this will use if it is a triple shot powerup
	public Material tripleShotMaterial;
	//shieldMaterial is a reference to the material this will use if it is a shield powerup
	public Material shieldMaterial;

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
		selector = Random.Range (0, 3);
		if (selector == 0) {
			duration = attackSpeedDuration;
		} else if (selector == 1) {
			duration = tripleShotDuration;
			GetComponent<Renderer> ().material = tripleShotMaterial;
		} else if (selector == 2) {
			duration = shieldDuration;
			GetComponent<Renderer> ().material = shieldMaterial;
		}
		player1 = GameObject.FindGameObjectWithTag ("Player1");
		player2 = GameObject.FindGameObjectWithTag ("Player2");
		direction = new Vector3 (Random.Range (-1f, 1f), 0, Random.Range (-1f, 1f));
		Destroy (this.gameObject, lifetime);
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
	//	Called every Update(). Calculates and performs movement.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	private void Move(){
		if (player1 != null && player2 != null) {
			if (Vector3.Distance (transform.position, player1.transform.position) < Vector3.Distance (transform.position, player2.transform.position)) {
				direction = player1.transform.position - transform.position;
				currentSpeed = Vector3.Distance (transform.position, player1.transform.position);
			} else {
				direction = player2.transform.position - transform.position;
				currentSpeed = Vector3.Distance (transform.position, player2.transform.position);
			}
		} else if (player1 != null && player2 == null) {
			direction = player1.transform.position - transform.position;
			currentSpeed = Vector3.Distance (transform.position, player1.transform.position);
		} else if (player1 == null && player2 != null) {
			direction = player2.transform.position - transform.position;
			currentSpeed = Vector3.Distance (transform.position, player2.transform.position);
		}

		currentSpeed = -currentSpeed / divisionAmount;
		currentSpeed += maxSpeed;
		currentSpeed = -currentSpeed;
	


		if (currentSpeed < minSpeed) {
			currentSpeed = minSpeed;
		} else if (currentSpeed > maxSpeed) {
			currentSpeed = maxSpeed;
		}

		if (Vector3.Magnitude(direction) > currentSpeed) {
			direction = direction.normalized * currentSpeed;
		}

		transform.position += (direction) * currentSpeed * Time.deltaTime;
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
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerMesh> ().Powerup (selector, duration);
			Destroy (this.gameObject);
		}
	}
}
