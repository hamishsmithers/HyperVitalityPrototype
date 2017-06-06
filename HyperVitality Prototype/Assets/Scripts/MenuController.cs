using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

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
