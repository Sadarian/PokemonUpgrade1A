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

	public void OnDragStart(dfControl control, dfDragEventArgs dragEvent)
	{
		if (!init) Init() ;
		
		dragEvent.Data = this;
		dragEvent.State = dfDragDropState.Dragging;
		dragEvent.Use();

		draged = true;
		rootPosition = transform.parent.GetComponent<dfControl>().RelativePosition + transform.parent.parent.GetComponent<dfControl>().RelativePosition;

		if (!inSlot)
		{
			gameController.HandleDrag(control.gameObject, -1);
		}
	}

	private void Init()
	{
		startPosition = gameObject.transform.position;
		init = true;
	}

	public void OnDragEnd(dfControl control, dfDragEventArgs dragEvent)
	{
		if (dragEvent.State == dfDragDropState.Dropped)return;
		Vector3 curPosition = Input.mousePosition;

		if (draged)
		{
			Debug.Log("reset position " + curPosition + " " + startPosition);
			gameController.HandleDrag(control.gameObject, 1);
			gameObject.transform.position = startPosition;

			dragEvent.State = dfDragDropState.Dropped;
		}
		draged = false;

		if (inSlot)
		{

		}
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
