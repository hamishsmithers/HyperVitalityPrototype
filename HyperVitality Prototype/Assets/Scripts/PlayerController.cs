using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {


	//controller is a reference to the Xbox controller the object uses for input
	public XboxController controller;

	//maxSpeed is the fastest the ship can move, and minSpeed is the slowest.
	public float minSpeed;
	public float maxSpeed;

	public float currentSpeed;

	public float acceleration;

	public float adjustment;


	//direction is the direction the player is moving in
	private Vector3 direction  = Vector3.zero;


	//moveX and moveY hold information for the Axes on the controller's left stick
	private float moveX;
	private float moveY;



	// Use this for initialization
	void Start () {
		moveX = 0;
		moveY = 0;
	}

	// Update is called once per frame
	void Update () {
		Move ();
	}

	//Move controls the player's movement and is called every Update
	private void Move() {

		moveX = XCI.GetAxis (XboxAxis.LeftStickX, controller);
		moveY = XCI.GetAxis (XboxAxis.LeftStickY, controller);

		Vector3 NewVector = new Vector3 (moveX, 0, moveY) * Time.deltaTime * adjustment;

		direction += NewVector;

		if (moveX == 0 && moveY == 0) {
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
			transform.Translate (direction / 10);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Boundary") {
			
		}
	}
}