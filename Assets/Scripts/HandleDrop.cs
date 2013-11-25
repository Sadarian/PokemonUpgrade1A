using UnityEngine;
using System.Collections.Generic;

public class HandleDrop : MonoBehaviour
{
	
	public dfSprite sprite;
	public GameObject firePrefab;
	public GameObject earthPrefab;
	public GameObject airPrefab;
	public GameObject waterPrefab;
	public int curChildCount;

	private GameController gameController;
	void Awake () 
	{
		
		sprite = transform.GetChild(0).GetComponent<dfSprite>();

		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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
			gameController.HandleDrag(gameController.spriteElement[sprite.SpriteName], 1);
		}
		curHandleDrag.inSlot = true;
		sprite.SpriteName = curSprite.SpriteName;

		args.State = dfDragDropState.Dropped;
		args.Use();
	}

	// Update is called once per frame
	void Update ()
	{
		curChildCount = transform.childCount;
	}
}
