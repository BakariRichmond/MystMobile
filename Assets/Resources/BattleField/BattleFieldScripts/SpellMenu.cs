//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class SpellMenu : MonoBehaviour {

	public List<GameObject> SpellBuffer = new List<GameObject> ();

	public GameObject[] SpellBufferArray = new GameObject[8];
	public List<int> SpellBufferOrder = new List<int> ();

	public int[] IndexArray = new int[8];
	public int IndexTracker = 0;
	public GameObject Player;
	public GameObject SlotBG;
	public GameObject RuneButton;
	public GameObject SpellPanel;
	public bool RuneActive;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("BasicTestModel");
		RuneActive = true;
		SlotBG = GameObject.Find ("SlotBG");
		RuneButton = GameObject.Find ("RuneButton");
		SpellPanel = GameObject.Find ("SpellPanel");
		RuneButton.transform.localScale = new Vector3 (0f, 0f, 0f);
	}

	// Update is called once per frame
	void Update () {

		if (CrossPlatformInputManager.GetButtonDown ("RuneButton")) {
			//if rune button pressed,clear spell panel children

			for (int i = 0; i < SpellPanel.transform.childCount; i = i + 1) {

				Destroy (SpellPanel.transform.GetChild (i).gameObject);

			}

			string CurrentSlot = "0";
			GameObject SlotObj = GameObject.Find ("0");
			PlayerMovement PlayerScript = Player.GetComponent<PlayerMovement> ();

			IndexTracker = 0;

			for (int i = 0; i < 8; i++) {
				IndexArray[i] = 0;
			}

			for (int i = 0; i < 8; i++) {
				CurrentSlot = i.ToString ();
				SlotObj = GameObject.Find (CurrentSlot);

				if (SlotObj.GetComponent<SlotChoice> ().SlotStatus == "selected") {
					//if spell was selected last round, it is removed form the spell array
					PlayerScript.SpellArray[i] = null;
				}

			}

			for (int i = 0; i < 8; i = i + 1) {
				SpellBufferArray[i] = null;
			}
			for (int i = 0; i < SpellBufferOrder.Count; i = i + 0) {
				SpellBufferOrder.RemoveAt (0);
			}
			//clears mana
			GameObject.Find ("slot0").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("slot1").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("slot2").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("slot3").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("slot4").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("slot5").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("slot6").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("slot7").GetComponent<Image> ().color = Color.white;

			RuneActive = true;
			//opens spell slot menu
			SlotBG.transform.localScale = new Vector3 (1f, 1f, 1f);
			RuneButton.transform.localScale = new Vector3 (0f, 0f, 0f);

		}

		if (CrossPlatformInputManager.GetButtonDown ("Confirm")) {
			//if confirm button clicked, add selected spells to active spells
			PlayerMovement PlayerScript = Player.GetComponent<PlayerMovement> ();

			for (int i = 0; i < SpellBufferOrder.Count; i++) {

				PlayerScript.ActiveSpells.Add (SpellBufferArray[SpellBufferOrder[i]]);
				print ("added" + SpellBufferArray[SpellBufferOrder[i]]);
				if (IndexArray[i] == 1) {
					PlayerScript.SpellArray[i] = null;
				}

			}
			//close spell slot menu
			SlotBG.transform.localScale = new Vector3 (0f, 0f, 0f);
			RuneButton.transform.localScale = new Vector3 (1f, 1f, 1f);
			//resume time
			Time.timeScale = 1f;
		}
	}
}