//this script destroys object and optionally a target object
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {
    public GameObject Target;
    public bool generalMode;
    public bool selfDestruct;
    public static bool CompletedRoomEvent = false;
    // Start is called before the first frame update
    void Start () {
        //checks if room event has already occured and stops it
        if(CompletedRoomEvent){
            Destroy (GameObject.Find("Quest TriggerWindow Test"));
            Destroy (GameObject.Find("QuestTriggerTest"));
        }

    }

    // Update is called once per frame
    void Update () {
        
        if (gameObject.GetComponent<SpeechController> ().trigger) {
            //if dialougue box ended, deestroys target object and itseld if selfDestruct enabled
            
            Destroy (Target);
            if (selfDestruct) {
                Destroy (gameObject, .2f);
                if(gameObject.name == "Quest TriggerWindow Test"){
                    //sets bool to true for room event
                    CompletedRoomEvent = true;
                }
                
                GameObject.Find("InteractButton").transform.localScale = new Vector3 (0f, 0f, 0);

            }

        }

    }
    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            if (generalMode) {
                //general mode destroys target on contact, optiionally self
                Destroy (Target);
                if (selfDestruct) {
                    Destroy (gameObject);

                }
            }
        }
    }
}