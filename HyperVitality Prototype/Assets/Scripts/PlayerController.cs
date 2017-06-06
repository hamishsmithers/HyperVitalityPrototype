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
	//bulletSpawnMinus is a reference to the place on the ship additional bullets with a smaller Y rotation will spawn
	public GameObject bulletSpawnMinus;
	//bulletSpawnMinus is a reference to the place on the ship additional bullets with a greater Y rotation will spawn
	public GameObject bulletSpawnPlus;

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

	//fireTimer is the time at which the player can fire again
	private float fireTimer;

	//direction is the direction the player is moving in
	private Vector3 direction  = Vector3.zero;

	//rotation is the direction the ship is facing
	private Vector3 rotation = Vector3.zero;

	//attackSpeedTimeBetweenShots is the time between this player's shots in seconds should the attackSpeed powerup be active
	public float attackSpeedTimeBetweenShots;
	//attackSpeed is whether or not the player has the attackSpeed powerup.
	private bool attackSpeed = false;
	//attackSpeedTimer is the time at which the attackSpeed powerup runs out.
	private float attackSpeedTimer;
	//attackSpeedDuration is how long the attackSpeed powerup lasts for.
	private float attackSpeedDuration;

	//tripleShot is whether or not the player has the tripleShot powerup
	private bool tripleShot = false;
	//tripleShotTimer is the time at which the tripleShot powerup runs out
	private float tripleShotTimer;
	//tripleShotDuration is how long the tripleShot powerup lasts for
	private float tripleShotDuration;

	//shield is a reference to the shield prefab
	public GameObject shield;
	//shielded is whether or not the player is shielded
	private bool shielded = false;
	//shieldTimer is the time at which the shield powerup runs out
	private float shieldTimer;
	//shieldDuration is how long the shield lasts for
	private float shieldDuration;

	//particleSystem is a reference to the particles used for damage
	public ParticleSystem particleSystem;

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
		fireTimer = Time.time;
		attackSpeedTimer = Time.time;
		tripleShotTimer = Time.time;
		shieldTimer = Time.time;
	}

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
	void Awake(){
		particleSystem.Stop ();
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
		CheckPowerups ();
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
		}
		if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.2f) {
			Fire ();
		}
	}


	//----------------------------------------------------------------------------------------------------
	//CheckPowerups()
	//	Called every Update(). Disables powerups if their timer has run out.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	private void CheckPowerups(){
		if (Time.time - attackSpeedTimer > attackSpeedDuration) {
			attackSpeed = false;
		}
		if (Time.time - tripleShotTimer > tripleShotDuration) {
			tripleShot = false;
		}
		if (Time.time - shieldTimer > shieldDuration) {
			shielded = false;
		}
	}

	//----------------------------------------------------------------------------------------------------
	//Fire()
	//	Called from PlayerInput() if player inputs attacks. Fires bullets if sufficient time has passed.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	private void Fire(){
		if (attackSpeed) {
			if (Time.time - fireTimer > attackSpeedTimeBetweenShots) {
				fireTimer = Time.time;
				//Bullet is a new instance of a bullet
				GameObject Bullet = Instantiate (bulletPrefab, bulletSpawn.transform.position, Quaternion.identity, bulletParent.transform) as GameObject;
				Bullet.GetComponent<Bullet> ().direction = bulletSpawn.transform.forward;
				Bullet.GetComponent<Bullet> ().speed = bulletSpeed + (currentSpeed / 3);
				if (tripleShot) {
					//BulletMinus is a new instance of a bullet, with a lesser Y rotation
					GameObject BulletMinus = Instantiate (bulletPrefab, bulletSpawnMinus.transform.position, Quaternion.identity, bulletParent.transform) as GameObject;
					BulletMinus.GetComponent<Bullet> ().direction = bulletSpawnMinus.transform.forward;
					BulletMinus.GetComponent<Bullet> ().speed = bulletSpeed + (currentSpeed / 3);
					//BulletPlus is a new instance of a bullet, with a greater Y rotation
					GameObject BulletPlus = Instantiate (bulletPrefab, bulletSpawnPlus.transform.position, Quaternion.identity, bulletParent.transform) as GameObject;
					BulletPlus.GetComponent<Bullet> ().direction = bulletSpawnPlus.transform.forward;
					BulletPlus.GetComponent<Bullet> ().speed = bulletSpeed + (currentSpeed / 3);
				}
			}
		} else {
			if (Time.time - fireTimer > timeBetweenShots) {
				fireTimer = Time.time;
				//Bullet is a new instance of a bullet
				GameObject Bullet = Instantiate (bulletPrefab, bulletSpawn.transform.position, Quaternion.identity, bulletParent.transform) as GameObject;
				Bullet.GetComponent<Bullet> ().direction = bulletSpawn.transform.forward;
				Bullet.GetComponent<Bullet> ().speed = bulletSpeed + (currentSpeed / 3);
				if (tripleShot) {
					//BulletMinus is a new instance of a bullet, with a lesser Y rotation
					GameObject BulletMinus = Instantiate (bulletPrefab, bulletSpawnMinus.transform.position, Quaternion.identity, bulletParent.transform) as GameObject;
					BulletMinus.GetComponent<Bullet> ().direction = bulletSpawnMinus.transform.forward;
					BulletMinus.GetComponent<Bullet> ().speed = bulletSpeed + (currentSpeed / 3);
					//BulletPlus is a new instance of a bullet, with a greater Y rotation
					GameObject BulletPlus = Instantiate (bulletPrefab, bulletSpawnPlus.transform.position, Quaternion.identity, bulletParent.transform) as GameObject;
					BulletPlus.GetComponent<Bullet> ().direction = bulletSpawnPlus.transform.forward;
					BulletPlus.GetComponent<Bullet> ().speed = bulletSpeed + (currentSpeed / 3);
				}
			}
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
		if (shielded == false) {
			health -= damage;
			particleSystem.Play ();
		}
	}
	//----------------------------------------------------------------------------------------------------
	//Powerup()
	//	Called from PlayerMesh's Powerup(). Activates a powerup.
	// 
	//	Param:
	//		int powerup - which powerup this is
	//		float duration - how long this powerup lasts for
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void Powerup(int powerup, float duration){
		if (powerup == 0) {
			attackSpeed = true;
			attackSpeedTimer = Time.time;
			attackSpeedDuration = duration;
		} else if (powerup == 1) {
			tripleShot = true;
			tripleShotTimer = Time.time;
			tripleShotDuration = duration;
		} else if (powerup == 2) {
			//GO is a reference to the newly created shield
			GameObject GO = Instantiate(shield, this.transform.position, Quaternion.identity);
			GO.GetComponent<Shield> ().Initilise (this.gameObject, duration);
			shielded = true;
			shieldTimer = Time.time;
			shieldDuration = duration;
		}
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
		transform.position+=direction * Time.deltaTime;
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