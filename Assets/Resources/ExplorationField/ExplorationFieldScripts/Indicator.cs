//this script enables indicators based on the mode
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour {
	public GameObject Pointer;
	public GameObject QuestTarget;
	public GameObject NewQuest;
	public GameObject IncompleteQuest;
	public GameObject CompleteQuest;
	public GameObject InteractButton;
	public string InteractName = "Interact";
	public GameObject TriggerObject;
	public GameObject InteractHandler;
	public bool ObjectBool;
	public int ActiveQuest = 0;
	public GameObject ChatTarget;
	public GameObject ChatTarget2;

	// Use this for initialization
	void Start () {
		if(ChatTarget == null){
			//sets minimap UI elements to 0 scale
			ChatTarget = GameObject.Find("MiniMapDefault");
			ChatTarget.transform.localScale = new Vector3(0,0,0);
		}
		if(ChatTarget2 == null){
			ChatTarget2 = GameObject.Find("MiniMapDefault1");
			ChatTarget2.transform.localScale = new Vector3(0,0,0);

		}
		InteractButton = GameObject.Find ("InteractButton");
		InteractHandler = GameObject.Find ("InteractButtonHandler");
		if (gameObject.GetComponent<QuestScript> () != null) {
			if (gameObject.GetComponent<QuestScript> ().hasQuest == false) {
				ActiveQuest = 0;

			}
		}

	}

	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<SpeechController> () != null) {

			if (ActiveQuest == 0) {
				//if no quest set world and minimap icons to default sprite
				if (QuestTarget != null) {
					QuestTarget.GetComponent<MeshRenderer> ().enabled = false;
				}
				ChatTarget2.GetComponent<SpriteRenderer>().color  = new Color(1,1,1,1);
				
				ChatTarget.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> 
				("ExplorationField/ExplorationFieldModels/Materials/ExploreTextures/Simple Vector Icons/128px/UI_Icon_Chat");

				ChatTarget2.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> 
				("ExplorationField/ExplorationFieldModels/Materials/ExploreTextures/Simple Vector Icons/128px/UI_Icon_Chat");
				CompleteQuest.GetComponent<MeshRenderer> ().enabled = false;
				NewQuest.GetComponent<MeshRenderer> ().enabled = false;
				IncompleteQuest.GetComponent<MeshRenderer> ().enabled = false;

			} else if (ActiveQuest == 1) {
				//if target quest enabled enable quest targets sprite
				ChatTarget2.GetComponent<SpriteRenderer>().color  = new Color(1,1,1,1);
				if (QuestTarget != null) {
					QuestTarget.GetComponent<MeshRenderer> ().enabled = true;
							ChatTarget.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> 
				("ExplorationField/ExplorationFieldModels/Materials/ExploreTextures/Simple Vector Icons/128px/UI_Icon_Chat");
				
				ChatTarget2.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> 
				("ExplorationField/ExplorationFieldModels/Materials/ExploreTextures/Simple Vector Icons/128px/UI_Icon_Chat");
				}

			} else if (ActiveQuest == 2) {
				//if incomplete quest set world and minimap icons to incomplete quest sprite
				
				IncompleteQuest.GetComponent<MeshRenderer> ().enabled = true;
				NewQuest.GetComponent<MeshRenderer> ().enabled = false;
				ChatTarget2.GetComponent<SpriteRenderer>().color  = new Color(1,1,1,1);
						ChatTarget.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> 
				("ExplorationField/ExplorationFieldModels/Materials/ExploreTextures/Simple Vector Icons/128px/Exclamation");
				
				ChatTarget2.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> 
				("ExplorationField/ExplorationFieldModels/Materials/ExploreTextures/Simple Vector Icons/128px/Exclamation");
				

			} else if (ActiveQuest == 3) {
				//if completed quest set world and minimap icons to completed quest sprite
		
				NewQuest.GetComponent<MeshRenderer> ().enabled = false;
				IncompleteQuest.GetComponent<MeshRenderer> ().enabled = false;
				CompleteQuest.GetComponent<MeshRenderer> ().enabled = true;
				ChatTarget2.GetComponent<SpriteRenderer>().color  = new Color(.51f,.87f,.91f,1);
						ChatTarget.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> 
				("ExplorationField/ExplorationFieldModels/Materials/ExploreTextures/Simple Vector Icons/128px/Exclamation");
				
				ChatTarget2.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> 
				("ExplorationField/ExplorationFieldModels/Materials/ExploreTextures/Simple Vector Icons/128px/Exclamation");

			} else if (ActiveQuest == 4) {
				//if new quest set world and minimap icons to new quest sprite
				ChatTarget2.GetComponent<SpriteRenderer>().color  = new Color(.51f,.87f,.91f,1);
				NewQuest.GetComponent<MeshRenderer> ().enabled = true;
						ChatTarget.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> 
				("ExplorationField/ExplorationFieldModels/Materials/ExploreTextures/Simple Vector Icons/128px/UI_Icon_EllipsisHorizontal");
				
				ChatTarget2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> 
				("ExplorationField/ExplorationFieldModels/Materials/ExploreTextures/Simple Vector Icons/128px/UI_Icon_EllipsisHorizontal");

			}
		}

	}
	void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag == "Player") {
			//enable pointer object above NPC and open interact button
			Pointer.GetComponent<MeshRenderer> ().enabled = true;
			InteractButton.GetComponentInChildren<Text> ().text = InteractName;
			InteractButton.transform.localScale = new Vector3 (1f, 1f, 1);
		}
	}
	void OnTriggerExit (Collider other) {

		if (other.gameObject.tag == "Player") {
			//disable pointer object above NPC and close interact button
			Pointer.GetComponent<MeshRenderer> ().enabled = false;
			InteractButton.transform.localScale = new Vector3 (0f, 0f, 0);
		}
	}

}