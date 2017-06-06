using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {




	public bool player1AutoFire = true;

	public bool player2AutoFire=true;



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

	//----------------------------------------------------------------------------------------------------
	//TogglePlayer1
	//	Called from toggle. Toggles player1AutoFire bool.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void TogglePlayer1(){
		if (player1AutoFire) {
			player1AutoFire = false;
		} else if (!player1AutoFire) {
			player1AutoFire = true;
		}
	}
	//----------------------------------------------------------------------------------------------------
	//TogglePlayer2
	//	Called from toggle. Toggles player2AutoFire bool.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void TogglePlayer2(){
		if (player2AutoFire) {
			player2AutoFire = false;
		} else if (!player2AutoFire) {
			player2AutoFire = true;
		}
	}

}
