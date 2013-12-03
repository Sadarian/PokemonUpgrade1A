using UnityEngine;
using System.Collections;

public class BarValue : MonoBehaviour {

	void Awake () {
	
	}

	public void OnValueChanged(dfControl control, System.Single value)
	{
		transform.GetChild(0).GetComponent<dfLabel>().Text = GetComponent<dfProgressBar>().Value.ToString() + " / " + GetComponent<dfProgressBar>().MaxValue.ToString();
	}

	
	void Update () {
	
	}
}
