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
	public GameObject enemyLifeBar;
	public GameObject enemyManaBar;
	public GameObject playerLifeBar;
	public GameObject playerManaBar;

	public GameObject[] bars = new GameObject[4];

	public Enemy enemy = new Enemy();

	private GameController gameController;

	void Awake()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		Init();
	}

	private void Init()
	{
		bars[0] = enemyLifeBar;
		bars[1] = enemyManaBar;
		bars[2] = playerLifeBar;
		bars[3] = playerManaBar;
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
