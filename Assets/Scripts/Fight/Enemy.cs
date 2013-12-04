using UnityEngine;
using System.Collections.Generic;

public class Enemy 
{
	public int life;
	public int baseLife;
	public int mana;
	public int baseMana;
	public int defence;

	public float defenceInPercent = 0f;

	public GameController.Rune[] equip = new GameController.Rune[9];

	private Combat combat;
	private GameController gameController;

	// Use this for initialization
	public Enemy (Combat combat, GameController gameController)
	{
		baseLife = Random.Range(300, 600);
		baseMana = Random.Range(100, 200);

		this.combat = combat;
		this.gameController = gameController;
		RandomRunes();
	}

	private void RandomRunes()
	{
		int curAddlife = 0;
		int curAddMana = 0;
		int curDefence = 0;

		for (int i = 0; i < equip.Length; i++)
		{
			int curIndex = Random.Range(0, 3);
			equip[i] = new GameController.Rune(gameController.runes[gameController.runeMeterials[curIndex]]);
			curAddlife += equip[i].life;
			curAddMana += equip[i].mana;
			curDefence += equip[i].defence;
			Debug.Log(gameController.runeMeterials[curIndex]);
		}
		defence = curDefence;
		defenceInPercent = (float)defence / (float)GameController.MAXDEFENCE;
		SetStats(curAddlife, curAddMana);
	}

	public void Attack()
	{
		List<GameController.Rune> activeCombination = new List<GameController.Rune>();
		int curIndex = 0;
		int count = 0;
		int numberUsableRunes = 0;

		numberUsableRunes = UsableRunes();
		if (numberUsableRunes >= 4)
		{
			curIndex = Random.Range(2, 4);
		}
		else
		{
			if (numberUsableRunes < 2)
			{
				Debug.Log("Can`t Attack!!");
				return;
			}

			Random.Range(2, numberUsableRunes);
		}
		


		while (count <= curIndex)
		{
			int randomRuneNumber = Random.Range(0, 8);
			//Debug.Log("" + randomRuneNumber + " / " + curIndex + " / " + count + " / " + activeCombination.Contains(equip[randomRuneNumber]) + " / " + equip[randomRuneNumber].uses);
			if (!activeCombination.Contains(equip[randomRuneNumber]) && equip[randomRuneNumber].uses > 0)
			{
				activeCombination.Add(equip[randomRuneNumber]);
				count++;
			}
		}

		combat.DoAttack(activeCombination, Combat.BarNames.PlayerLifeBar);
	}

	private int UsableRunes()
	{
		int numberUsableRunes = 0;
		foreach (GameController.Rune rune in equip)
		{
			if (rune.uses > 0)
			{
				numberUsableRunes++;
			}
		}
		return numberUsableRunes;
	}

	private void SetStats(int additionLife = 0, int additinalMana = 0)
	{
		life = baseLife + additionLife;
		mana = baseMana + additinalMana;

		combat.SetStatsToBars(Combat.BarNames.EnemyLifeBar, life);
		combat.SetStatsToBars(Combat.BarNames.EnemyManaBar, mana);
	}

	// Update is called once per frame
	void Update () {
	}
}
