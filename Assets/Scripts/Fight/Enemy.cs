using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public int life;
	public int baseLife;
	public int mana;
	public int baseMana;
	public int defence;

	// Use this for initialization
	void Awake ()
	{
		baseLife = Random.Range(300, 600);
		baseMana = Random.Range(100, 300);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
