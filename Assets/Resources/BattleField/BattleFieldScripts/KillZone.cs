//This script destoys any projectiles and resets the combo multiplier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillZone : MonoBehaviour {
	public GameObject Player;
	public GameObject MultiText;

	// Use this for initialization
	void Start () {
		MultiText = GameObject.Find ("Multiplier");
		Player = GameObject.Find ("BasicTestModel");
	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter (Collider other) {
		if (other.tag == "PlayerAttack") {
			//if player projectile collides with zone, combo multi is reset to 1

			Player.GetComponent<PlayerMovement> ().ComboMulti = 1;

			if (Player.GetComponent<PlayerMovement> ().ComboMulti <= 1) {
				Player.GetComponent<PlayerMovement> ().ComboMulti = 1;
				//sets UI combo text with new multi
				MultiText.GetComponent<Text> ().text = Player.GetComponent<PlayerMovement> ().ComboMulti.ToString ();
			} else {

				MultiText.GetComponent<Text> ().text = Player.GetComponent<PlayerMovement> ().ComboMulti.ToString ();
			}

		}
		//destroys projectile
		Destroy (other.gameObject);
	}
}