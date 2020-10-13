//this script plays an event which spawns an enemy
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnEvent : MonoBehaviour {
    public GameObject Smoke;
    public GameObject SpawnLoc;
    public GameObject SpawnLoc2;
    public GameObject Deer;
    private bool init = false;
    public bool moving = false;
    public Animator Anim;
    static public bool completed;
    // Start is called before the first frame update
    void Start () {
        Deer = GameObject.Find ("Deer");
        Anim = GameObject.Find("Deer Gray").GetComponent<Animator>();
        //if event already completed, destroy involved objects
        if (completed){
            Destroy(GameObject.Find("Deer"));
            Destroy(gameObject);
            
        }
    }

    // Update is called once per frame
    void Update () {

        if (moving) {
            //move deer NPC to target position
            float step = 3 * Time.deltaTime; // calculate distance to move

            Deer.transform.position = Vector3.MoveTowards (Deer.transform.position, SpawnLoc2.transform.position, step);
        }

        if (Vector3.Distance (Deer.transform.position, SpawnLoc2.transform.position) <= 1) {
            moving = false;
            Anim.SetBool ("isRunning", false);
        }

    }
    void OnTriggerEnter (Collider other) {
        

        if (other.gameObject.tag == "Player" & !init) {
            init = true;
            moving = true;
            Anim.SetBool ("isRunning", true);
            //spawns enemy object
            GameObject EnemyClone = Instantiate (Resources.Load ("ExplorationField/ExplorationFieldModels/Enemies/Reaper")) as GameObject;
           
            GameObject SmokeClone = Instantiate (Smoke) as GameObject;
            SmokeClone.transform.position = SpawnLoc.transform.position;
           
            Destroy (SmokeClone, 3);
            Vector3 pos = transform.position;
            EnemyClone.transform.position = SpawnLoc.transform.position;
            //set event to complete
            completed = true;

        }
    }
    void OnTriggerExit (Collider other) {
        

        if (other.gameObject.tag == "Player") {

        }
    }
}