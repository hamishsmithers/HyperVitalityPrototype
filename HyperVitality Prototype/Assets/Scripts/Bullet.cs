using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	//direction is the direction the bullet is moving in
	public Vector3 direction;

	//damage is the damage the bullet will deal to players if hit
	public int damage;

	//speed is the speed at which the bullet moves
	public float speed;

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
		Destroy (this.gameObject, 2);
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
		transform.position += direction*speed * Time.deltaTime;
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
		if (other.gameObject.tag == "Asteroid") {
			other.gameObject.GetComponent<Asteroid> ().Break (true);
			Destroy (this.gameObject);
		} else if (other.gameObject.tag == "Boundary") {
			Destroy (this.gameObject);
		} else if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerMesh> ().TakeDamage (damage);
			Destroy (this.gameObject);
		} 
	}
}
