using UnityEngine;
using System.Collections.Generic;

public class Skill : MonoBehaviour {

	public List<dfSprite> equipSlots = new List<dfSprite>();
	public GameController.Rune[] equip = new GameController.Rune[9];
	public List<dfSprite> activeCombination = new List<dfSprite>();

	private GameController gameController;

	private const float DISTANCE = 45f;

	void Awake () {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	public void Init(List<dfSprite> eq)
	{
		for (int i = 0; i < eq.Count; i++)
		{
			equipSlots.Add(eq[i]);
		}
	}

	public void SetEquip(List<dfSprite> eq)
	{
		int curAddlife = 0;
		int curAddMana = 0;

		for (int i = 0; i < eq.Count; i++)
		{
			if (eq[i].SpriteName != "")
			{
				equip[i] = gameController.runes[gameController.spriteToElement[eq[i].SpriteName]];
				eq[i].gameObject.GetComponent<EquipSlot>().rune = equip[i];
				curAddlife += equip[i].life;
				curAddMana += equip[i].mana;
			}
		}
		gameController.SetLife(curAddlife);
		gameController.SetMana(curAddMana);
	}

	public void OnMouseMove(dfControl control, dfMouseEventArgs mouseEvent)
	{
		//mouseEvent.Use();
		for (int i = 0; i < equipSlots.Count; i++)
		{
			GameController.Rune curRune = equipSlots[i].gameObject.GetComponent<EquipSlot>().rune;
			if (curRune != null)
			{
				Vector2 runeSlotCenter = new Vector2(equipSlots[i].Position.x + equipSlots[i].Size.x / 2, equipSlots[i].Position.y - equipSlots[i].Size.y / 2);

				float dist = (mouseEvent.Position - runeSlotCenter).magnitude;
				Debug.Log("slot " + (i + 1) + runeSlotCenter + " distance " + dist);

				if (!activeCombination.Contains(equipSlots[i]) && dist <= DISTANCE)
				{
					activeCombination.Add(equipSlots[i]);
				}
			}
		}
	}

	public void OnMouseUp(dfControl control, dfMouseEventArgs mouseEvent)
	{
		//Debug.Log("Mouse Up!");


		//if (activeCombination.Count == 4)
		//{
		//	GameObject.FindGameObjectWithTag("GameController").GetComponent<Combat>().Attack(activeCombination);
		//	foreach (dfButton button in activeCombination)
		//	{
		//		button.State = dfButton.ButtonState.Default;
		//	}
		//	activeCombination.Clear();
		//}
	}
	
	void Update () {
	
	}
}
