//this script determines enemy drops
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class EnemyDrop : MonoBehaviour {
	public GameObject Field;
	public GameObject EnemyLoot;
	static public float LowestRate;
	public GameObject ItemText;
	public float LootRate;
	public bool Success = false;
	public bool init = true;

	// Use this for initialization
	void Start () {
		LowestRate = 100;
		Field = GameObject.Find ("Field");
		ItemText = GameObject.Find ("ItemText");

		int rndm = (int) Mathf.Round (Random.Range (0f, 100f));
		if (rndm <= LootRate) {

			if (LootRate < LowestRate) {
				LowestRate = LootRate;
			}
			if (LowestRate == LootRate) {

				Success = true;
				//gives enemy loot spell
				ItemText.GetComponent<Text> ().text = ("You obtained " + EnemyLoot.name + "!");
				PlayerStats.LootSpell = EnemyLoot.name;
				

			} else {
				if (rndm <= 100) {
					//gives money amount based on random int
				
				ItemText.GetComponent<Text> ().text = ("You obtained 25z!");
				PlayerStats.Zells += 25;
				}
				else if (rndm <= 75){
					ItemText.GetComponent<Text> ().text = ("You obtained 50z!");
					PlayerStats.Zells += 50;

				}
				else if (rndm <= 50){
					ItemText.GetComponent<Text> ().text = ("You obtained 100z!");
					PlayerStats.Zells += 100;

				}
				else if (rndm <= 25){
					ItemText.GetComponent<Text> ().text = ("You obtained 150z!");
					PlayerStats.Zells += 150;

				}
			}
		}
	}

	// Update is called once per frame
	void Update () { }

}
