using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	//player1 is a reference to the object the first player controls
	public PlayerController player1;
	//player2 is a reference to the object the second player controls
	public PlayerController player2;

	//player1Text is a reference to the text used to display player 1's health
	public Text player1Text;
	//player2Text is a reference to the text used to display player 2's health
	public Text player2Text;

	//winner detects which player has won
	private int winner = 0;

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
		if (winner==0) {
			if (player1.health < 1) {
				Destroy (player1.gameObject);
				player1Text.text = "Defeated";
				player2Text.text = "Champion";
				winner = 1;
			}
		}
		if (winner==0) {
			if (player2.health < 1) {
				Destroy (player2.gameObject);
				player2Text.text = "Defeated";
				player1Text.text = "Champion";
				winner = 2;
			}
		}


		if (player1 != null && player2 != null && winner ==0) {
			player1Text.text = player1.health.ToString ();
			player2Text.text = player2.health.ToString ();

		}
	}
}
