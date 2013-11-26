using UnityEngine;
using System.Collections;

public class ResourcesGrabbing : MonoBehaviour {

	public dfLabel fireCompCounter;
	public dfLabel airCompCounter;
	public dfLabel earthCompCounter;
	public dfLabel waterCompCounter;
	public dfLabel greatFireCompCounter;

	// Use this for initialization
	void Awake ()
	{
		fireCompCounter = transform.FindChild("Fire").FindChild("Count").GetComponent<dfLabel>();
		airCompCounter = transform.FindChild("Air").FindChild("Count").GetComponent<dfLabel>();
		earthCompCounter = transform.FindChild("Earth").FindChild("Count").GetComponent<dfLabel>();
		waterCompCounter = transform.FindChild("Water").FindChild("Count").GetComponent<dfLabel>();
		greatFireCompCounter = transform.FindChild("GreatFire").FindChild("Count").GetComponent<dfLabel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
