using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public enum Materials
	{
		Fire,
		Air,
		Earth,
		Water,
		GreatFire
	}
	public Materials material;
	public Dictionary<string, Materials> spriteElement = new Dictionary<string, Materials>();
	
	public int fireComponents;
	public int airComponents;
	public int earthComponents;
	public int waterComponents;
	public int greatFireComponents;

	private dfLabel fireCompCounter;
	private dfLabel airCompCounter;
	private dfLabel earthCompCounter;
	private dfLabel waterCompCounter;
	private dfLabel greatFireCompCounter;

	private const int COMPONENTVALVE = 5;

	// Use this for initialization
	void Awake()
	{
		fireComponents = (int)(Random.value * COMPONENTVALVE + 1);
		airComponents = (int)(Random.value * COMPONENTVALVE + 1);
		earthComponents = (int)(Random.value * COMPONENTVALVE + 1);
		waterComponents = (int)(Random.value * COMPONENTVALVE + 1);
		greatFireComponents = 0;

		fireCompCounter = gameObject.transform.FindChild("Fire").FindChild("Count").GetComponent<dfLabel>();
		airCompCounter = gameObject.transform.FindChild("Air").FindChild("Count").GetComponent<dfLabel>(); 
		earthCompCounter = gameObject.transform.FindChild("Earth").FindChild("Count").GetComponent<dfLabel>();
		waterCompCounter = gameObject.transform.FindChild("Water").FindChild("Count").GetComponent<dfLabel>();
		greatFireCompCounter = gameObject.transform.FindChild("GreatFire").FindChild("Count").GetComponent<dfLabel>();

		spriteElement.Add("spell-2", Materials.Fire);
		spriteElement.Add("spell-6", Materials.Air);
		spriteElement.Add("spell-8", Materials.Earth);
		spriteElement.Add("spell-9", Materials.Water);
		spriteElement.Add("spell-11", Materials.GreatFire);

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
		greatFireCompCounter.Text = greatFireComponents.ToString();
	}

	public int HandleDrag(Materials dragedRecourse, int count)
	{
		int temp = 0;
		switch (dragedRecourse)
		{
			case Materials.Fire:
				{
					fireComponents += count;
					temp = fireComponents;
					break;
				}
			case Materials.Air:
				{
					airComponents += count;
					temp = airComponents;
					break;
				}
			case Materials.Earth:
				{
					earthComponents += count;
					temp = earthComponents;
					break;
				}
			case Materials.Water:
				{
					waterComponents += count;
					temp = waterComponents;
					break;
				}
			case Materials.GreatFire:
				{
					greatFireComponents += count;
					temp = greatFireComponents;
					break;
				}
		}
		ComponentValue();
		return temp;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
