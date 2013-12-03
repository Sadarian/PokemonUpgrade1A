using UnityEngine;
using System.Collections.Generic;

public class HandleDrop : MonoBehaviour
{
	public dfSprite sprite;
	public int curChildCount;

	private GameController gameController;
	private Combine combine;
	void Awake () 
	{
		sprite = transform.GetChild(0).GetComponent<dfSprite>();

		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		combine = GameObject.FindGameObjectWithTag("Combine").GetComponent<Combine>();
	}

	public void OnDragDrop(dfControl source, dfDragEventArgs args)
	{
		//Debug.Log("Drop it!");

		HandleDrag curHandleDrag = (HandleDrag)args.Data;
		dfSprite curSprite = curHandleDrag.sprite;

		if (!curHandleDrag.draged)return;

		if (sprite.SpriteName != "")
		{
			Debug.Log(sprite.SpriteName);
			gameController.HandleDrag(gameController.spriteToElement[sprite.SpriteName], 1);
		}
		curHandleDrag.inSlot = true;
		sprite.SpriteName = curSprite.SpriteName;

		combine.FillSlot();

		args.State = dfDragDropState.Dropped;
		args.Use();
	}

	// Update is called once per frame
	void Update ()
	{
		curChildCount = transform.childCount;
	}
}
