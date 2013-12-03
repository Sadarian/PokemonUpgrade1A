using UnityEngine;
using System.Collections.Generic;

public class Combine : MonoBehaviour 
{
	public List<GameObject> slots = new List<GameObject>();

	public Dictionary<Combination, GameController.Materials> optionalCombinations = new Dictionary<Combination, GameController.Materials>();
	public List<Combination> combinations = new List<Combination>();

	public GameObject endProdukt;

	public JSONObject jsonCobinationList;

	private GameController gameController;
	private Combination curCombination = new Combination(0,0,0,0);

	private bool newCombination = false;

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

		for (int i = 0; i < gameController.runeMeterials.Count; i++ )
		{
			combinations.Add(CombinationFromJson(jsonComb, gameController.runeMeterials[i]));
			optionalCombinations.Add(combinations[i], gameController.runeMeterials[i]);
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

	public void FillSlot()
	{
		curCombination.Clear();
		foreach (GameObject slot in slots)
		{
			dfSprite curSprite = slot.GetComponent<dfSprite>();

			if (curSprite.SpriteName != "")
			{
				switch (gameController.spriteToElement[curSprite.SpriteName])
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
				newCombination = true;
			}
		}
	}

	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
	{
		gameController.HandleDrag(gameController.spriteToElement[endProdukt.GetComponent<dfSprite>().SpriteName], 1);
		Clear();
	}

	private void Clear()
	{
		foreach (GameObject slot in slots)
		{
			dfSprite curSprite = slot.GetComponent<dfSprite>();

			curSprite.SpriteName = "";
		}
		endProdukt.GetComponent<dfSprite>().SpriteName = "";
	}

	void Update () {
		if (newCombination)
		{
			foreach (Combination combination in combinations)
			{
				if (combination == curCombination)
				{
					endProdukt.GetComponent<dfSprite>().SpriteName = gameController.elementToSprite[optionalCombinations[combination]];
				}
			}
			newCombination = false;
		}
	}
}
