using UnityEngine;
using System.Collections.Generic;

public class Combine : MonoBehaviour 
{
	public List<GameObject> slots = new List<GameObject>();
	
	private GameController gameController;
	private List<GameController.Materials> combinatonElements = new List<GameController.Materials>();

	// Use this for initialization
	void Awake ()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
	{
		foreach (GameObject slot in slots)
		{
			dfSprite curSprite = slot.GetComponent<dfSprite>();

			if (curSprite.SpriteName != "")
				{
					combinatonElements.Add(gameController.spriteElement[curSprite.SpriteName]);
				}
		}

		CombineElements();
	}

	private void CombineElements()
	{
		if (combinatonElements.Count == 3)
		{
			foreach (GameObject slot in slots)
			{
				dfSprite curSprite = slot.GetComponent<dfSprite>();

				curSprite.SpriteName = "";
			}

			gameController.HandleDrag(GameController.Materials.GreatFire, 1);
		}

		combinatonElements.Clear();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
