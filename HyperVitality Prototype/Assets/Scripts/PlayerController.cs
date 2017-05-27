using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {

	//controller is a reference to the Xbox controller the object uses for input
	public XboxController controller;

	//health is how much health the ship has; 
	public int health;

	//mesh is a reference to the model of the ship
	public GameObject mesh;

	//maxSpeed is the fastest the ship can move, and minSpeed is the slowest.
	public float minSpeed;
	public float maxSpeed;

	//acceleration is how much the currentSpeed increases or decreases each second
	public float acceleration;

	//adjustment controls how 'glidey' movement feels; higher numbers = less glidey
	public float adjustment;

	//bulletPrefab is the bullet that will be shot
	public GameObject bulletPrefab;

	//bulletSpawn is the place on the ship that the bullet will spawn from
	public GameObject bulletSpawn;

	//bulletParent is the parent of bullets in the heirarchy
	public GameObject bulletParent;

	//set amount of bulletSpeed;
	public float bulletSpeed;

	//timeBetweenShots is the time between shots
	public float timeBetweenShots;

	//These are the transforms for raycasts the ship uses for wall detection
	public Transform raycastForward;
	public Transform raycastBackward;
	public Transform raycastLeft;
	public Transform raycastRight;

	//currentSpeed tracks the speed the ship will currently move
	private float currentSpeed;

	//timer tracks the time until the player can fire again
	private float timer;

	//direction is the direction the player is moving in
	private Vector3 direction  = Vector3.zero;

	//moveX and moveZ hold information for the Axes on the controller's left stick
	private float moveX = 0;
	private float moveZ = 0;

	//rotation is the direction the ship will face
	private Vector3 rotation = Vector3.zero;

	//rotateX and rotateZ hold information for the Axes on the controller's right stick
	private float rotateX;
	private float rotateZ;

	// Use this for initialization
	void Start () {
		timer = Time.time;
	}

	// Update is called once per frame
	void Update () {
		PlayerInput ();
		RayCastForWalls ();
		MovePlayer ();
	}

	//PlayerInput gets the player's inputs and sets values apropriately
	private void PlayerInput() {
		moveX = XCI.GetAxis (XboxAxis.LeftStickX, controller);
		moveZ = XCI.GetAxis (XboxAxis.LeftStickY, controller);
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

		rotateX = XCI.GetAxis (XboxAxis.RightStickX, controller);
		rotateZ = XCI.GetAxis (XboxAxis.RightStickY, controller);
		if (rotateX != 0 || rotateZ != 0) {
			Vector3 tempRotationInput = new Vector3 (rotateX, 0, rotateZ).normalized * 10;
			rotation = transform.position + tempRotationInput;
			mesh.transform.LookAt (rotation);
			Fire (tempRotationInput);
		}
	}

	//Fires a bullet if allowed
	private void Fire(Vector3 bulletDirection){
		if (Time.time - timer > timeBetweenShots) {
			timer = Time.time;
			GameObject Bullet = Instantiate (bulletPrefab, bulletSpawn.transform.position, Quaternion.identity, bulletParent.transform) as GameObject;
			Bullet.GetComponent<Bullet> ().direction = bulletDirection.normalized;
			Bullet.GetComponent<Bullet> ().speed = bulletSpeed + (currentSpeed / 3);
		}
	}

	public void TakeDamage(int damage){
		health -= damage;
	}

	//Moves the player. Called every update.
	private void MovePlayer(){
		transform.position+=direction / 10;
	}

	//Detects boundaries and alters direction of movement to avoid collision
	void RayCastForWalls()
	{
		RaycastHit hit;
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