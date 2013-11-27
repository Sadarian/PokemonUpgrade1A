using UnityEngine;
using System.Collections.Generic;

public class EquipHandle : MonoBehaviour {
	public List<dfSprite> equipSlots = new List<dfSprite>();
	public List<dfSprite> toEquippingSlots = new List<dfSprite>();

	private GameController gameController;

	void Awake ()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		for (int i = 1; i <= 9; i++ )
		{
			equipSlots.Add(transform.FindChild("Slot "+i).GetChild(0).GetComponent<dfSprite>());
		}

		for (int i = 1; i <= 9; i++)
		{
			toEquippingSlots.Add(GameObject.FindGameObjectWithTag("Skill").transform.FindChild("Skill Slot " + i).GetChild(0).GetComponent<dfSprite>());
		}
	}

	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
	{
		for (int i = 0; i < equipSlots.Count; i++)
		{
			toEquippingSlots[i].SpriteName = equipSlots[i].SpriteName;
		}
	}

	void Update () {
	
	}
}
