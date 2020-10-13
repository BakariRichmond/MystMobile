//this script opens menu when button clicked
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour {
	public GameObject menu;
	public bool open;

	// Use this for initialization
	void Start () {
		if (open == false){transform.localScale = new Vector3(0f, 0f, 0f);}
	}
	
	// Update is called once per frame
	void Update () {
		 if (CrossPlatformInputManager.GetButtonDown(gameObject.name))
		 {
		
		menu.transform.localScale = new Vector3(1f, 1f, 1);
		
		 }
	
	}
}
