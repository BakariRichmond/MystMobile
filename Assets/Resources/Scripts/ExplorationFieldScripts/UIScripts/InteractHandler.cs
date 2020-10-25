//this script handles interactable buttons
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class InteractHandler : MonoBehaviour {
	public bool ObjectBool = false;
	public bool open = false;
	public GameObject InteractButton;
	public GameObject CloseButton;
	public GameObject TriggerObject;
	public GameObject ShopCanvasBG;
	public GameObject ShopCanvasConfirm;
	public GameObject InteractObject;
	public GameObject Menu;
	public GameObject MiniMap;
	public GameObject SpeechBG;

	// Use this for initialization
	void Start () {
		SpeechBG = GameObject.Find("SpeechBG");
		MiniMap=GameObject.Find("MiniMapGroup");
		Menu = GameObject.Find("Menu");
		InteractButton = GameObject.Find ("InteractButton");
		CloseButton = GameObject.Find ("CloseButton");
		ShopCanvasBG = GameObject.Find ("ShopCanvasBG");
		ShopCanvasConfirm = GameObject.Find ("ShopCanvasConfirm");

	}

	// Update is called once per frame
	void Update () {
		

	}
	public void OnClickOpen () {
		//scales up close button and scales down the interact button

		ObjectBool = true;
		print("objectbool is " + ObjectBool);
		if (!open) {

			CloseButton.transform.localScale = new Vector3 (1f, 1f, 1);
		}
		InteractButton.transform.localScale = new Vector3 (0f, 0f, 0);

	}
	public void OnClickClose () {
		//scales up interact button and scales down close button
		ObjectBool = true;
		InteractButton.transform.localScale = new Vector3 (1f, 1f, 1);
		CloseButton.transform.localScale = new Vector3 (0f, 0f, 0);
		SpeechBG.transform.localScale = new Vector3 (0f, 0f, 0);
		//GameObject.Find ("Menu").GetComponent<PauseToggle> ().open = false;
		

		if (open) {
			InteractButton.transform.localScale = new Vector3 (0f, 0f, 0);

			open = false;
		}

	}
	public void OnClickTransport () {
		//calls transport, giving it the target destination for its area

		GameObject.Find ("TeleportLinkPlatform").GetComponent<TeleportLinkPlatform> ().transport (InteractArea.Target);
	}
	public void OpenShop () {
		//opens shop menu and closes pause menu
		ShopCanvasBG.transform.localScale = new Vector3 (1f, 1f, 1);
		GameObject.Find ("Menu").transform.localScale = new Vector3 (0f, 0f, 0);
		MiniMap.transform.localScale = new Vector3 (0f, 0f, 0);

	}
	public void CloseShop () {
		//closes shop menu and opens pause menu
		ShopCanvasBG.transform.localScale = new Vector3 (0f, 0f, 0);
		GameObject.Find ("Menu").transform.localScale = new Vector3 (1f, 1f, 1);
		MiniMap.transform.localScale = new Vector3 (1.719539f, 1.719539f, 1.719539f);

	}
	public void OpenShopConfirm () {
		//opens shop confirmation UI element
		ShopCanvasConfirm.transform.localScale = new Vector3 (1f, 1f, 1);

	}
	public void CloseShopConfirm () {
		//closes shop confirmation UI element
		ShopCanvasConfirm.transform.localScale = new Vector3 (0f, 0f, 0);

	}
	public void OnClickPush () {
		//sets moving to true for pushing objects animation
		ObjectBool = true;
		InteractObject.GetComponent<Push> ().moving = true;

	}
}