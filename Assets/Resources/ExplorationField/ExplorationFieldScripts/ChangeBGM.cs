//this script sets background music
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBGM : MonoBehaviour {
    GameObject Game;
    public AudioSource NewBGM;
    bool entered;
    // Start is called before the first frame update
    void Start () {
        Game = GameObject.Find ("Game");
    }

    // Update is called once per frame
    void Update () {
        

    }
    void OnTriggerEnter (Collider other) {
        //sets audio and plays it when in collider
        if (other.gameObject.tag == "Player") {
            entered = true;
            Game.GetComponent<AudioSource>().clip = NewBGM.clip;
             Game.GetComponent<AudioSource>().Play();

        }
    }
     void OnTriggerExit (Collider other) {
         //sets back to default music
        if (other.gameObject.tag == "Player") {
            entered = false;
            Game.GetComponent<AudioSource>().clip = Game.GetComponent<PlayerSettingsScript>().defaultBGM;
            Game.GetComponent<AudioSource>().Play();

        }
    }
}