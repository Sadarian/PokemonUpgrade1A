﻿using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public enum Materials
	{
		Air,
		Earth,
		Fire,
		Water,
		GreatAir,
		GreatEarth,
		GreatFire,
		GreatWater
	}
	public Materials material;

	public Dictionary<string, Materials> spriteToElement = new Dictionary<string, Materials>();
	public Dictionary<Materials, string> elementToSprite = new Dictionary<Materials, string>();
	public Dictionary<Materials, Rune> runes = new Dictionary<Materials, Rune>();

	public List<Materials> runeMeterials = new List<Materials>();
	
	public JSONObject jsonRuneList; 

	public int fireComponents;
	public int airComponents;
	public int earthComponents;
	public int waterComponents;
	public int greatFireComponents;
	public int greatAirComponents;
	public int greatWaterComponents;
	public int greatEarthComponents;

	public ResourcesGrabbing resourcesGrabber;
	public GlyphGrabbing glyphGrabber;

	public int life = 0;
	public int mana = 0;
	public int defence = 0;

	private const int BASELIFE = 500;
	private const int BASEMANA = 125;
	private const int COMPONENTVALVE = 50;
	private bool init = false;

	public class Rune
	{
		public int level = 0;
		public int damage = 0;
		public int manaCost = 0;
		public int mana = 0;
		public int life = 0;
		public int defence = 0;
		public int uses = 0;

		public Rune(int damage, int manaCost, int mana, int life, int defence, int uses, int level)
		{
			this.damage = damage;
			this.manaCost = manaCost;
			this.mana = mana;
			this.life = life;
			this.defence = defence;
			this.uses = uses;
			this.level = level;
		}

		public Rune(JSONObject runeValves, int level)
		{
			damage = (int)runeValves["Damage"];
			manaCost = (int)runeValves["Manacost"];
			mana = (int)runeValves["Mana"];
			life = (int)runeValves["Life"];
			defence = (int)runeValves["Defence"];
			uses = (int)runeValves["Uses"];
			this.level = level;
		}
	}

	#region Init

	private void Awake()
	{
		fireComponents = (int) (Random.value*COMPONENTVALVE + 1);
		airComponents = (int) (Random.value*COMPONENTVALVE + 1);
		earthComponents = (int) (Random.value*COMPONENTVALVE + 1);
		waterComponents = (int) (Random.value*COMPONENTVALVE + 1);
		greatFireComponents = 5;
		greatAirComponents = 5;
		greatWaterComponents = 5;
		greatEarthComponents = 5;

		resourcesGrabber = GameObject.FindGameObjectWithTag("Resources").GetComponent<ResourcesGrabbing>();
		glyphGrabber = GameObject.FindGameObjectWithTag("Glyph").GetComponent<GlyphGrabbing>();

		runeMeterials.Add(Materials.GreatAir);
		runeMeterials.Add(Materials.GreatEarth);
		runeMeterials.Add(Materials.GreatFire);
		runeMeterials.Add(Materials.GreatWater);

		spriteToElement.Add("spell-2", Materials.Fire);
		spriteToElement.Add("spell-6", Materials.Air);
		spriteToElement.Add("spell-8", Materials.Earth);
		spriteToElement.Add("spell-9", Materials.Water);
		spriteToElement.Add("spell-fire", Materials.GreatFire);
		spriteToElement.Add("spell-air", Materials.GreatAir);
		spriteToElement.Add("spell-water", Materials.GreatWater);
		spriteToElement.Add("spell-earth", Materials.GreatEarth);

		elementToSprite.Add(Materials.Fire, "spell-2");
		elementToSprite.Add(Materials.Air, "spell-6");
		elementToSprite.Add(Materials.Earth, "spell-8");
		elementToSprite.Add(Materials.Water, "spell-9");
		elementToSprite.Add(Materials.GreatFire, "spell-fire");
		elementToSprite.Add(Materials.GreatAir, "spell-air");
		elementToSprite.Add(Materials.GreatWater, "spell-water");
		elementToSprite.Add(Materials.GreatEarth, "spell-earth");

		TextAsset textAsset = (TextAsset) Resources.Load("UpgradeTree", typeof (TextAsset));
		if (textAsset == null)
		{
			Debug.LogError("Missing Resources/UpgradeTree.txt !");
			return;
		}
		;
		//Debug.Log(textAsset);
		jsonRuneList = JSONParser.parse(textAsset.text);
		//Debug.Log("Waves loaded:" + jsonCobinations["Name"]);

		JSONObject jsonRunes = jsonRuneList["Runes"];
		FillRuneDictonary(jsonRunes);
	}

	private void FillRuneDictonary(JSONObject jsonRunes)
	{
		foreach (Materials runeMeterial in runeMeterials)
		{
			JSONObject curJsonRune = jsonRunes[runeMeterial.ToString()];
			for (int i = 0; i < curJsonRune.Count; i++)
			{
				runes.Add(runeMeterial, new Rune(curJsonRune[i], i + 1));
			}
		}
	}

	#endregion

	#region Glyphen/Components

	private void Init()
	{
		ComponentValue();
	}

	private void ComponentValue()
	{
		resourcesGrabber.fireCompCounter.Text = fireComponents.ToString();
		resourcesGrabber.airCompCounter.Text = airComponents.ToString();
		resourcesGrabber.earthCompCounter.Text = earthComponents.ToString();
		resourcesGrabber.waterCompCounter.Text = waterComponents.ToString();
		GlyphenValve();
	}

	private void GlyphenValve()
	{
		resourcesGrabber.greatFireCompCounter.Text = greatFireComponents.ToString();
		glyphGrabber.greatFireCompCounter.Text = greatFireComponents.ToString();
		resourcesGrabber.greatAirCompCounter.Text = greatAirComponents.ToString();
		glyphGrabber.greatAirCompCounter.Text = greatAirComponents.ToString();
		resourcesGrabber.greatWaterCompCounter.Text = greatWaterComponents.ToString();
		glyphGrabber.greatWaterCompCounter.Text = greatWaterComponents.ToString();
		resourcesGrabber.greatEarthCompCounter.Text = greatEarthComponents.ToString();
		glyphGrabber.greatEarthCompCounter.Text = greatEarthComponents.ToString();
	}

	#endregion

	public void SetSats(int additionalLife, int additionalMana, int defence)
	{
		life = BASELIFE + additionalLife;
		mana = BASEMANA + additionalMana;
		this.defence = defence;
		GameObject.FindGameObjectWithTag("Combat").GetComponent<Combat>().SetStatsToBars(Combat.BarNames.PlayerLifeBar, life);
		GameObject.FindGameObjectWithTag("Combat").GetComponent<Combat>().SetStatsToBars(Combat.BarNames.PlayerManaBar, mana);
	}

	public int HandleDrag(Materials dragedRecourse, int count)
	{
		int temp = 0;
		switch (dragedRecourse)
		{
			case Materials.Fire:
				{
					fireComponents += count;
					temp = fireComponents;
					break;
				}
			case Materials.Air:
				{
					airComponents += count;
					temp = airComponents;
					break;
				}
			case Materials.Earth:
				{
					earthComponents += count;
					temp = earthComponents;
					break;
				}
			case Materials.Water:
				{
					waterComponents += count;
					temp = waterComponents;
					break;
				}
			case Materials.GreatFire:
				{
					greatFireComponents += count;
					temp = greatFireComponents;
					break;
				}
			case Materials.GreatAir:
				{
					greatAirComponents += count;
					temp = greatAirComponents;
					break;
				}
			case Materials.GreatWater:
				{
					greatWaterComponents += count;
					temp = greatWaterComponents;
					break;
				}
			case Materials.GreatEarth:
				{
					greatEarthComponents += count;
					temp = greatEarthComponents;
					break;
				}
		}
		ComponentValue();
		return temp;
	}


	void Update ()
	{
		if (!init) Init();
	}
}
