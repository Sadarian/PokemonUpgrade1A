using UnityEngine;
using System.Collections.Generic;

public class Combine : MonoBehaviour 
{
	public List<GameObject> slots = new List<GameObject>();

	public Dictionary<Combination, GameController.Materials> optionalCombinations = new Dictionary<Combination, GameController.Materials>();
	public List<Combination> combinations = new List<Combination>();

	public JSONObject jsonCobinationList;

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

	void Awake ()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		TextAsset textAsset = (TextAsset)Resources.Load("UpgradeTree", typeof(TextAsset));
		if (textAsset == null) { Debug.LogError("Missing Resources/UpgradeTree.txt !"); return; };
		//Debug.Log(textAsset);
		jsonCobinationList = JSONParser.parse(textAsset.text);
		//Debug.Log("Waves loaded:" + jsonCobinations["Name"]);

		JSONObject jsonComb = jsonCobinationList["Combinations"];

		for (int i = 0; i < gameController.runes.Count; i++ )
		{
			combinations.Add(CombinationFromJson(jsonComb, gameController.runes[i]));
			optionalCombinations.Add(combinations[i], gameController.runes[i]);
		}
	}

	private Combination CombinationFromJson(JSONObject jsonComb, GameController.Materials material)
	{
		int[] comb = new int[4];
		JSONObject jList = jsonComb[material.ToString()];
		for (int i = 0; i < jList.Count; i++)
		{
			comb[i] = (int)jList[i];
		}

		return new Combination(comb);
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

	void Update () {
	
	}
}
