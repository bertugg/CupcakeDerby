using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour {
	[SerializeField]
	private GameObject player1Joined;
	[SerializeField]
	private GameObject player1Press;

	[SerializeField]
	private GameObject player2Joined;
	[SerializeField]
	private GameObject player2Press;

	[SerializeField]
	private GameObject player3Joined;
	[SerializeField]
	private GameObject player3Press;

	[SerializeField]
	private GameObject player4Joined;
	[SerializeField]
	private GameObject player4Press;

	[HideInInspector]
	public List<bool> joinedPlayers;

	void Start()
	{
		for (int i = 0; i < 4; ++i)
			joinedPlayers.Add (false);
	}

	public void PlayerJoined(int playerNumber)
	{
		Debug.Log (playerNumber + " joined!");
		switch (playerNumber) {
		case 1:
			player1Joined.SetActive (true);
			player1Press.SetActive (false);
			joinedPlayers [0] = true;
			break;
		case 2:
			player2Joined.SetActive (true);
			player2Press.SetActive (false);
			joinedPlayers [1] = true;
			break;
		case 3:
			player3Joined.SetActive (true);
			player3Press.SetActive (false);
			joinedPlayers [2] = true;
			break;
		case 4:
			player4Joined.SetActive (true);
			player4Press.SetActive (false);
			joinedPlayers [3] = true;
			break;
		default:
			break;
		}
	}
}
