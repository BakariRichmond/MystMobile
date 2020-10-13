//this script causes camera to look at a target
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {
    public GameObject endMarker = null; // create an empty gameobject and assign in inspector
    
    public GameObject Camera;
    public GameObject DefaultLock;
    float speed = 3;
    public bool init = false;
    // Start is called before the first frame update
    void Start () {
        Camera = GameObject.Find("Main Camera");
        DefaultLock = GameObject.Find("CameraLock");

    }
    void LateUpdate () {
        if (init) {
            //sets cameras locked target to endMarker
           Camera.GetComponent<CameraController> ().CameraLock = endMarker;
            
          
        }
        else{
            Camera.GetComponent<CameraController> ().CameraLock = DefaultLock;
            
        }

    }

    // Update is called once per frame
    void Update () {

    }
    void OnTriggerEnter(){
         init = true;
        // Camera.GetComponent<CameraController> ().FollowMode = false;
       

    }
     void OnTriggerExit(){
         init = false;
         //Camera.GetComponent<CameraController> ().FollowMode = true;
       

    }
}