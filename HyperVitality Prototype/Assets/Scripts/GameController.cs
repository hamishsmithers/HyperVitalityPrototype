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

	//endScreen is a reference to the buttons and text that appears after the game ends
	public GameObject endScreen;

	//startScreem is a reference to the start screen
	public GameObject startScreen;

	//asteroidControl is a reference to the asteroid controller
	public GameObject asteroidControl;

	//winner detects which player has won
	private int winner = 0;

	//started is whther or not the game has started
	private bool started = false;


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
	void Start(){
		Invoke ("StartGame", 1f);
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
		if (started) {
			TrackHealth ();
		}
	}

	//----------------------------------------------------------------------------------------------------
	//TrackHealth()
	//	Called every Update(). Tracks and displays health of players
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	private void TrackHealth(){
		if (winner==0) {
			if (player1.health < 1) {
				Destroy (player1.gameObject);
				player1Text.text = "Defeated";
				player2Text.text = "Champion";
				winner = 1;
				Invoke ("EnableEndScreen", 3f);
			}
		}
		if (winner==0) {
			if (player2.health < 1) {
				Destroy (player2.gameObject);
				player2Text.text = "Defeated";
				player1Text.text = "Champion";
				winner = 2;
				Invoke ("EnableEndScreen", 3f);
			}
		}


		if (player1 != null && player2 != null && winner ==0) {
			player1Text.text = player1.health.ToString ();
			player2Text.text = player2.health.ToString ();

		}
	}

	//----------------------------------------------------------------------------------------------------
	//EnableEndScreen()
	//	Called upon Victory. Enables the end screen.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	private void EnableEndScreen(){
		endScreen.SetActive (true);
	}


	//----------------------------------------------------------------------------------------------------
	//StartGame()
	//	Called shortly after start. Enables players and asteroids.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	private void StartGame(){
		startScreen.SetActive (false);
		player1.gameObject.SetActive (true);
		player2.gameObject.SetActive (true);
		asteroidControl.SetActive (true);
		player1Text.gameObject.SetActive (true);
		player2Text.gameObject.SetActive (true);
		player1Text.text = "500";
		player2Text.text = "500";
		started = true;
	}

}
