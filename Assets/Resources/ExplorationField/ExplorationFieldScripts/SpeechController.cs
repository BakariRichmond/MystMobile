//This script encompasses speech controls for quests and basic dialougue for an NPC or character

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class SpeechController : MonoBehaviour {
	public TextAsset SpeechTextRes;
	public TextAsset DefaultTextRes;
	public string SpeechText;
	public string DefaultText;
	public GameObject SpeechBG;
	public GameObject Camera;
	public GameObject CameraLockObject;
	public GameObject Player;
	public Animator anim;
	public GameObject NextButton;

	public GameObject Menu;
	public string SpeakerName;
	public Sprite SpeakerImage;
	public GameObject Model;
	
	public bool look = false;
	public bool turning = false;
	public AudioSource audio1;
	public AudioSource audio2;
	public int test = 0;
	public bool inCollider = false;
	public bool UIButton = false;
	public GameObject InteractButton;
	public GameObject CloseButton;
	public GameObject InteractHandler;
	public bool QuestComplete;
	public bool walkIn;
	public bool noCameraMove;
	bool initWalkIn;
	public string InteractText;
	public bool trigger;
	public bool triggerStart;

	public bool isTarget;

	Coroutine LookRoutine;

	// Use this for initialization
	void Start () {
		if (walkIn) {
			initWalkIn = true;
		}
		//if there is no quest script, speech text is pulled from default resource
		if (gameObject.GetComponent<QuestScript> () == null) {
			SpeechText = SpeechTextRes.ToString ();

		}

		DefaultText = DefaultTextRes.ToString ();

		//sets animator if relevant
		if (Model != null) {
			anim = Model.GetComponent<Animator> ();
		}

		//sets object references
		InteractButton = GameObject.Find ("InteractButton");
		CloseButton = GameObject.Find ("CloseButton");
		NextButton = GameObject.Find ("NextButton");
		Player = GameObject.Find ("OverWorldPlayer");
		SpeechBG = GameObject.Find ("SpeechBG");
		Camera = GameObject.Find ("Main Camera");
		Menu = GameObject.Find ("Menu");
		InteractHandler = GameObject.Find ("InteractButtonHandler");

	}
	//when clicked, UI button set to true
	public void OnClickOpen () {
		UIButton = true;

	}
	public void OnClickClose () {
		UIButton = true;

	}

	// Update is called once per frame
	void Update () {
		if (inCollider) {
			//sets interact button text to current interact text while in the collider
			if (InteractButton.GetComponentInChildren<Text> ().text != "") {
				InteractButton.GetComponentInChildren<Text> ().text = InteractText;
			}
		}
		//checks if dialouge handler is in finished state and reveals the close button if so
		if (SpeechBG.GetComponent<DialougueHandler> ().finished == 2) {

			NextButton.transform.localScale = new Vector3 (0f, 0f, 0);
			CloseButton.transform.localScale = new Vector3 (1f, 1f, 1);
			if (initWalkIn) {
				InteractHandler.GetComponent<InteractHandler> ().open = true;

			}

		}
		//calls Proceed if dialougue handler finished state is not complete, which moves to next block of dialougue
		 else if (SpeechBG.GetComponent<DialougueHandler> ().finished == 1 & (Input.GetButtonDown ("Fire1") & inCollider)) {

			SpeechBG.GetComponent<DialougueHandler> ().Proceed ();

		}
		//if in collider and press button, initiates dialougue box
		if ((Input.GetButtonDown ("Jump") | InteractHandler.GetComponent<InteractHandler> ().ObjectBool | walkIn) & inCollider) {
			
			InteractHandler.GetComponent<InteractHandler> ().ObjectBool = false;
			triggerStart = true;

			walkIn = false;
			
			
			if (SpeechBG.GetComponent<DialougueHandler> ().finished == 0 | SpeechBG.GetComponent<DialougueHandler> ().finished == 2) {
				//if dialouge handler is in initial stage, bring up next button and close interact button as well as send text to dialougue handler for parsing
				if (SpeechBG.GetComponent<DialougueHandler> ().finished == 0) {
					NextButton.transform.localScale = new Vector3 (1f, 1f, 1);
					InteractButton.transform.localScale = new Vector3 (0f, 0f, 0);
					SpeechBG.GetComponent<DialougueHandler> ().dialougue = SpeechText;
					SpeechBG.GetComponent<DialougueHandler> ().finished = 1;
					CloseButton.transform.localScale = new Vector3 (0f, 0f, 0);
					SpeechBG.GetComponent<DialougueHandler> ().ParseText ();

				} //if in final state, set back to initial state and remove the close button
				else if (SpeechBG.GetComponent<DialougueHandler> ().finished == 2) {
					SpeechBG.GetComponent<DialougueHandler> ().finished = 0;
					CloseButton.transform.localScale = new Vector3 (0f, 0f, 0);
					//sets trigger which indicates speech is finished, and resets triggerStart which indicates speech started
					trigger = true;
					triggerStart = false;
					
					if (!initWalkIn) {
						InteractButton.transform.localScale = new Vector3 (1f, 1f, 1);
					}

					//trigger new quest and set indicator mode

					if (gameObject.GetComponentInChildren<Indicator> ().ActiveQuest == 1) {
						gameObject.GetComponentInChildren<Indicator> ().ActiveQuest = 0;
						SpeechText = DefaultText;
						//calls quest update given gameobject's parent
						GameObject.Find ("Game").GetComponent<QuestManager> ().QuestUpdate (gameObject.transform.parent.name);
						GameObject.Find ("Game").GetComponent<QuestManager> ().UIQuestUpdate ();

					} else if (gameObject.GetComponentInChildren<Indicator> ().ActiveQuest == 3) {
						//quest is marked as complete and quest log is updated
						QuestComplete = true;
						GameObject.Find ("Game").GetComponent<QuestManager> ().UIQuestUpdate ();

						SpeechText = DefaultText;
						gameObject.GetComponentInChildren<Indicator> ().ActiveQuest = 0;

					}
					if (gameObject.GetComponent<QuestScript> () != null) {
						//if there is a quest script, starts the quest
						print ("attempting quest start");
						gameObject.GetComponent<QuestScript> ().QuestTrigger ();
					}
				}

				test += 1;
				//if look boolean is set to true, camera will turn towards NPC
				if (look) {
					
					turning = true;

					

				}
				
				GameObject.Find ("SpeakerName").GetComponent<Text> ().text = SpeakerName;
				GameObject.Find ("SpeakerImage").GetComponent<Image> ().sprite = SpeakerImage;

				if (Menu.GetComponent<PauseToggle> ().open == false) {
					//if noCameraMove is false, camera turns toward gameObject
					if (!noCameraMove) {
						Camera.GetComponent<CameraController> ().CameraLock = gameObject;
					}
					SpeechBG.transform.localScale = new Vector3 (1f, 1f, 1);
					if (Model != null) {
						//set talking animation
						anim.SetBool ("isTalking", true);
					}
					audio1.Play ();

					//toggles pause menu so it cant be opened while in dialougue
					GameObject.Find ("Menu").GetComponent<PauseToggle> ().open = true;

					Menu.transform.localScale = new Vector3 (0f, 0f, 0);

				} else if (Menu.GetComponent<PauseToggle> ().open == true) {
					//if pause menu is open, camera stops turning
					turning = false;
					

					
					if (!noCameraMove) {
						Camera.GetComponent<CameraController> ().rotSpeed = 1;
						Camera.GetComponent<CameraController> ().CameraLock = CameraLockObject;
					}
					
					SpeechBG.transform.localScale = new Vector3 (0f, 0f, 0);
					if (Model != null) {
						anim.SetBool ("isTalking", false);
					}
					audio2.Play ();
					Menu.GetComponent<PauseToggle> ().open = false;

			

					Menu.transform.localScale = new Vector3 (1f, 1f, 1);

				}

			}

		}

		if (turning) {
			if (!noCameraMove) {

				// fast rotation
				float rotSpeed = 2f;

				// distance between target and rotating object
				Vector3 D = Player.transform.position - transform.position;

				// calc Quaternion for rotation
				Quaternion rot = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (D), rotSpeed * Time.deltaTime);

				//Apply rotation 
				gameObject.transform.parent.rotation = rot;

				gameObject.transform.parent.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
			}
		}

	}
	//coroutine which sets turning to true and waits .1seconds
	IEnumerator LookCoroutine () {
		turning = true;

		yield return new WaitForSeconds (.1f);

	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			inCollider = true;

		}
	}
	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag == "Player") {
			inCollider = false;
			if (initWalkIn) {
				walkIn = true;
			}

		}
	}
}