//Swivels camera to a target position

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwivelCamera : MonoBehaviour {
    public GameObject Camera;
    public GameObject Position;
    public GameObject View;
    bool inTrigger;
    public GameObject InteractHandler;
    public bool toggleSun;
    public GameObject Sun;
    public bool instant;
    // Start is called before the first frame update
    void Start () {
        Sun=GameObject.Find("Sun");
        Camera = GameObject.Find ("Main Camera");
        InteractHandler = GameObject.Find ("InteractButtonHandler");

    }

    // Update is called once per frame
    void Update () {
        //checks for trigger in speech controller which is called after dialouge ends
        if (gameObject.GetComponent<SpeechController> ().trigger) {
            //sets camera to follow player
            
            Camera.GetComponent<CameraController> ().FollowMode = true;
            Camera.GetComponent<CameraController> ().CameraLock = GameObject.Find ("CameraLock");
            if(toggleSun){
            Sun.GetComponent<Light>().cullingMask &= ~(1 << LayerMask.NameToLayer ("Default"));}

        }
        if (inTrigger) {
            


            
            //if interact button is pressed, speech controller auto calls triggerstart and camera rotates toward "view" object
            if (Input.GetButtonDown ("Jump") | gameObject.GetComponent<SpeechController>().triggerStart){
                
                 Camera.GetComponent<CameraController> ().rotSpeed = 50;
                Camera.GetComponent<CameraController> ().FollowMode = false;
                Camera.GetComponent<CameraController> ().CameraLock = View;
                Camera.transform.position = Position.transform.position;
                if(toggleSun){
                Sun.GetComponent<Light>().cullingMask |= (1 << LayerMask.NameToLayer ("Default"));}
                
            }
        }

    }
    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            if(instant){

                 Camera.GetComponent<CameraController> ().rotSpeed = 50;
                Camera.GetComponent<CameraController> ().FollowMode = false;
                Camera.GetComponent<CameraController> ().CameraLock = View;
                Camera.transform.position = Position.transform.position;
                if(toggleSun){
                Sun.GetComponent<Light>().cullingMask |= (1 << LayerMask.NameToLayer ("Default"));}
            }
            
            inTrigger = true;
            

        }
    }
    void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Player") {
            if(instant){
                  //sets camera to follow player
            
            Camera.GetComponent<CameraController> ().FollowMode = true;
            Camera.GetComponent<CameraController> ().CameraLock = GameObject.Find ("CameraLock");
            if(toggleSun){
            Sun.GetComponent<Light>().cullingMask &= ~(1 << LayerMask.NameToLayer ("Default"));}

            }
            
            inTrigger = false;

        }
    }
}