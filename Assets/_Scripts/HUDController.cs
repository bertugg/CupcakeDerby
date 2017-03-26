using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour {
	[SerializeField]
	public HPBarController[] hpBars;

	public void createHUD(List<bool> players, int maxHP)
	{
		for(int i = 0; i < hpBars.Length; ++i)
		{
			// Reset Phase
			hpBars [i].gameObject.SetActive (false);
			// Create Phase
			if (players[i]) {
				hpBars [i].gameObject.SetActive (true);
				hpBars [i].createHPBar (maxHP, "Player " + i);
			}
		}
	}
	public void updateHPBar(int playerNumber, int hp)
	{
		if (hpBars [playerNumber].gameObject.activeSelf)
			hpBars [playerNumber].UpdateHPBar (hp);
	}
}