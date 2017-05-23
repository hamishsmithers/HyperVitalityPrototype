using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour {

	public GameObject player;

	public void TakeDamage(int damage){
		player.GetComponent<PlayerController> ().TakeDamage (damage);
	}
}
