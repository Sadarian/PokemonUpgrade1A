using UnityEngine;
using System.Collections;

public class HandleDrop : MonoBehaviour
{
	public dfSprite curSprite;
	public GameObject firePrefab;
	public GameObject earthPrefab;
	public GameObject airPrefab;
	public GameObject waterPrefab;

	void Awake () {
	
	}

	public void OnDragDrop(dfControl source, dfDragEventArgs args)
	{
		Debug.Log("Drop it!");
		HandleDrag curHandleDrag = (HandleDrag)args.Data;
		curSprite = curHandleDrag.sprite;

		curSprite.transform.parent = transform;
		args.State = dfDragDropState.Dropped;
		args.Use();
		curHandleDrag.draged = false;

		CreateDragObject();
	}

	private void CreateDragObject()
	{
		switch (curSprite.GetComponent<HandleDrag>().material)
		{
			case GameController.Materials.Fire:
				{
					GameObject temp Instantiate(firePrefab, curSprite.)
					break;
				}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
