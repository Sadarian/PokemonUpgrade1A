using UnityEngine;
using System.Collections.Generic;

public class EquipHandle : MonoBehaviour {
	public List<dfSprite> slots = new List<dfSprite>();
	public List<GameController.Materials> curEquip = new List<GameController.Materials>();

	private GameController gameController;

	void Awake ()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
	{
		for (int i = 0; i < slots.Count; i++)
		{
			curEquip.Add(gameController.spriteElement[slots[i].SpriteName]);
		}
	}

	void Update () {
	
	}
}
