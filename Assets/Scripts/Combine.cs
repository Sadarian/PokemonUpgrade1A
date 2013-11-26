using UnityEngine;
using System.Collections.Generic;

public class Combine : MonoBehaviour 
{
	public List<GameObject> slots = new List<GameObject>();

	public Dictionary<Combination, GameController.Materials> optionalCombinations = new Dictionary<Combination, GameController.Materials>();
	public List<Combination> combinations = new List<Combination>();

	private GameController gameController;
	private Combination curCombination = new Combination(0,0,0,0);

	private int counter = 0;

	public class Combination
	{
		public int[] comb = new int[4];

		public Combination(int[] combination)
		{
			for (int i = 0; i < combination.Length; i++)
			{
				comb[i] = combination[i];
			}
		}

		public Combination(int air, int earth, int fire, int water)
		{
			comb[0] = air;
			comb[1] = earth;
			comb[2] = fire;
			comb[3] = water;
		}

		public void Clear()
		{
			for (int i = 0; i < comb.Length; i++)
			{
				comb[i] = 0;
			}
		}

		public static bool operator ==(Combination c1, Combination c2)
		{
			if (c1.comb[0] == c2.comb[0] &&
				c1.comb[1] == c2.comb[1] &&
				c1.comb[2] == c2.comb[2] &&
				c1.comb[3] == c2.comb[3])
			{
				return true;
			}
			return false;
		}

		public static bool operator !=(Combination c1, Combination c2)
		{
			return !(c1 == c2);
		}
	}

	// Use this for initialization
	void Awake ()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		combinations.Add(new Combination(3, 0, 0, 0));
		combinations.Add(new Combination(0, 3, 0, 0));
		combinations.Add(new Combination(0, 0, 3, 0));
		combinations.Add(new Combination(0, 0, 0, 3));

		optionalCombinations.Add(combinations[0], GameController.Materials.GreatAir);
		optionalCombinations.Add(combinations[1], GameController.Materials.GreatEarth);
		optionalCombinations.Add(combinations[2], GameController.Materials.GreatFire);
		optionalCombinations.Add(combinations[3], GameController.Materials.GreatWater);
	}

	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
	{
		foreach (GameObject slot in slots)
		{
			dfSprite curSprite = slot.GetComponent<dfSprite>();

			if (curSprite.SpriteName != "")
			{
				counter++;
				switch (gameController.spriteElement[curSprite.SpriteName])
				{
					case GameController.Materials.Air:
						{
							curCombination.comb[0]++;
							break;
						}
					case GameController.Materials.Earth:
						{
							curCombination.comb[1]++;
							break;
						}
					case GameController.Materials.Fire:
						{
							curCombination.comb[2]++;
							break;
						}
					case GameController.Materials.Water:
						{
							curCombination.comb[3]++;
							break;
						}
				}
			}
		}

		CombineElements();
	}

	private void CombineElements()
	{
		if (counter == 3)
		{
			foreach (Combination combination in combinations)
			{
				if (combination == curCombination)
				{
					gameController.HandleDrag(optionalCombinations[combination], 1);
					Clear();
				}
			}
		}

		curCombination.Clear();
		counter = 0;
	}

	private void Clear()
	{
		foreach (GameObject slot in slots)
		{
			dfSprite curSprite = slot.GetComponent<dfSprite>();

			curSprite.SpriteName = "";
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
