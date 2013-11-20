using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public enum Materials
	{
		Fire,
		Air,
		Earth,
		Water
	}
	public Materials material;

	public int fireComponents;
	public int airComponents;
	public int earthComponents;
	public int waterComponents;

	private dfLabel fireCompCounter;
	private dfLabel airCompCounter;
	private dfLabel earthCompCounter;
	private dfLabel waterCompCounter;

	// Use this for initialization
	void Awake()
	{
		fireComponents = (int)(Random.value * 20 + 1);
		airComponents = (int)(Random.value * 20 + 1);
		earthComponents = (int)(Random.value * 20 + 1);
		waterComponents = (int)(Random.value * 20 + 1);

		fireCompCounter = gameObject.transform.FindChild("Fire").transform.FindChild("Count").GetComponent<dfLabel>();
		airCompCounter = gameObject.transform.FindChild("Air").transform.FindChild("Count").GetComponent<dfLabel>(); 
		earthCompCounter = gameObject.transform.FindChild("Earth").transform.FindChild("Count").GetComponent<dfLabel>();
		waterCompCounter = gameObject.transform.FindChild("Water").transform.FindChild("Count").GetComponent<dfLabel>();


		ComponentValue();
	}

	private void ComponentValue()
	{
		//Debug.Log("Fire Components: " + fireComponents);
		//Debug.Log("Earth Components: " + earthComponents);
		//Debug.Log("Air Components: " + airComponents);
		//Debug.Log("Water Components: " + waterComponents);

		fireCompCounter.Text = fireComponents.ToString();
		airCompCounter.Text = airComponents.ToString();
		earthCompCounter.Text = earthComponents.ToString();
		waterCompCounter.Text = waterComponents.ToString();
	}

	public void HandleDrag(GameObject dragedRecourse, int count)
	{
		switch (dragedRecourse.GetComponent<HandleDrag>().material)
		{
			case Materials.Fire:
				{
					fireComponents += count;
					break;
				}
			case Materials.Air:
				{
					airComponents += count;
					break;
				}
			case Materials.Earth:
				{
					earthComponents += count;
					break;
				}
			case Materials.Water:
				{
					waterComponents += count;
					break;
				}
		}
		ComponentValue();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
