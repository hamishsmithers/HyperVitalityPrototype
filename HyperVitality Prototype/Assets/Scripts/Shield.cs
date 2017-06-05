using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	//myParent is a reference to the player that this instance is sheilding
	private GameObject myParent;

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
	void Update(){
		if (myParent != null) {
			transform.position = myParent.transform.position;
		}
	
	}

	//----------------------------------------------------------------------------------------------------
	//Initilise()
	//	Called with instantiation on Player. Sets destroy time and 'parent'
	// 
	//	Param:
	//		GameObject newParent - who this instance will shield
	//		float duration - how long this instance will last for
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void Initilise(GameObject newParent, float duration){
		myParent = newParent;
		Destroy (this.gameObject, duration);
	}


}
