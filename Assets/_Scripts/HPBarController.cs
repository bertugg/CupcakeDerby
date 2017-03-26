using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour {

	[SerializeField]
	private Image hpBarFill;
	[SerializeField]
	private Text playerText;

	private float _hp;
	private float _maxhp;

	public int createHPBar(int maxHP, string name = "Player", int currentHP = -1)
	{
		if (currentHP < -1)
			currentHP = maxHP;
		if (maxHP > 0) {
			_maxhp = (float)maxHP;
			_hp = (float)currentHP;
		}
		playerText.text = name;
		return Mathf.FloorToInt(_maxhp);
	}

	public float UpdateHPBar (int hp)
	{
		this._hp = (float)hp;
		if (_hp > _maxhp)
			_hp = _maxhp;
		else if (_hp >= 0f)
		{
			Debug.Log (_hp + " / " + _maxhp);
			hpBarFill.fillAmount = _hp / _maxhp;
			return hpBarFill.fillAmount;
		}
		return 0f;
	}
}
