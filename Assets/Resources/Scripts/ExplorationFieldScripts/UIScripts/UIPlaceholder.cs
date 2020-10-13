//keeps track of UI menus open
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.CrossPlatformInput {

	public class UIPlaceholder : MonoBehaviour {
		public List<string> UIList = new List<string> ();
		public GameObject Target;
		public GameObject Back;
		public GameObject MenuButton;
		public GameObject TopMenus;
		public GameObject HealthUI;
		public GameObject UIAudio;

		// Use this for initialization
		void Start () {
			

		}

		// Update is called once per frame
		void Update () {
			if (UIList.Count < 1) {
				//if UIlist count has less than 1 element, close back button and open main menu items
				Back.transform.localScale = new Vector3 (0f, 0f, 0);
				
				if(MenuButton.GetComponent<PauseToggle>().open & CrossPlatformInputManager.GetButtonDown(MenuButton.name)){
				TopMenus.transform.localScale = new Vector3 (1f, 1f, 1);
				
				}
			
				HealthUI.transform.localScale = new Vector3 (1f, 1f, 1);
			} else if (UIList.Count > 0) {
				//if 1 or more elements in UIList, open back button and close main menu items
				Back.transform.localScale = new Vector3 (1f, 1f, 1);
				MenuButton.transform.localScale = new Vector3 (0f, 0f, 0);
				TopMenus.transform.localScale = new Vector3 (0f, 0f, 0);
				HealthUI.transform.localScale = new Vector3 (0f, 0f, 0);
			}

			if (CrossPlatformInputManager.GetButtonDown ("Back")) {
				//if back button pressed, open last element and close the current element
				if (UIList.Count > 0) {

					string LastElement = UIList[UIList.Count - 1];
					
					Target = GameObject.Find (LastElement);
					Target.transform.localScale = new Vector3 (0f, 0f, 0);

					UIList.RemoveAt (UIList.Count - 1);
					print(UIList.Count);
					if (UIList.Count > 0) {
						Target = GameObject.Find (UIList[UIList.Count - 1]);
						print(Target);
					
					Target.transform.localScale = new Vector3 (1f, 1f, 1);
					}
						
				if(UIList.Count == 0){
					//if no elements, open main menus
				TopMenus.transform.localScale = new Vector3 (1f, 1f, 1);
				MenuButton.transform.localScale = new Vector3 (1f, 1f, 1);
				}
				
					UIAudio.GetComponent<UIAudioHandler>().Audio1.Play();

				
				
					

				}

			}
			if (CrossPlatformInputManager.GetButtonDown ("Menu")) {
				

			}
		}
	}
}