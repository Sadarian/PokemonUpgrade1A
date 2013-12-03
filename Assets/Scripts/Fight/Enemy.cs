using UnityEngine;
using System.Collections;

public class Enemy 
{
	public int life;
	public int baseLife;
	public int mana;
	public int baseMana;
	public int defence;

	private Combat combat;

	// Use this for initialization
	public Enemy (Combat combat)
	{
		baseLife = Random.Range(300, 600);
		baseMana = Random.Range(100, 200);

		this.combat = combat;
		SetStats();
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
