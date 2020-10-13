//destroys projectiles and tracks how many hit an enemy
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destruct : MonoBehaviour {
    [SerializeField] private string DeadlyTag;
    [SerializeField] private float Time = 2;
    public GameObject Player;
    public GameObject MultiText;
    public bool hit = false;

    // Use this for initialization
    void Start () {

        MultiText = GameObject.Find ("Multiplier");
        Player = GameObject.Find ("BasicTestModel");

    }

    // Update is called once per frame
    void Update () {

    }
    //destroys when collided with an enemy tag
    void OnTriggerEnter (Collider other) {
        print ("collided");
        if (other.gameObject.tag == DeadlyTag) {
            if (DeadlyTag == "Enemy") {
                //if projectile collides with enemy, increases combo multiplier
                Player.GetComponent<PlayerMovement> ().ComboMulti += 1;
                //multi caps at 5
                if (Player.GetComponent<PlayerMovement> ().ComboMulti >= 5) {
                    Player.GetComponent<PlayerMovement> ().ComboMulti = 5;
                    MultiText.GetComponent<Text> ().text = Player.GetComponent<PlayerMovement> ().ComboMulti.ToString ();
                } else {

                    MultiText.GetComponent<Text> ().text = Player.GetComponent<PlayerMovement> ().ComboMulti.ToString ();
                }
                //combo multi is added to manainit
                Player.GetComponent<PlayerMovement> ().ManaInit += Player.GetComponent<PlayerMovement> ().ComboMulti;
                print("manaINIT=" + Player.GetComponent<PlayerMovement> ().ManaInit);
                Player.GetComponent<PlayerMovement> ().Hit = true;

            }
            //destroys projectile
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            Destroy (gameObject, .5f);
        }

    }

}