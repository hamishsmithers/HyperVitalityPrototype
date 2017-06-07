using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputToggle : MonoBehaviour {

	//playerNumber refers to the player this will control the input for;
	public int playerNumber;

	//inputController is a reference to the input controller
	InputController inputController;

	//----------------------------------------------------------------------------------------------------
	//Initialise()
	//	Called with/acts as instantiation from other objects. Instantiate this instance.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void Initialise(){
		Debug.Log ("START ON INPUT TOGGLE");
		inputController = FindObjectOfType<InputController> ();

		if (playerNumber == 1) {
			this.GetComponent<Toggle> ().isOn = inputController.player1AutoFire;
		}else if(playerNumber==2){
			this.GetComponent<Toggle> ().isOn = inputController.player2AutoFire;
		}

	}


}
