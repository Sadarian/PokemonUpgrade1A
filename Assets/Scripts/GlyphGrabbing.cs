using UnityEngine;
using System.Collections;

public class GlyphGrabbing : MonoBehaviour
{

	public dfLabel greatFireCompCounter;

	// Use this for initialization
	void Awake () 
	{
		greatFireCompCounter = transform.FindChild("GreatFire").FindChild("Count").GetComponent<dfLabel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
