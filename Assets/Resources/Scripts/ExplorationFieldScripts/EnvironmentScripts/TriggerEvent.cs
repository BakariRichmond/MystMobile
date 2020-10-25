using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour {
    public bool Triggered = false;
    public GameObject Police;
    public GameObject Police2;
    public GameObject Police3;
    public GameObject Police4;
    public GameObject Police5;
    public GameObject Police6;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    void OnTriggerEnter (Collider other) {

        if (other.gameObject.tag == "Player") { 
            Triggered = true;
            Police.GetComponent<NPCsearchScript>().triggered = true;
            Police2.GetComponent<NPCsearchScript>().triggered = true;
            Police3.GetComponent<NPCsearchScript>().triggered = true;
            Police4.GetComponent<NPCsearchScript>().triggered = true;
            Police5.GetComponent<NPCsearchScript>().triggered = true;
            Police6.GetComponent<NPCsearchScript>().triggered = true;
        }
    }
    void OnTriggerExit (Collider other) {

        if (other.gameObject.tag == "Player") {
            //Triggered = false;
         }
    }
}