using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour {
	
	public GameObject player1Joined;
	public GameObject player1Press;

	public GameObject player2Joined;
	public GameObject player2Press;

	public GameObject player3Joined;
	public GameObject player3Press;

	public GameObject player4Joined;
	public GameObject player4Press;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			player1Joined.SetActive (true);
			player1Press.SetActive (false);
		}	

		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			player2Joined.SetActive (true);
			player2Press.SetActive (false);
		}

		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			player3Joined.SetActive (true);
			player3Press.SetActive (false);
		}

		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			player4Joined.SetActive (true);
			player4Press.SetActive (false);
		}
	}
}
