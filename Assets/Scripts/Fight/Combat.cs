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
		enemy = new Enemy(this);
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
