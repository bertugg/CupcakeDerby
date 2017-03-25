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
	public List<int> joinedPlayers;

	public void PlayerJoined(int playerNumber)
	{
		Debug.Log (playerNumber + " joined!");
		switch (playerNumber) {
		case 1:
			player1Joined.SetActive (true);
			player1Press.SetActive (false);
			joinedPlayers.Add (1);
			break;
		case 2:
			player2Joined.SetActive (true);
			player2Press.SetActive (false);
			joinedPlayers.Add (2);
			break;
		case 3:
			player3Joined.SetActive (true);
			player3Press.SetActive (false);
			joinedPlayers.Add (3);
			break;
		case 4:
			player4Joined.SetActive (true);
			player4Press.SetActive (false);
			joinedPlayers.Add (4);
			break;
		default:
			break;
		}
	}
}
