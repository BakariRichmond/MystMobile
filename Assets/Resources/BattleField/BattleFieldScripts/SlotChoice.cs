//manages spell slot buttons selected
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class SlotChoice : MonoBehaviour {
	public GameObject Player;
	public GameObject Gauge;
	public GameObject ConfirmButton;
	public GameObject SpellPanel;
	public GameObject SpellPreview;
	private string SpellName;
	public string SlotStatus
	;
	private bool init = false;

	// Use this for initialization
	void Start () {
		
		Player = GameObject.Find ("BasicTestModel");
		ConfirmButton = GameObject.Find ("Confirm");
		Gauge = GameObject.Find ("ManaGauge");
		SpellPanel = GameObject.Find ("SpellPanel");
		SpellPreview = GameObject.Find ("SpellPreview");
		SlotStatus = gameObject.GetComponent<Text> ().text;
		bool reset = true;

	}

	// Update is called once per frame
	void Update () {

		if (CrossPlatformInputManager.GetButtonDown ("RuneButton")) {
			//if rune button pressed, slotStatus is set to gameojects text component
			
		
			
			SlotStatus = gameObject.GetComponent<Text> ().text;
			init = false;
		}

		if (CrossPlatformInputManager.GetButtonDown (gameObject.name)) {
			//if  this objects button is pressed, slot text status is set to gameobjects text
			string SlotTextStatus = gameObject.GetComponent<Text> ().text;

			if (SlotTextStatus != "null") {
				if (SlotStatus == "selected") {
					
					SpellMenu MenuScript = ConfirmButton.GetComponent<SpellMenu> ();
					PlayerMovement PlayerScript = Player.GetComponent<PlayerMovement> ();
					ManaGauge GaugeScript = Gauge.GetComponent<ManaGauge> ();
					
					//if slot has been selected add the spell to spell buffer
					MenuScript.SpellBufferArray[Int32.Parse (gameObject.name)] = null;
					int BufferIndex = MenuScript.SpellBufferOrder.FindIndex (a => a == Int32.Parse (gameObject.name));
					//remove spellpanel child at buffer index
					Destroy (SpellPanel.transform.GetChild (BufferIndex).gameObject);
					

					MenuScript.SpellBufferOrder.RemoveAll (x => x == Int32.Parse (gameObject.name));

					//subtracts 1 from the index
					MenuScript.IndexTracker = MenuScript.IndexTracker - 1;
					MenuScript.IndexArray[Int32.Parse (gameObject.name)] = 0;
					gameObject.GetComponent<Text> ().text = SpellName;
					SlotStatus = SpellName;
					//adds back one mana
					PlayerScript.Mana += 1;
					GaugeScript.Added = true;
					gameObject.transform.parent.gameObject.GetComponent<Image> ().color = Color.white;

				} else if (Gauge.GetComponent<ManaGauge> ().GaugeList.Count > 0) {
					if (!init) {
						SpellName = gameObject.GetComponent<Text> ().text;

						print ("parented");
						init = true;
					}
					PlayerMovement PlayerScript = Player.GetComponent<PlayerMovement> ();
					SpellMenu MenuScript = ConfirmButton.GetComponent<SpellMenu> ();
					ManaGauge GaugeScript = Gauge.GetComponent<ManaGauge> ();

					
					GameObject Spell = PlayerScript.SpellArray[Int32.Parse (gameObject.name)];
					

					

					GameObject SpellPreviewClone;
					//instantiate spell preview in UI
					SpellPreviewClone = Instantiate (SpellPreview, transform);

					SpellPreviewClone.transform.SetParent (SpellPanel.transform, false);
					SpellPreviewClone.GetComponentInChildren<Text> ().text = SpellName;

					MenuScript.SpellBufferArray[Int32.Parse (gameObject.name)] = Spell;

					MenuScript.SpellBufferOrder.Add (Int32.Parse (gameObject.name));

					
					MenuScript.IndexTracker = MenuScript.IndexTracker + 1;
					print (SlotTextStatus + " is number " + MenuScript.IndexTracker + " in line");
					MenuScript.IndexArray[Int32.Parse (gameObject.name)] = 1;
					//mana is reduced by 1
					PlayerScript.Mana -= 1;

					GaugeScript.Subtracted = true;
					print ("subtracted = true");
					//set status to selected
					SlotStatus = "selected";
					gameObject.transform.parent.gameObject.GetComponent<Image> ().color = Color.grey;
					
				}
			} else { print ("No spell in this slot"); }
		}

	}
}