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

	public ResourcesGrabbing resourcesGrabber;
	public GlyphGrabbing glyphGrabber;

	private const int COMPONENTVALVE = 20;
	private bool init = false;

	// Use this for initialization
	void Awake()
	{
		fireComponents = (int)(Random.value * COMPONENTVALVE + 1);
		airComponents = (int)(Random.value * COMPONENTVALVE + 1);
		earthComponents = (int)(Random.value * COMPONENTVALVE + 1);
		waterComponents = (int)(Random.value * COMPONENTVALVE + 1);
		greatFireComponents = 0;

		resourcesGrabber = GameObject.FindGameObjectWithTag("Resources").GetComponent<ResourcesGrabbing>();
		glyphGrabber = GameObject.FindGameObjectWithTag("Glyph").GetComponent<GlyphGrabbing>();

		spriteElement.Add("spell-2", Materials.Fire);
		spriteElement.Add("spell-6", Materials.Air);
		spriteElement.Add("spell-8", Materials.Earth);
		spriteElement.Add("spell-9", Materials.Water);
		spriteElement.Add("spell-11", Materials.GreatFire);
	}

	private void Init()
	{
		ComponentValue();
	}

	#region Glyphen/Components

	private void ComponentValue()
	{
		resourcesGrabber.fireCompCounter.Text = fireComponents.ToString();
		resourcesGrabber.airCompCounter.Text = airComponents.ToString();
		resourcesGrabber.earthCompCounter.Text = earthComponents.ToString();
		resourcesGrabber.waterCompCounter.Text = waterComponents.ToString();
		GlyphenValve();
	}

	private void GlyphenValve()
	{
		resourcesGrabber.greatFireCompCounter.Text = greatFireComponents.ToString();
		glyphGrabber.greatFireCompCounter.Text = greatFireComponents.ToString();
	}

	#endregion


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
	void Update ()
	{
		if (!init) Init();
	}
}
