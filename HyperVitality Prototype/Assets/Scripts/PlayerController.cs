using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {

	//controller is a reference to the Xbox controller this player uses for input
	public XboxController controller;

	//health is how much health the ship has
	public int health;

	//mesh is a reference to the model of the ship
	public GameObject mesh;

	//maxSpeed is the fastest speed this player can move
	public float minSpeed;
	//minSpeed isthe slowest speed this player can move
	public float maxSpeed;

	//acceleration is how much the currentSpeed increases or decreases each frame
	public float acceleration;

	//adjustment indicates how 'glidey' this player's movement feels; higher numbers = less glidey
	public float adjustment;

	//bulletPrefab is a referemce to the bullet that will be shot
	public GameObject bulletPrefab;

	//bulletSpawn is a reference to the place on the ship that the bullet will spawn from
	public GameObject bulletSpawn;

	//bulletParent is a reference to the parent of bullets in the heirarchy
	public GameObject bulletParent;

	//bulletSpeed is the base speed at which this player's bullets will move
	public float bulletSpeed;

	//timeBetweenShots is the time between this player's shots in seconds
	public float timeBetweenShots;

	//raycastForward is a reference to the location forward, left, and right racyasts will be cast from
	public Transform raycastForward;
	//raycastBackward is a reference to the location backward, left, and right racyasts will be cast from
	public Transform raycastBackward;
	//raycastLeft is a reference to the location forward, backward, and left racyasts will be cast from
	public Transform raycastLeft;
	//raycastRight is a reference to the location forward, backward, and right racyasts will be cast from
	public Transform raycastRight;

	//currentSpeed is the speed at which the player is currently moving
	private float currentSpeed;

	//timer is the time at which the player can fire again
	private float timer;

	//direction is the direction the player is moving in
	private Vector3 direction  = Vector3.zero;

	//rotation is the direction the ship is facing
	private Vector3 rotation = Vector3.zero;

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
		timer = Time.time;
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
		PlayerInput ();
		RaycastForWalls ();
		MovePlayer ();
	}

	//----------------------------------------------------------------------------------------------------
	//PlayerInput()
	//	Called every Update(). Obtains input and sets direction and rotation.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	private void PlayerInput() {
		//moveX holds the X value of the left analog stick
		float moveX = XCI.GetAxis (XboxAxis.LeftStickX, controller);
		//moveY holds the Y value of the left analog stick
		float moveZ = XCI.GetAxis (XboxAxis.LeftStickY, controller);
		//tempPositionInput holds the current input multiplied by the adjustment value
		Vector3 tempPositionInput = new Vector3 (moveX, 0, moveZ) * Time.deltaTime * adjustment;

		direction += tempPositionInput;

		if (moveX == 0 && moveZ == 0) {
			currentSpeed -= acceleration;
			if (currentSpeed < minSpeed) {
				currentSpeed = minSpeed;
			}
		} else {
			currentSpeed += acceleration;
			if (currentSpeed > maxSpeed) {
				currentSpeed = maxSpeed;
			}
		}

		if (Vector3.Magnitude (direction) > currentSpeed) {
			direction = direction.normalized*currentSpeed;
		}

		//rotateX holds the X value of the right analog stick
		float rotateX = XCI.GetAxis (XboxAxis.RightStickX, controller);
		//rotateY holds the Y value of the right analog stick
		float rotateZ = XCI.GetAxis (XboxAxis.RightStickY, controller);
		if (rotateX != 0 || rotateZ != 0) {
			//tempRotationInput holds the current input with normalization
			Vector3 tempRotationInput = new Vector3 (rotateX, 0, rotateZ).normalized * 10;
			rotation = transform.position + tempRotationInput;
			mesh.transform.LookAt (rotation);
			Fire (tempRotationInput);
		}
	}

	//----------------------------------------------------------------------------------------------------
	//Fire()
	//	Called from PlayerInput() if player inputs attacks. Fires bullets if sufficient time has passed.
	// 
	//	Param:
	//		Vector3 bulletDirection - the direction the bullets will move in
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	private void Fire(Vector3 bulletDirection){
		if (Time.time - timer > timeBetweenShots) {
			timer = Time.time;
			GameObject Bullet = Instantiate (bulletPrefab, bulletSpawn.transform.position, Quaternion.identity, bulletParent.transform) as GameObject;
			Bullet.GetComponent<Bullet> ().direction = bulletDirection.normalized;
			Bullet.GetComponent<Bullet> ().speed = bulletSpeed + (currentSpeed / 3);
		}
	}

	//----------------------------------------------------------------------------------------------------
	//TakeDamage()
	//	Called from the player mesh when colliding with a damaging object. Reduces health by damage.
	// 
	//	Param:
	//		int damage - damage to take
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void TakeDamage(int damage){
		health -= damage;
	}

	//----------------------------------------------------------------------------------------------------
	//MovePlayer()
	//	Called every Update(). Moves the player.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	private void MovePlayer(){
		transform.position+=direction / 10;
	}

	//----------------------------------------------------------------------------------------------------
	//RaycastForWalls()
	//	Called every Update() (prior to MovePlayer()). Detects boundaries near the player and prevents movement.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	void RaycastForWalls()
	{
		//hit holds a reference to the object the raycast hits
		RaycastHit hit;
		//raycastLength is the distance at which the raycast travels
		float raycastLength = 0.25f;

		//Forward Raycasts
		if (Physics.Raycast (raycastForward.position, transform.forward, out hit,raycastLength)) {
			if (hit.transform.tag == "Boundary") {
				if (direction.z > 0) {
					direction = new Vector3 (direction.x, direction.y, 0);
				}
			}
		}
		if (Physics.Raycast (raycastRight.position, transform.forward, out hit,raycastLength)) {
			if (hit.transform.tag == "Boundary") {
				if (direction.z > 0) {
					direction = new Vector3 (direction.x, direction.y, 0);
				}
			}
		}
		if (Physics.Raycast (raycastLeft.position, transform.forward, out hit,raycastLength)) {
			if (hit.transform.tag == "Boundary") {
				if (direction.z > 0) {
					direction = new Vector3 (direction.x, direction.y, 0);
				}
			}
		}

		//Backward Raycasts
		if (Physics.Raycast (raycastBackward.position, -transform.forward, out hit,raycastLength)) {
			if (hit.transform.tag == "Boundary") {
				if (direction.z < 0) {
					direction = new Vector3 (direction.x, direction.y, 0);
				}
			}
		}
		if (Physics.Raycast (raycastRight.position, -transform.forward, out hit,raycastLength)) {
			if (hit.transform.tag == "Boundary") {
				if (direction.z < 0) {
					direction = new Vector3 (direction.x, direction.y, 0);
				}
			}
		}
		if (Physics.Raycast (raycastLeft.position, -transform.forward, out hit,raycastLength)) {
			if (hit.transform.tag == "Boundary") {
				if (direction.z < 0) {
					direction = new Vector3 (direction.x, direction.y, 0);
				}
			}
		}

		//Right Raycasts
		if (Physics.Raycast (raycastRight.position, transform.right, out hit,raycastLength)) {
			if (hit.transform.tag == "Boundary") {
				if (direction.x > 0) {
					direction = new Vector3 (0, direction.y, direction.z);
				}
			}
		}
		if (Physics.Raycast (raycastForward.position, transform.right, out hit,raycastLength)) {
			if (hit.transform.tag == "Boundary") {
				if (direction.x > 0) {
					direction = new Vector3 (0, direction.y, direction.z);
				}
			}
		}
		if (Physics.Raycast (raycastBackward.position, transform.right, out hit,raycastLength)) {
			if (hit.transform.tag == "Boundary") {
				if (direction.x > 0) {
					direction = new Vector3 (0, direction.y, direction.z);
				}
			}
		}

		//Left Raycasts
		if (Physics.Raycast (raycastLeft.position, -transform.right, out hit,raycastLength)) {
			if (hit.transform.tag == "Boundary") {
				if (direction.x < 0) {
					direction = new Vector3 (0, direction.y, direction.z);
				}
			}
		}
		if (Physics.Raycast (raycastForward.position, -transform.right, out hit,raycastLength)) {
			if (hit.transform.tag == "Boundary") {
				if (direction.x < 0) {
					direction = new Vector3 (0, direction.y, direction.z);
				}
			}
		}
		if (Physics.Raycast (raycastBackward.position, -transform.right, out hit,raycastLength)) {
			if (hit.transform.tag == "Boundary") {
				if (direction.x < 0) {
					direction = new Vector3 (0, direction.y, direction.z);
				}
			}
		}

	}

}