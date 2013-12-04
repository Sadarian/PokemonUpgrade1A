using UnityEngine;
using System.Collections;

public class EquipSlot : MonoBehaviour
{
	public GameController.Rune rune;
	public int uses = 0;

	void Awake () {
	
	}
	
	void Update () {
		if (rune != null)
		{
			uses = rune.uses;
		}
	}
}
