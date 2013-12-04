using UnityEngine;
using System.Collections.Generic;

public class Skill : MonoBehaviour {

	public List<dfSprite> equipSlots = new List<dfSprite>();
	public GameController.Rune[] equip = new GameController.Rune[9];
	public List<GameController.Rune> activeCombination = new List<GameController.Rune>();

	private GameController gameController;

	private const float DISTANCE = 55f;

	void Awake () {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	public void Init(List<dfSprite> eq)
	{
		for (int i = 0; i < eq.Count; i++)
		{
			equipSlots.Add(eq[i]);
			equipSlots[i].transform.GetChild(0).GetComponent<dfSprite>().Hide();
		}
	}

	public void SetEquip(List<dfSprite> eq)
	{
		int curAddlife = 0;
		int curAddMana = 0;
		int curDefence = 0;

		for (int i = 0; i < eq.Count; i++)
		{
			if (eq[i].SpriteName != "")
			{
				equip[i] = new GameController.Rune(gameController.runes[gameController.spriteToElement[eq[i].SpriteName]]);
				eq[i].gameObject.GetComponent<EquipSlot>().rune = equip[i];
				curAddlife += equip[i].life;
				curAddMana += equip[i].mana;
				curDefence += equip[i].defence;
				if(!equipSlots[i].IsEnabled) equipSlots[i].Enable();
			}
		}
		gameController.SetSats(curAddlife, curAddMana, curDefence);
	}

	public void OnMouseMove(dfControl control, dfMouseEventArgs mouseEvent)
	{
		if (activeCombination.Count == 4 || gameController.curTurn == GameController.Turns.Enemy)return;

		for (int i = 0; i < equipSlots.Count; i++)
		{
			dfControl curSlot = equipSlots[i].transform.parent.GetComponent<dfControl>();
			GameController.Rune curRune = equipSlots[i].gameObject.GetComponent<EquipSlot>().rune;
			if (curRune != null)
			{
				Vector2 runeSlotCenter = new Vector2(curSlot.GetGUIScreenPos().x, curSlot.GetGUIScreenPos().y);

				float dist = (curSlot.GetManager().ScreenToGui(Input.mousePosition) - runeSlotCenter).magnitude;
				//Debug.Log("slot " + (i + 1) + runeSlotCenter + " distance " + dist);

				if (!activeCombination.Contains(equip[i]) && equip[i].uses > 0 && dist <= DISTANCE)
				{
					activeCombination.Add(equip[i]);
					equipSlots[i].transform.GetChild(0).GetComponent<dfSprite>().Show();
				}
			}
		}
	}

	public void OnMouseUp(dfControl control, dfMouseEventArgs mouseEvent)
	{
		if (activeCombination.Count > 1)
		{
			GameObject.FindGameObjectWithTag("Combat").GetComponent<Combat>().DoAttack(activeCombination, Combat.BarNames.EnemyLifeBar);

			for (int i = 0; i < equipSlots.Count; i++)
			{
				equipSlots[i].transform.GetChild(0).GetComponent<dfSprite>().Hide();

				if (equip[i] != null && equip[i].uses == 0)
				{
					equipSlots[i].Disable();	
				}
			}
			activeCombination.Clear();
		}
	}
	
	void Update () {
	
	}
}

public static class dfControlExtension
{
	public static Vector3 GetGUIScreenPos(this dfControl self)
	{
		dfControl parent = self.Parent;
		Vector3 pos = self.RelativePosition;
		while (parent != null)
		{
			pos += parent.RelativePosition;
			parent = parent.Parent;
		}
		pos += new Vector3(self.Width / 2, self.Height / 2, 0);
		return pos;
	}

	public static void SetGUIScreenPos(this dfControl self, Vector3 pos)
	{
		dfControl parent = self.Parent;
		Vector3 offset = new Vector3();
		while (parent != null)
		{
			offset += parent.RelativePosition;
			parent = parent.Parent;
		}
		pos -= offset;
		pos -= new Vector3(self.Width / 2, self.Height / 2, 0);
		self.RelativePosition = pos;
	}
}
