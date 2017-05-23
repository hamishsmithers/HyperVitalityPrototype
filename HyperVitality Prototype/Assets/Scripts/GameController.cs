using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;

	public Text player1Text;
	public Text player2Text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		player1Text.text = player1.GetComponent<PlayerController> ().health.ToString();
		player2Text.text = player2.GetComponent<PlayerController> ().health.ToString();

	}
}
