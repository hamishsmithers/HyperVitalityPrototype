using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public PlayerController player1;
	public PlayerController player2;

	public Text player1Text;
	public Text player2Text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player1 != null && player2 != null) {
			if (player1.health < 1) {
				Destroy (player1.gameObject);
				player1Text.text = "Defeated";
				player2Text.text = "Champion";
			}
		}
		if (player1 != null && player2 != null) {
			if (player2.health < 1) {
				Destroy (player2.gameObject);
				player2Text.text = "Defeated";
				player1Text.text = "Champion";
			}
		}


		if (player1 != null && player2 != null) {
			player1Text.text = player1.health.ToString ();
			player2Text.text = player2.health.ToString ();
		}
	}
}
