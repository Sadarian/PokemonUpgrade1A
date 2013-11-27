using UnityEngine;
using System.Collections;

public class ResourcesGrabbing : MonoBehaviour {

	public dfLabel fireCompCounter;
	public dfLabel airCompCounter;
	public dfLabel earthCompCounter;
	public dfLabel waterCompCounter;
	public dfLabel greatFireCompCounter;
	public dfLabel greatAirCompCounter;
	public dfLabel greatWaterCompCounter;
	public dfLabel greatEarthCompCounter;

	// Use this for initialization
	void Awake ()
	{
		fireCompCounter = transform.FindChild("Fire").FindChild("Count").GetComponent<dfLabel>();
		airCompCounter = transform.FindChild("Air").FindChild("Count").GetComponent<dfLabel>();
		earthCompCounter = transform.FindChild("Earth").FindChild("Count").GetComponent<dfLabel>();
		waterCompCounter = transform.FindChild("Water").FindChild("Count").GetComponent<dfLabel>();
		greatFireCompCounter = transform.FindChild("GreatFire").FindChild("Count").GetComponent<dfLabel>();
		greatAirCompCounter = transform.FindChild("GreatAir").FindChild("Count").GetComponent<dfLabel>();
		greatWaterCompCounter = transform.FindChild("GreatWater").FindChild("Count").GetComponent<dfLabel>();
		greatEarthCompCounter = transform.FindChild("GreatEarth").FindChild("Count").GetComponent<dfLabel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
