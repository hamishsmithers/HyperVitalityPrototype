using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	//myParent is a reference to who this instance is shielding and following
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
	//	Called alongside instantiation. Initlise this instance.
	// 
	//	Param:
	//		GameObject newParent - who the shield will be shielding
	//		float duration - how long this instance will last for
	//
	//	Return:
	//		Void
	//----------------------------------------------------------------------------------------------------
	public void Initialise(GameObject newParent, float duration) {
		Destroy (this.gameObject, duration);
		myParent = newParent;
	}


}
