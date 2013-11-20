using UnityEngine;
using System.Collections.Generic;

public class HandleDrop : MonoBehaviour
{
	public Dictionary<GameController.Materials, GameObject> prefabs = new Dictionary<GameController.Materials, GameObject>();
	public dfSprite curSprite;
	public GameObject firePrefab;
	public GameObject earthPrefab;
	public GameObject airPrefab;
	public GameObject waterPrefab;
	public int curChildCount;

	void Awake () 
	{
		prefabs.Add(GameController.Materials.Fire, firePrefab);
		prefabs.Add(GameController.Materials.Air, airPrefab);
		prefabs.Add(GameController.Materials.Earth, earthPrefab);
		prefabs.Add(GameController.Materials.Water, waterPrefab);
	}

	public void OnDragDrop(dfControl source, dfDragEventArgs args)
	{
		Debug.Log("Drop it!");
		HandleDrag curHandleDrag = (HandleDrag)args.Data;
		curSprite = curHandleDrag.sprite;

		if (curHandleDrag.inSlot)
		{
			curSprite.transform.parent.gameObject.GetComponent<HandleDrop>().curSprite = null;
		}

		transform.GetChild(0).GetComponent<dfPanel>().BackgroundSprite = curSprite.SpriteInfo.name;

		args.State = dfDragDropState.Dropped;
		args.Use();
	}

	// Update is called once per frame
	void Update ()
	{
		curChildCount = transform.childCount;
	}
}
