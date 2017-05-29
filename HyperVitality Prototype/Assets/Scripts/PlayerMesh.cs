using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour {

	//player is a reference to the player which controls this mesh
	public GameObject player;

	//----------------------------------------------------------------------------------------------------
	//TakeDamage()
	//	Called from harmful object's GetCollisionEnter(). Sends damage to Player.
	// 
	//	Param:
	//		int damage - damage for player to take.
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void TakeDamage(int damage){
		player.GetComponent<PlayerController> ().TakeDamage (damage);
	}
}
