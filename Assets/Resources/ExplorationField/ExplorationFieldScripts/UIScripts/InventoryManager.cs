//manages spells in inventory
using System.Collections;
using System.Collections.Generic;
using CustomClasses;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
[System.Serializable]

public class InventoryManager : MonoBehaviour {

	static public List<GameObject> InventorySpells = new List<GameObject> ();
	static public List<GameObject> EquippedSpells = new List<GameObject> ();
	static public bool init = true;
	public bool update = false;

	public GameObject Firerang;
	public GameObject Linedrive;
	public GameObject Zipper;
	public GameObject InvSpell;
	public GameObject GridPanel;
	public GameObject TestSpell;
	public GameObject Loot;
	public GameObject InventoryAudio;
	public GameObject InteractHandler;
	public GameObject InteractButton;
	public GameObject CloseButton;
	int test;
	Coroutine SaveInvRoutine;

	// Use this for initialization
	void Start () {
		InteractHandler = GameObject.Find ("InteractButtonHandler");
		InteractButton = GameObject.Find ("InteractButton");
		CloseButton = GameObject.Find ("CloseButton");

		if (init) {
			//sets default spells and adds to inventory

			SpellRef newSpell1 = new SpellRef ();
			newSpell1.name = Zipper.name;
			newSpell1.equipped = false;
			PlayerStats.SpellRefs.Add (newSpell1);

			SpellRef newSpell2 = new SpellRef ();
			newSpell2.name = Firerang.name;
			PlayerStats.SpellRefs.Add (newSpell2);

			SpellRef newSpell3 = new SpellRef ();
			newSpell3.name = Linedrive.name;
			PlayerStats.SpellRefs.Add (newSpell3);
			init = false;
		}
		if (PlayerStats.LootSpell != "null") {
			//adds loot spell obtained to inventory
			Loot = Instantiate (Resources.Load ("Projectile Models/Spells/" + PlayerStats.LootSpell)) as GameObject;

			SpellRef lootSpell = new SpellRef ();
			lootSpell.name = Loot.name.Replace ("(Clone)", "");

			PlayerStats.SpellRefs.Add (lootSpell);

			Destroy (Loot);

			PlayerStats.LootSpell = "null";
		}

		for (int i = 0; i < PlayerStats.SpellRefs.Count; i++) {
			//adds spells to UI list

			print (PlayerStats.SpellRefs[i].name + " at " + i);

			GameObject InvSpellClone = Instantiate (Resources.Load ("Projectile Models/Spells/" + PlayerStats.SpellRefs[i].name)) as GameObject;

			if (PlayerStats.SpellRefs[i].equipped) {
				InvSpellClone.GetComponent<InvSlot> ().Equipped = true;
			}
			string spellName = InvSpellClone.name.Replace ("(Clone)", "");

			InvSpellClone.name = spellName;

			InvSpellClone.transform.SetParent (GridPanel.transform, false);

			InvSpellClone.transform.localScale = new Vector3 (1f, 1f, 1);
			InvSpellClone.GetComponentInChildren<Text> ().text = InvSpellClone.GetComponent<InvSlot> ().SpellName;

		}

		PlayerStats.Spells.Clear ();

		EquippedSpells.Clear ();
		for (int i = 0; i < GridPanel.transform.childCount; i = i + 1) {
			//adds equipped spells to active spells

			if (GridPanel.transform.GetChild (i).gameObject.GetComponent<InvSlot> ().Equipped == true) {

				PlayerStats.SpellNames.Add (GridPanel.transform.GetChild (i).gameObject.name);
			}

		}

	}

	// Update is called once per frame
	void Update () {

		if (InvSlot.ActiveSlot != -1) {
			if (GridPanel.transform.GetChild (InvSlot.ActiveSlot).gameObject.GetComponent<InvSlot> ().Equipped == true) {
				//if spell is equipped, unequip when clicked
				GameObject.Find ("EquipButton").GetComponentInChildren<Text> ().text = "Unequip";

			} else {
				//if spell is unequipped, equip when clicked
				GameObject.Find ("EquipButton").GetComponentInChildren<Text> ().text = "Equip";

			}
		}

		if (CrossPlatformInputManager.GetButtonDown ("Equip"))

		{
			if (InvSlot.ActiveSlot != -1) {
				if (GridPanel.transform.GetChild (InvSlot.ActiveSlot).gameObject.GetComponent<InvSlot> ().Equipped == false) {
					//play equip sound
					InventoryAudio.GetComponent<InvAudioHandler> ().Equip.Play ();
					GridPanel.transform.GetChild (InvSlot.ActiveSlot).gameObject.GetComponent<InvSlot> ().Equipped = true;

					while (InvSlot.ActiveSlot >= PlayerStats.EquipIndex.Count) {
						PlayerStats.EquipIndex.Add (false);
					}
					PlayerStats.EquipIndex[InvSlot.ActiveSlot] = true;
				} else {
					//play unequip sound
					InventoryAudio.GetComponent<InvAudioHandler> ().Unequip.Play ();
					GridPanel.transform.GetChild (InvSlot.ActiveSlot).gameObject.GetComponent<InvSlot> ().Equipped = false;
					while (InvSlot.ActiveSlot >= PlayerStats.EquipIndex.Count) {
						PlayerStats.EquipIndex.Add (false);
					}
					PlayerStats.EquipIndex[InvSlot.ActiveSlot] = false;
				}
			}

		}

		if (CrossPlatformInputManager.GetButtonDown ("SaveInv"))

		{	//save inventory to current state
			InventoryAudio.GetComponent<InvAudioHandler> ().Audio1.Play ();
			PlayerStats.Spells.Clear ();
			PlayerStats.SpellNames.Clear ();
			EquippedSpells.Clear ();
			for (int i = 0; i < GridPanel.transform.childCount; i = i + 1) {

				if (GridPanel.transform.GetChild (i).gameObject.GetComponent<InvSlot> ().Equipped == true) {

					PlayerStats.SpellRefs[i].equipped = true;

				} else {
					PlayerStats.SpellRefs[i].equipped = false;
				}

			}
			SaveInvRoutine = StartCoroutine (SaveInventoryCoroutine ());
			print ("saved");

		}
		Vector3 scale = new Vector3 (1f, 1f, 1);

		if ((Input.GetKeyDown ("space") | InteractHandler.GetComponent<InteractHandler> ().ObjectBool) & ItemPickup.NotificationOn) {

			ItemPickup.NotificationOn = false;
			InteractHandler.GetComponent<InteractHandler> ().ObjectBool = false;

			GameObject.Find ("NotificationBG").transform.localScale = new Vector3 (0f, 0f, 0);
			GameObject.Find ("Menu").GetComponent<PauseToggle> ().open = false;
			GameObject.Find ("Menu").transform.localScale = new Vector3 (1f, 1f, 1);

		}

	}
	IEnumerator SaveInventoryCoroutine () {
		//shows checkmark for 1 second to confirm save
		GameObject.Find ("Check").transform.localScale = new Vector3 (1f, 1f, 1f);
		yield return new WaitForSeconds (1);
		GameObject.Find ("Check").transform.localScale = new Vector3 (0f, 0f, 0f);

	}
}