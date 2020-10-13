//opens dialougue box to give player an item
using System.Collections;
using System.Collections.Generic;
using CustomClasses;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ItemPickup : MonoBehaviour {
	public GameObject Item;
	public int Amount;
	public bool item = true;
	public GameObject Spell;
	public GameObject Smoke;
	static public List<ItemRef> ItemList = new List<ItemRef> ();
	public SpellRef NewSpell;
	public GameObject NotificationBG;
	static public bool NotificationOn = false;
	public Coroutine NotifRoutine;
	public Coroutine CloseRoutine;
	public bool init = false;
	public AudioSource click;
	public AudioSource open;
	public AudioSource close;
	public bool inCollider;
	public GameObject InteractButton;
	public GameObject CloseButton;
	public ItemRef NewItem;
	public bool worldObject = true;

	public GameObject InteractHandler;

	// Use this for initialization
	void Start () {
		if (worldObject) {
			//calls InitLoot for worldObjects as opposed to quest rewards
			InitLoot ();
		}

	}
	public void InitLoot () {
		InteractButton = GameObject.Find ("InteractButton");
		CloseButton = GameObject.Find ("CloseButton");
		InteractHandler = GameObject.Find ("InteractButtonHandler");

		NewItem.coords = gameObject.transform.position.ToString ();

		if (worldObject) {
			//if the item has been picked up and added to ItemList previously, it will no longer load
			for (int i = 0; i < ItemList.Count; i++) {
				if (ItemList[i].coords == NewItem.coords) {
					Destroy (gameObject);

				} else {

				}

			}
		}
		NotificationBG = GameObject.Find ("NotificationBG");

		NewSpell = new SpellRef ();
		NewSpell.name = Spell.name;
		NewSpell.equipped = false;

	}

	// Update is called once per frame
	void Update () {
		if (NotificationOn) {
			print (NotificationOn);
		}

	}
	IEnumerator NotifCoroutine () {
		//opens notification box which plays an animation for chests and displays item obtained
		yield return new WaitForSeconds (.9f);
		close.Play ();
		GetComponentInChildren<Light> ().intensity = 10;
		yield return new WaitForSeconds (.1f);

		NotificationBG.transform.localScale = new Vector3 (1f, 1f, 1);
		open.Play ();
		yield return new WaitForSeconds (2);
		NotificationOn = true;
		InteractButton.transform.localScale = new Vector3 (0f, 0f, 0);
		CloseButton.transform.localScale = new Vector3 (1f, 1f, 1);

		GameObject SmokeClone = Instantiate (Smoke) as GameObject;
		SmokeClone.transform.position = gameObject.transform.position;
		//Smoke.GetComponent<ParticleSystem>().Play();
		Destroy (SmokeClone, 3);
		InteractButton.transform.localScale = new Vector3 (0f, 0f, 0);
		Destroy (gameObject);

	}
	IEnumerator closeCoroutine () {
		//displays item obtained and causes item to dissapear

		gameObject.GetComponent<Renderer> ().enabled = false;
		NotificationOn = true;

		NotificationBG.transform.localScale = new Vector3 (1f, 1f, 1);
		InteractButton.transform.localScale = new Vector3 (0f, 0f, 0);
		
		yield return new WaitForSeconds (2);
		
		NotificationBG.transform.localScale = new Vector3 (0f, 0f, 0);
		
		if (!worldObject) {
			InteractButton.transform.localScale = new Vector3 (1f, 1f, 1);
		}
		GameObject.Find ("Menu").GetComponent<PauseToggle> ().open = false;
		NotificationOn = false;

		Destroy (gameObject);
		yield return null;

	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			inCollider = true;
			InteractHandler.GetComponent<InteractHandler> ().open = true;

		}
	}
	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag == "Player") {
			inCollider = false;
			InteractHandler.GetComponent<InteractHandler> ().open = false;

		}
	}
	void OnTriggerStay (Collider other) {
		//adds item calling itemGet

		if (other.gameObject.tag == "Player") {
			if (Input.GetButtonDown ("Jump") | InteractHandler.GetComponent<InteractHandler> ().ObjectBool | item == true) {
				itemGet ();

			}

		}
	}
	public void itemGet () {
		if (!init) {

			init = true;
			InteractHandler.GetComponent<InteractHandler> ().ObjectBool = false;
			//plays click sound
			if (click != null) {
				click.Play ();
			}

			if (Amount == 0) {
				//if not a munny amount, adds spell item and displays the item obtained

				GameObject GridPanel = GameObject.Find ("GridPanel");
				GameObject InvSpellClone = Instantiate (Resources.Load ("Projectile Models/Spells/" + NewSpell.name)) as GameObject;

				PlayerStats.SpellRefs.Add (NewSpell);
				InvSpellClone.transform.SetParent (GridPanel.transform, false);

				InvSpellClone.transform.localScale = new Vector3 (1f, 1f, 1);
				InvSpellClone.GetComponentInChildren<Text> ().text = InvSpellClone.GetComponent<InvSlot> ().SpellName;

				NotificationBG.GetComponentInChildren<Text> ().text = "Obtained " + NewSpell.name + "!";

				print ("scaled up");

				GameObject.Find ("Menu").GetComponent<PauseToggle> ().open = true;

				GameObject.Find ("Menu").transform.localScale = new Vector3 (0f, 0f, 0);
				ItemList.Add (NewItem);
				if (item) {
					//if item, open notification box and call close routine
					NotificationBG.transform.localScale = new Vector3 (1f, 1f, 1);
					
					

					if (open != null) {
						open.Play ();
					}
					NotificationOn = true;
					CloseRoutine = StartCoroutine (closeCoroutine ());

				} else {
					//if not item, use notifCouroutine and play animation
					NotifRoutine = StartCoroutine (NotifCoroutine ());
					GetComponent<Animator> ().SetTrigger ("chestOpen");

				}
			} else {
				//if cash amount, add amount to player
				PlayerStats.Zells += Amount;
				if (worldObject) {
					
					GameObject.Find ("Game").GetComponent<QuestManager> ().QuestAmountUpdate ();
					GameObject.Find ("Game").GetComponent<QuestManager> ().UIQuestUpdate ();
				}

				NotificationBG.GetComponentInChildren<Text> ().text = "Obtained " + Amount + "z!";

				//closes menu and opens notification box

				GameObject.Find ("Menu").GetComponent<PauseToggle> ().open = true;

				GameObject.Find ("Menu").transform.localScale = new Vector3 (0f, 0f, 0);

				NotificationBG.transform.localScale = new Vector3 (1f, 1f, 1);
			

				if (item) {
					//for items, opens notification and calls closeCoroutine
					NotificationBG.transform.localScale = new Vector3 (1f, 1f, 1);
					
					

					if (open != null) {
						open.Play ();
					}
					NotificationOn = true;
					CloseRoutine = StartCoroutine (closeCoroutine ());
					

				} else {
					//calls notification routine
					NotifRoutine = StartCoroutine (NotifCoroutine ());
					GetComponent<Animator> ().SetTrigger ("chestOpen");

				}
			}

		}

	}
}