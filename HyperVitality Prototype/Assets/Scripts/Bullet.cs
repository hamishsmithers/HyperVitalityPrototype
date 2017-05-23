using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Vector3 direction;

	public float speed;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += direction*speed/10;
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Asteroid") {
			other.gameObject.GetComponent<Asteroid> ().Break (true);
			Destroy (this.gameObject);
		} else if (other.gameObject.tag == "Boundary") {
			Destroy (this.gameObject);
		} else if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerMesh>().TakeDamage(5);
			Destroy(this.gameObject);
		}
	}
}
