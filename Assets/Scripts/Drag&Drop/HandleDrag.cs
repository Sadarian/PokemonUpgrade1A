using UnityEngine;
using System.Collections;

public class HandleDrag : MonoBehaviour
{
	public GameController.Materials material;
	public Vector3 startPosition;
	public dfSprite sprite;
	public bool draged = false;
	public bool inSlot = false;

	private Vector2 rootPosition;

	private bool init = false;
	private GameController gameController;

	// Use this for initialization
	void Awake ()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		
		sprite = gameObject.GetComponent<dfSprite>();
	}

	private void Init()
	{
		startPosition = gameObject.transform.position;
		rootPosition = new Vector2(GetComponent<dfControl>().GetGUIScreenPos().x - sprite.Size.x, GetComponent<dfControl>().GetGUIScreenPos().y - sprite.Size.y);
		init = true;
	}

	public void OnDragStart(dfControl control, dfDragEventArgs dragEvent)
	{
		if (!init) Init() ;
		
		dragEvent.Data = this;
		dragEvent.State = dfDragDropState.Dragging;
		dragEvent.Use();

		draged = true;

		if (gameController.HandleDrag(material, -1) < 0)
		{
			gameController.HandleDrag(material, 1);
			inSlot = true;
			draged = false;
		}
	}

	public void OnDragEnd(dfControl control, dfDragEventArgs dragEvent)
	{
		if (dragEvent.State == dfDragDropState.Denied)return;

		//Debug.Log("reset to Stack");
		gameObject.transform.position = startPosition;
		dragEvent.State = dfDragDropState.Denied;


		if (!inSlot)
		{
			gameController.HandleDrag(material, 1);
		}

		draged = false;
		inSlot = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (draged)
		{
			Vector2 position = sprite.GetManager().ScreenToGui(Input.mousePosition) - rootPosition;

			sprite.RelativePosition = position - sprite.Size * 0.5f;
		}
	}
}
