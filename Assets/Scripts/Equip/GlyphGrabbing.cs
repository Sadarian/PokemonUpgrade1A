using UnityEngine;
using System.Collections;

public class GlyphGrabbing : MonoBehaviour
{

	public dfLabel greatFireCompCounter;
	public dfLabel greatAirCompCounter;
	public dfLabel greatWaterCompCounter;
	public dfLabel greatEarthCompCounter;

	// Use this for initialization
	void Awake () 
	{
		greatFireCompCounter = transform.FindChild("GreatFire").FindChild("Count").GetComponent<dfLabel>();
		greatAirCompCounter = transform.FindChild("GreatAir").FindChild("Count").GetComponent<dfLabel>();
		greatWaterCompCounter = transform.FindChild("GreatWater").FindChild("Count").GetComponent<dfLabel>();
		greatEarthCompCounter = transform.FindChild("GreatEarth").FindChild("Count").GetComponent<dfLabel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
