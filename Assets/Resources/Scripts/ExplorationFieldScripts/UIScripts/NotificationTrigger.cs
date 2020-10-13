//Triggers notification text
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class NotificationTrigger : MonoBehaviour {

    public GameObject SpeechBG;
    public GameObject NextButton;
    public GameObject InteractButton;
    public GameObject CloseButton;
    public GameObject InteractHandler;
    public GameObject Menu;
    public TextAsset NotifTextRes;
    public string NotifText;
    public string NotifSpeaker;
    public Sprite NotifImage;
    public bool Active = true;
    public bool entered;
    public bool instant;
    private bool init = false;

    // Start is called before the first frame update
    void Start () {
        if(NotifTextRes != null){
        NotifText = NotifTextRes.ToString();}
        NextButton = GameObject.Find ("NextButton");
        InteractButton = GameObject.Find ("InteractPushButton");
        CloseButton = GameObject.Find ("CloseButton");
        SpeechBG = GameObject.Find ("SpeechBG");
        InteractHandler = GameObject.Find ("InteractButtonHandler");
        Menu = GameObject.Find("Menu");

    }

    // Update is called once per frame
    void Update () {
        //if player enters collider set to active:
        if (entered & Active) {
            if (!init) {
                if (Input.GetButtonDown ("Jump") | GameObject.Find ("InteractButtonHandler").GetComponent<InteractHandler> ().ObjectBool | instant) {
                    if (instant) {
                        init = true;
                    }
                    //pause world, and set speaker name and image
                    Menu.GetComponent<PauseToggle> ().open = true;
                    GameObject.Find ("SpeakerName").GetComponent<Text> ().text = NotifSpeaker;
                    GameObject.Find ("SpeakerImage").GetComponent<Image> ().sprite = NotifImage;
                    InteractHandler.GetComponent<InteractHandler> ().ObjectBool = false;
                    //sends text to dialougue handler and parses text
                    if (SpeechBG.GetComponent<DialougueHandler> ().finished == 0) {
                        NextButton.transform.localScale = new Vector3 (1f, 1f, 1);
                        InteractButton.transform.localScale = new Vector3 (0f, 0f, 0);
                        SpeechBG.GetComponent<DialougueHandler> ().dialougue = NotifText;
                        SpeechBG.GetComponent<DialougueHandler> ().finished = 1;
                        CloseButton.transform.localScale = new Vector3 (0f, 0f, 0);
                        SpeechBG.GetComponent<DialougueHandler> ().ParseText ();
                        SpeechBG.transform.localScale = new Vector3 (1f, 1f, 1);
                        

                    }

                }
                
            }
            //if dialougue is finished, open close button
            if (SpeechBG.GetComponent<DialougueHandler> ().finished == 2) {
                        SpeechBG.GetComponent<DialougueHandler> ().finished = 0;
                        CloseButton.transform.localScale = new Vector3 (1f, 1f, 1);
                        //SpeechBG.transform.localScale = new Vector3 (0f, 0f, 0);
                    }
            Vector3 open = new Vector3 (1f, 1f, 1);
                if (GameObject.Find ("InteractButton").transform.localScale == open) {
                    //reopen interact button and close all other dialougue UI elements
                    GameObject.Find ("InteractButton").transform.localScale = new Vector3 (0f, 0f, 0);
                    if(!instant){
                    InteractButton.transform.localScale = new Vector3 (1f, 1f, 1);}
                    Menu.GetComponent<PauseToggle> ().open = false;
                    SpeechBG.transform.localScale = new Vector3 (0f, 0f, 0);
                    CloseButton.transform.localScale = new Vector3 (0f, 0f, 0);
                    InteractHandler.GetComponent<InteractHandler> ().ObjectBool = false;

                }
        }

    }

    void OnTriggerEnter (Collider other) {
        //sets entered to true on trigger enter
        if (other.gameObject.tag == "Player" & Active) {
            entered = true;

        }
    }

    void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Player" & Active) {
            //sets entered to false and closes dialougue UI elements
            entered = false;
            GameObject.Find ("InteractButton").transform.localScale = new Vector3 (0f, 0f, 0);
            InteractButton.transform.localScale = new Vector3 (0f, 0f, 0);
            SpeechBG.transform.localScale = new Vector3 (0f, 0f, 0);
            CloseButton.transform.localScale = new Vector3 (0f, 0f, 0);
            NextButton.transform.localScale = new Vector3 (0f, 0f, 0);
            InteractHandler.GetComponent<InteractHandler> ().ObjectBool = false;

        }
    }
}