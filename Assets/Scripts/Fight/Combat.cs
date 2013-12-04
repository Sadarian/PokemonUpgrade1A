using UnityEngine;
using System.Collections.Generic;

public class Combat : MonoBehaviour
{
	public enum BarNames
	{
		EnemyLifeBar,
		EnemyManaBar,
		PlayerLifeBar,
		PlayerManaBar
	}
	public BarNames barName;

	public GameObject[] bars = new GameObject[4];

	public Enemy enemy;

	private GameController gameController;

	void Awake()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		enemy = new Enemy(this, gameController);
		gameController.curTurn = GameController.Turns.Player;
	}

	public void DoAttack(List<GameController.Rune> activRunes, BarNames victim)
	{
		int dealtDamage = 0;
		int curManaCoast = 0;

		for (int i = 0; i < activRunes.Count; i++)
		{
			dealtDamage += activRunes[i].damage;
			curManaCoast += activRunes[i].manaCost;
			activRunes[i].uses--;
		}

		if (victim == BarNames.EnemyLifeBar)
		{
			enemy.life -= (int)(dealtDamage*(1 - enemy.defenceInPercent));
			gameController.mana -= curManaCoast;
		}
		else
		{
			gameController.life -= (int)(dealtDamage * (1 - enemy.defenceInPercent));
			enemy.mana -= curManaCoast;
		}

		UpdateBars();
		SwitchTurns();
	}

	private void SwitchTurns()
	{
		switch (gameController.curTurn)
		{
			case GameController.Turns.Player:
				{
					gameController.curTurn = GameController.Turns.Enemy;
					enemy.Attack();
					break;
				}
			case GameController.Turns.Enemy:
				{
					gameController.curTurn = GameController.Turns.Player;
					break;
				}
		}
	}

	private void UpdateBars()
	{
		bars[0].GetComponent<dfProgressBar>().Value = enemy.life;
		bars[1].GetComponent<dfProgressBar>().Value = enemy.mana;
		bars[2].GetComponent<dfProgressBar>().Value = gameController.life;
		bars[3].GetComponent<dfProgressBar>().Value = gameController.mana;
	}

	public void SetStatsToBars(BarNames bar, float maxValve)
	{
		dfProgressBar curprogessbar = bars[(int)bar].GetComponent<dfProgressBar>();
		curprogessbar.MaxValue = maxValve;
		curprogessbar.Value = maxValve;
	}


	void Update () {
	
	}
}
