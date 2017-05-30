using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour {

	//player is a reference to the player which controls this mesh
	public GameObject player;

	//----------------------------------------------------------------------------------------------------
	//TakeDamage()
	//	Called from harmful object's OnCollisionEnter(). Sends damage to Player.
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

	//----------------------------------------------------------------------------------------------------
	//Powerup()
	//	Called from powerup's OnCollisionEnter(). Sends powerup to Player.
	// 
	//	Param:
	//		int powerup - which powerup this is
	//		float duration - how long this powerup lasts for
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void Powerup(int powerup, float duration){
		player.GetComponent<PlayerController> ().Powerup(powerup, duration);
	}
}
