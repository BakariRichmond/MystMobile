//This script manages battlefield mana gauge
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ManaGauge : MonoBehaviour {
	public List<int> GaugeList = new List<int> ();
	public List<int> GaugeListTemp = new List<int> ();
	public GameObject Player;

	public bool DoubleLit8;
	public bool Subtracted = false;
	public bool Added = false;
	public int StartingGauge;

	// Use this for initialization
	void Start () {

		Player = GameObject.Find ("BasicTestModel");
		for (int i = 0; i < StartingGauge; i++) {
			InitializeGauge ();

		}

	}
	public void InitializeGauge () {
		//determines if player gets yellow mana(worth 2) based on player luck

		int rndm = (int) Mathf.Round (Random.Range (0, 100));
		if (rndm > PlayerStats.Luck) {
			GaugeList.Add (1);
			Player.GetComponent<PlayerMovement> ().Mana += 1;
		} else {
			GaugeList.Add (2);
			Player.GetComponent<PlayerMovement> ().Mana += 2;
		}
		Player.GetComponent<PlayerMovement> ().ManaInit = 0;

		Player.GetComponent<PlayerMovement> ().Mana = 0;
		//sets mana colors based on the int at the manas index
		for (int i = 0; i < GaugeList.Count; i++) {
			if (GaugeList[i] == 1) {
				//normal white mana
				string ManaName = "Mana" + i;
				GameObject CurrMana = GameObject.Find (ManaName);
				CurrMana.GetComponent<Image> ().color = Color.white;
				Player.GetComponent<PlayerMovement> ().Mana += 1;

			} else if (GaugeList[i] == 2) {
				//yellow mana worth 2
				string ManaName = "Mana" + i;
				GameObject CurrMana = GameObject.Find (ManaName);
				CurrMana.GetComponent<Image> ().color = Color.yellow;
				Player.GetComponent<PlayerMovement> ().Mana += 2;
			} else if (GaugeList[i] == 3) {
				//temporary blue mana; 'half' a yellow mana
				string ManaName = "Mana" + i;
				GameObject CurrMana = GameObject.Find (ManaName);
				CurrMana.GetComponent<Image> ().color = Color.blue;
				Player.GetComponent<PlayerMovement> ().Mana += 1;
			} else {
				string ManaName = "Mana" + i;
				GameObject CurrMana = GameObject.Find (ManaName);
				CurrMana.GetComponent<Image> ().color = new Color32 (68, 68, 68, 255);
			}
		}

	}
	// Update is called once per frame
	void Update () {
		//called if 1  mana has been used
		if (Subtracted) {
			//sets all mana to black
			for (int i = 0; i < 9; i++) {
				string ManaName = "Mana" + i;
				GameObject CurrMana = GameObject.Find (ManaName);
				CurrMana.GetComponent<Image> ().color = new Color32 (68, 68, 68, 255);

			}
			//checks if last mana in gaugelist has an int equal to 1 and if so adds it as 1 to a temp list
			if (GaugeList[GaugeList.Count - 1] == 1) {
				GaugeListTemp.Add (1);
				GaugeList.RemoveAt (GaugeList.Count - 1);

			} //if the mana equals 2, adds 2 to the temp list and sets the current mana to 3
			else if (GaugeList[GaugeList.Count - 1] == 2) {

				GaugeListTemp.Add (2);

				GaugeList[GaugeList.Count - 1] = 3;

			} //if the mana equals 3, adds a 3 to the temp list and removes the current mana
			else if (GaugeList[GaugeList.Count - 1] == 3) {
				GaugeListTemp.Add (3);
				GaugeList.RemoveAt (GaugeList.Count - 1);

			}
			//clears manas to black and based on the mana ints adds mana to the players mana count
			Player.GetComponent<PlayerMovement> ().Mana = 0;
			for (int i = 0; i < GaugeList.Count; i++) {
				if (GaugeList[i] == 1) {
					string ManaName = "Mana" + i;
					GameObject CurrMana = GameObject.Find (ManaName);
					CurrMana.GetComponent<Image> ().color = Color.white;
					//adds 1 to mana
					Player.GetComponent<PlayerMovement> ().Mana += 1;

				} else if (GaugeList[i] == 2) {
					string ManaName = "Mana" + i;
					GameObject CurrMana = GameObject.Find (ManaName);
					CurrMana.GetComponent<Image> ().color = Color.yellow;
					//adds 2 to mana
					Player.GetComponent<PlayerMovement> ().Mana += 2;
				} else if (GaugeList[i] == 3) {
					string ManaName = "Mana" + i;
					GameObject CurrMana = GameObject.Find (ManaName);
					CurrMana.GetComponent<Image> ().color = Color.blue;
					//adds 1 to mana
					Player.GetComponent<PlayerMovement> ().Mana += 1;
				} else {
					string ManaName = "Mana" + i;
					GameObject CurrMana = GameObject.Find (ManaName);
					CurrMana.GetComponent<Image> ().color = new Color32 (68, 68, 68, 255);
				}
			}

			Subtracted = false;
		} else if (Added) {
			//if a mana is added (through hitting an enemy, or undoing a spell selection)

			//clear all mana
			for (int i = 0; i < 9; i++) {
				string ManaName = "Mana" + i;
				GameObject CurrMana = GameObject.Find (ManaName);
				CurrMana.GetComponent<Image> ().color = new Color32 (68, 68, 68, 255);

			}
			//if mana int is 1, add a 1 to the temporary list
			if (GaugeListTemp[GaugeListTemp.Count - 1] == 1) {

				GaugeList.Add (1);

				GaugeListTemp.RemoveAt (GaugeListTemp.Count - 1);
				//if 2, gauge equals 2, and removes the last gauge from the temp list
			} else if (GaugeListTemp[GaugeListTemp.Count - 1] == 2) {

				GaugeList[GaugeList.Count - 1] = 2;
				GaugeListTemp.RemoveAt (GaugeListTemp.Count - 1);
			}
			//if 3, add a 3 to the gauge list and remove one from the temp list
			else if (GaugeListTemp[GaugeListTemp.Count - 1] == 3) {

				GaugeList.Add (3);
				GaugeListTemp.RemoveAt (GaugeListTemp.Count - 1);
			}

			//sets mana colors and adds player mana based on mana ints
			Player.GetComponent<PlayerMovement> ().Mana = 0;
			for (int i = 0; i < GaugeList.Count; i++) {
				if (GaugeList[i] == 1) {
					string ManaName = "Mana" + i;
					GameObject CurrMana = GameObject.Find (ManaName);
					CurrMana.GetComponent<Image> ().color = Color.white;
					//adds 1 mana
					Player.GetComponent<PlayerMovement> ().Mana += 1;

				} else if (GaugeList[i] == 2) {
					string ManaName = "Mana" + i;
					GameObject CurrMana = GameObject.Find (ManaName);
					CurrMana.GetComponent<Image> ().color = Color.yellow;
					//adds 2 mana
					Player.GetComponent<PlayerMovement> ().Mana += 2;
				} else if (GaugeList[i] == 3) {
					string ManaName = "Mana" + i;
					GameObject CurrMana = GameObject.Find (ManaName);
					CurrMana.GetComponent<Image> ().color = Color.blue;
					//ads 1 mana
					Player.GetComponent<PlayerMovement> ().Mana += 1;
				} else {
					string ManaName = "Mana" + i;
					GameObject CurrMana = GameObject.Find (ManaName);
					CurrMana.GetComponent<Image> ().color = new Color32 (68, 68, 68, 255);
				}
			}
			print ("Mana: " + Player.GetComponent<PlayerMovement> ().Mana);

			Added = false;
		}

		if (Player.GetComponent<PlayerMovement> ().Hit == true) {
			//checks if player hit an enemy
			if (Player.GetComponent<PlayerMovement> ().ManaInit >= 7) {
				//mana lights up

				if (GaugeList.Count < 9) {
					//based on luck, player will get +1 or +2 mana
					int rndm = (int) Mathf.Round (Random.Range (0, 100));
					if (rndm > PlayerStats.Luck) {
						GaugeList.Add (1);
						Player.GetComponent<PlayerMovement> ().Mana += 1;
					} else {
						GaugeList.Add (2);
						Player.GetComponent<PlayerMovement> ().Mana += 2;
					}
					Player.GetComponent<PlayerMovement> ().ManaInit = 0;
				}

			}
			//sets ne player mana based on gaugelist ints
			Player.GetComponent<PlayerMovement> ().Mana = 0;
			for (int i = 0; i < GaugeList.Count; i++) {
				if (GaugeList[i] == 1) {
					string ManaName = "Mana" + i;
					GameObject CurrMana = GameObject.Find (ManaName);
					CurrMana.GetComponent<Image> ().color = Color.white;
					Player.GetComponent<PlayerMovement> ().Mana += 1;

				} else if (GaugeList[i] == 2) {
					string ManaName = "Mana" + i;
					GameObject CurrMana = GameObject.Find (ManaName);
					CurrMana.GetComponent<Image> ().color = Color.yellow;
					Player.GetComponent<PlayerMovement> ().Mana += 2;
				} else if (GaugeList[i] == 3) {
					string ManaName = "Mana" + i;
					GameObject CurrMana = GameObject.Find (ManaName);
					CurrMana.GetComponent<Image> ().color = Color.blue;
					Player.GetComponent<PlayerMovement> ().Mana += 1;
				} else {
					string ManaName = "Mana" + i;
					GameObject CurrMana = GameObject.Find (ManaName);
					CurrMana.GetComponent<Image> ().color = new Color32 (68, 68, 68, 255);
				}
			}

			Player.GetComponent<PlayerMovement> ().Hit = false;

		}
	}
}