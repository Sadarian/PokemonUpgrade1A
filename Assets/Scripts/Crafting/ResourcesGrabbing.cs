using UnityEngine;
using System.Collections.Generic;

public class ResourcesGrabbing : MonoBehaviour {

	public dfLabel fireCompCounter;
	public dfLabel airCompCounter;
	public dfLabel earthCompCounter;
	public dfLabel waterCompCounter;
	
	
	public List<RuneLayer>  runeLayers = new List<RuneLayer>();

	private GameObject layers;

	public class RuneLayer
	{
		public GameObject airRune;
		public GameObject earthRune;
		public GameObject fireRune;
		public GameObject waterRune;

		public dfLabel greatFireCompCounter;
		public dfLabel greatAirCompCounter;
		public dfLabel greatWaterCompCounter;
		public dfLabel greatEarthCompCounter;

		public RuneLayer(GameObject airRune, GameObject earthRune, GameObject fireRune, GameObject waterRune)
		{
			this.airRune = airRune;
			this.earthRune = earthRune;
			this.fireRune = fireRune;
			this.waterRune = waterRune;
			GetCouter();
		}

		private void GetCouter()
		{
			greatAirCompCounter = airRune.transform.FindChild("Count").GetComponent<dfLabel>();
			greatEarthCompCounter = earthRune.transform.FindChild("Count").GetComponent<dfLabel>();
			greatFireCompCounter = fireRune.transform.FindChild("Count").GetComponent<dfLabel>();
			greatWaterCompCounter = waterRune.transform.FindChild("Count").GetComponent<dfLabel>();
		}
	}

	void Awake ()
	{
		fireCompCounter = transform.FindChild("Fire").FindChild("Count").GetComponent<dfLabel>();
		airCompCounter = transform.FindChild("Air").FindChild("Count").GetComponent<dfLabel>();
		earthCompCounter = transform.FindChild("Earth").FindChild("Count").GetComponent<dfLabel>();
		waterCompCounter = transform.FindChild("Water").FindChild("Count").GetComponent<dfLabel>();

		layers = GameObject.FindGameObjectWithTag("RuneLayers");

		for (int i = 0; i < layers.transform.childCount; i++)
		{
			Transform curLayer = layers.transform.FindChild("Layer " + (i + 1));

			runeLayers.Add(new RuneLayer(curLayer.GetChild(0).gameObject, curLayer.GetChild(1).gameObject, curLayer.GetChild(2).gameObject, curLayer.GetChild(3).gameObject));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
