using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour {



	//player1AutoFire tracks whether player 1 is using auto fire control scheme
	public bool player1AutoFire = true;
	//player2AutoFire tracks whether player 2 is using auto fire control scheme
	public bool player2AutoFire=true;

	//----------------------------------------------------------------------------------------------------
	//SetToggles()
	//	Called from Menu Controller. Sets toggles to correct state.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void SetToggles(){
		foreach (GameObject toggle in GameObject.FindGameObjectsWithTag("InputToggle")) {
			if (toggle.GetComponent<InputToggle>().playerNumber == 1) {
				toggle.GetComponent<Toggle> ().isOn = player1AutoFire;
			} else if (toggle.GetComponent<InputToggle>().playerNumber == 2) {
				toggle.GetComponent<Toggle> ().isOn = player2AutoFire;
			}
		}
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
		Object.DontDestroyOnLoad (this.gameObject);
	}		
}
