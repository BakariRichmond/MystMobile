//controls the hp bar in the overworld and battles
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour {
	public float CurrHP;
	public float MaxHP;
	
	public bool BattleMode = false;
	public GameObject Player;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find("BasicTestModel");
		CurrHP = PlayerStats.CurrentHP / 2;
		MaxHP = PlayerStats.HP / 2;
		//based on the ratio of total health, hp bar changes colors to denote good, medium, and critically low hp

		if (CurrHP <= MaxHP / 6) {
			
			gameObject.GetComponent<Image> ().color = new Color32 (255, 13, 13, 255);

		} else if (CurrHP <= MaxHP / 3) {
			
			gameObject.GetComponent<Image> ().color = new Color32 (255, 255, 13, 255);

		} else {
			
			gameObject.GetComponent<Image> ().color = new Color32 (13, 255, 13, 255);

		}
		var HPTransform = gameObject.transform as RectTransform;
		//sets hp size based on current hp
		gameObject.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, CurrHP);
		GameObject.Find ("MaxHp").GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, MaxHP);

		

	}

	// Update is called once per frame
	void Update () {
		//battlemode updates hp every frame
		if (BattleMode) {
			//based on the ratio of total health, hp bar changes colors to denote good, medium, and critically low hp
			CurrHP = Player.GetComponent<PlayerMovement>().health/2;
			if (CurrHP <= MaxHP / 6) {
			
			gameObject.GetComponent<Image> ().color = new Color32 (255, 13, 13, 255);

		} else if (CurrHP <= MaxHP / 3) {
			
			gameObject.GetComponent<Image> ().color = new Color32 (255, 255, 13, 255);

		} else {
			
			gameObject.GetComponent<Image> ().color = new Color32 (13, 255, 13, 255);

		}
		var HPTransform = gameObject.transform as RectTransform;
		//sets hp size based on current hp
		gameObject.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, CurrHP);
		GameObject.Find ("MaxHp").GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, MaxHP);
		}

	}
}