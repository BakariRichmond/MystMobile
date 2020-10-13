//tracks inventory slots status
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class InvSlot : MonoBehaviour {
	static public int ActiveSlot = -1;
	public bool Equipped = false;
	public bool Active = false;
	public Rigidbody Spell;
	public string SpellName;
	public string SpellDescription;
	public string SpellElement;
	public GameObject InventoryAudio;
	public GameObject Desc;

	// Use this for initialization

	
	void Start () {
		InventoryAudio = GameObject.Find("InventoryAudio");
		
		
		
		Desc = GameObject.Find("Description");

		ActiveSlot = -1;
	}

	// Update is called once per frame
	void Update () {

		if (ActiveSlot == gameObject.transform.GetSiblingIndex ()) {
			//if active slot is not = to sibling index, change color
			gameObject.GetComponent<Image> ().color = new Color32 (100, 100, 100, 255);
			if(Desc != null){
				//set spell description
			Desc.GetComponentInChildren<Text>().text = SpellDescription;
			}

		} else { gameObject.GetComponent<Image> ().color = new Color32 (75, 75, 75, 255); }

		if (CrossPlatformInputManager.GetButtonDown (gameObject.transform.GetSiblingIndex ().ToString ())) {
			if(InventoryAudio != null){
				//play click
			InventoryAudio.GetComponent<InvAudioHandler>().Click.Play();}

			if (ActiveSlot == gameObject.transform.GetSiblingIndex ()) { ActiveSlot = -1; } else {
				ActiveSlot = gameObject.transform.GetSiblingIndex ();
				
			}
			print (ActiveSlot + "clicked");
			

		}
		if (Equipped) {
			//if item is equipped set "E"
			gameObject.transform.GetChild (1).GetComponent<Text> ().text = "E";
		} else {

			gameObject.transform.GetChild (1).GetComponent<Text> ().text = "";
		}

	}
}