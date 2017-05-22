using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {

	//mesh is a reference to the model of the ship
	public GameObject mesh;

	//bulletPrefab is the bullet that will be shot
	public GameObject bulletPrefab;

	public GameObject bulletSpawn;

	public GameObject bulletHolder;

	public float timeBetweenShots;

	//controller is a reference to the Xbox controller the object uses for input
	public XboxController controller;

	//maxSpeed is the fastest the ship can move, and minSpeed is the slowest.
	public float minSpeed;
	public float maxSpeed;

	public float currentSpeed;

	public float acceleration;

	public float adjustment;

	public Transform raycastForward;
	public Transform raycastBackward;
	public Transform raycastLeft;
	public Transform raycastRight;



	private float timer;

	//direction is the direction the player is moving in
	private Vector3 direction  = Vector3.zero;

	//moveX and moveZ hold information for the Axes on the controller's left stick
	private float moveX;
	private float moveZ;

	private Vector3 rotation = Vector3.zero;

	private float rotateX;
	private float rotateZ;



	// Use this for initialization
	void Start () {
		moveX = 0;
		moveZ = 0;
		timer = Time.time;
	}

	// Update is called once per frame
	void Update () {
		PlayerInput ();
		//Fire ();
		RayCastForWalls ();
		MovePlayer ();
	}

	//Move controls the player's movement and is called every Update
	private void PlayerInput() {

		//Position And Movement

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

		//Rotation and Firing

		rotateX = XCI.GetAxis (XboxAxis.RightStickX, controller);
		rotateZ = XCI.GetAxis (XboxAxis.RightStickY, controller);

		Vector3 tempRotationInput = new Vector3 (rotateX, 0, rotateZ).normalized * 10;

		rotation = transform.position + tempRotationInput;

		mesh.transform.LookAt (rotation);

		if (rotateX != 0 || rotateZ != 0) {
			Fire (tempRotationInput);
		}
	}

	private void Fire(Vector3 bulletDirection){
		if (Time.time - timer > timeBetweenShots) {
			timer = Time.time;
			GameObject Bullet = Instantiate (bulletPrefab, bulletSpawn.transform.position, Quaternion.identity, bulletHolder.transform) as GameObject;
			Bullet.GetComponent<Bullet> ().direction = bulletDirection.normalized;
		}
	}

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

	private void MovePlayer(){
		transform.position+=direction / 10;
	}


}