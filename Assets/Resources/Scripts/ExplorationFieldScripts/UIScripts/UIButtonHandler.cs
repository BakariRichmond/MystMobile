//this script adds menus to a list to keep track of their order
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class UIButtonHandler : MonoBehaviour {

    public GameObject TargetMenu;
    public bool open;
    public bool DefaultName;
    public bool keepOpen;
    public bool TargetChild;
    public int ChildIndex;
    public string Name;
    public GameObject UI;
    public GameObject UIAudio;

    void Start () {
        UI = GameObject.Find ("UI");
        UIAudio = GameObject.Find ("UIAudio");
        if (TargetChild) {
            TargetMenu = gameObject.transform.GetChild (ChildIndex).gameObject;
        }
        if (DefaultName) {
            if (gameObject.GetComponent<UIQuestScript> () != null) {
                gameObject.name = gameObject.GetComponent<UIQuestScript> ().UIName + "_Quest";
                TargetMenu.name = gameObject.GetComponent<UIQuestScript> ().UIName + "_Quest Details";
            }
            Name = gameObject.name;
        }
       

    }

    void OnEnable () {

    }

    public void SetDownState () {
        CrossPlatformInputManager.SetButtonDown (Name);
    }

    public void SetUpState () {
        CrossPlatformInputManager.SetButtonUp (Name);
    }

    public void SetAxisPositiveState () {
        CrossPlatformInputManager.SetAxisPositive (Name);
    }

    public void SetAxisNeutralState () {
        CrossPlatformInputManager.SetAxisZero (Name);
    }

    public void SetAxisNegativeState () {
        CrossPlatformInputManager.SetAxisNegative (Name);
    }

    public void Update () {
        if (CrossPlatformInputManager.GetButtonDown (gameObject.name)) {
            //if current objects button is clicked:
            if (!keepOpen) {
                //open target menu and add it to the UIList
                TargetMenu.transform.localScale = new Vector3 (1f, 1f, 1);

                UI.GetComponent<UIPlaceholder> ().UIList.Add (TargetMenu.name);
                print (TargetMenu.name);
                if (UI.GetComponent<UIPlaceholder> ().UIList.Count > 1) {
                    //if theres more than 1 element, set the previous element and close it
                    string PreviousElement = UI.GetComponent<UIPlaceholder> ().UIList[UI.GetComponent<UIPlaceholder> ().UIList.Count - 2];
                    
                    GameObject PreviousTarget = GameObject.Find (PreviousElement);

                    PreviousTarget.transform.localScale = new Vector3 (0f, 0f, 0);

                }
                UIAudio.GetComponent<UIAudioHandler> ().Audio2.Play ();
                print ("menu clicked");
            } else {
               
                if (UI.GetComponent<UIPlaceholder> ().UIList[UI.GetComponent<UIPlaceholder> ().UIList.Count - 1] != TargetMenu.name) {
                    //open target menu
                    TargetMenu.transform.localScale = new Vector3 (1f, 1f, 1);

                    UI.GetComponent<UIPlaceholder> ().UIList.Add (TargetMenu.name);
                    print (TargetMenu.name);
                    if (UI.GetComponent<UIPlaceholder> ().UIList.Count > 1) {
                        //if theres more than 1 element, set the previous element and close it
                        string PreviousElement = UI.GetComponent<UIPlaceholder> ().UIList[UI.GetComponent<UIPlaceholder> ().UIList.Count - 2];
                        
                        GameObject PreviousTarget = GameObject.Find (PreviousElement);

                    }
                    UIAudio.GetComponent<UIAudioHandler> ().Audio2.Play ();
                    print ("menu clicked");
                }

            }
        }


    }
}