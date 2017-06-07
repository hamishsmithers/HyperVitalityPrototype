using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	//inputController is a reference to the input controller
	InputController inputController;

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
		inputController = FindObjectOfType<InputController> ();
		inputController.SetToggles ();
	}

	//----------------------------------------------------------------------------------------------------
	//TogglePlayer1
	//	Called from toggle. Calls InputController's varient.
	// 
	//	Param:
	//		GameObject toggle - Object that has toggle
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void TogglePlayer1(GameObject toggle){
		if (toggle.GetComponent<Toggle>().isOn) {
			inputController.player1AutoFire = true;
		} else if (!toggle.GetComponent<Toggle>().isOn) {
			inputController.player1AutoFire = false;
		}
	}

	//----------------------------------------------------------------------------------------------------
	//TogglePlayer2
	//	Called from toggle. Calls InputController's varient.
	// 
	//	Param:
	//		GameObject toggle - Object that has toggle
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void TogglePlayer2(GameObject toggle){
		if (toggle.GetComponent<Toggle>().isOn) {
			inputController.player2AutoFire = true;
		} else if (!toggle.GetComponent<Toggle>().isOn) {
			inputController.player2AutoFire = false;
		}
	}
	//----------------------------------------------------------------------------------------------------
	//LoadScene()
	//	Called from button. Loads selected scene.
	// 
	//	Param:
	//		string SceneName - name of scene to load
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void LoadScene(string sceneName){
		SceneManager.LoadScene (sceneName);
	}

	//----------------------------------------------------------------------------------------------------
	//QuitGame()
	//	Called from button. Quits application.
	// 
	//	Param:
	//		None
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void QuitGame(){
		Application.Quit ();
	}
		
}
