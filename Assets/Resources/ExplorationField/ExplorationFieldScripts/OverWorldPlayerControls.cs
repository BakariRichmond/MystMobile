//this script sets the players controls in the overworld
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class OverWorldPlayerControls : MonoBehaviour {
    public GameObject Field;
    public GameObject Menu;
    public Animator Anim;
    public float speed = 6.0f;

    public float gravity = 20.0f;
    float myAngle;
    bool mobileControl = false;
    public bool devLocation;
    public AudioSource walking;
    public AudioSource running;

    private Vector3 moveDirection = Vector3.zero;
    CharacterController controller;

    

    // Use this for initialization
    void Start () {
        PlayerStats.CurrentScene = SceneManager.GetActiveScene ().name;
        Anim = gameObject.GetComponent<Animator> (); //GameObject.Find("MainCharacter").

        Menu = GameObject.Find ("Menu");
        if(!devLocation){
        gameObject.transform.position = PlayerStats.PlayerPos;
        }
        controller = GetComponent<CharacterController> ();
    }
    void OnTriggerEnter (Collider other) {
        

        if (other.gameObject.tag == "Enemy") {
            //calls engageBattle when collided with enemy
            engageBattle(other.gameObject);

            
        }
    }
    public void engageBattle(GameObject attacker){
        int initialCount = GameObject.Find ("GridPanel").transform.childCount;
            int j = 0;
            
            Vector3 newPos = new Vector3 (gameObject.transform.position.x, 0, gameObject.transform.position.z);
            //sets playerpos to current position
            PlayerStats.PlayerPos = newPos;

            FieldScript fScript = Field.GetComponent<FieldScript> ();
            OverWorldEnemyScript eScript = attacker.GetComponent<OverWorldEnemyScript> ();
            //sets fieldscript experience to experience of enemy
            fScript.currentExp = eScript.EnemyExp;
            //setsfieldscript  battleseed to enemy battleseed
            fScript.BattleSeed = eScript.EnemySeed;
            //loads battle scene
            SceneManager.LoadScene ("BattleFieldScene", LoadSceneMode.Single);

    }

    // Update is called once per frame
    void Update () {
     

        if (Menu.GetComponent<PauseToggle> ().open == false) {
            //increases speed to running speed if the control stick is moved far enough
            if (Input.GetButton ("Fire3") |
                CrossPlatformInputManager.GetAxis ("Vertical") >.40 |
                CrossPlatformInputManager.GetAxis ("Vertical") < -.40 |
                CrossPlatformInputManager.GetAxis ("Horizontal") >.40 |
                CrossPlatformInputManager.GetAxis ("Horizontal") < -.40) {
                speed = 6f;
            //play running animation
                Anim.SetBool ("isRunning", true);
                walking.Stop ();
                if (!running.isPlaying) {
                    running.Play ();
                }

            } else {
                //player speed set to walking and running animation stopped
                speed = 3.5f;
                Anim.SetBool ("isRunning", false);
                running.Stop ();

            }

            Vector3 pos = transform.position;
            
            //if not moving, stop walking animation
            if (Input.GetAxis ("Vertical") == 0 & Input.GetAxis ("Horizontal") == 0 
            & CrossPlatformInputManager.GetAxis ("Vertical") <=.20 
            & CrossPlatformInputManager.GetAxis ("Horizontal") <= .20 
            & CrossPlatformInputManager.GetAxis ("Vertical") >= -.20 
            & CrossPlatformInputManager.GetAxis ("Horizontal") >= -.20 
            ) {Anim.SetBool ("isWalking", false);
                walking.Stop ();}
            else{
                //otherwise play walk animation
                Anim.SetBool ("isWalking", true);
                if (!walking.isPlaying) {
                    walking.Play ();
                }
            }
         
            if (CrossPlatformInputManager.GetAxis ("Vertical") == 0 & CrossPlatformInputManager.GetAxis ("Horizontal") == 0) {
                //if control stick is not moving, turn off mobile control

                mobileControl = false;
            } else {
                mobileControl = true;

            }

            float temp = myAngle;

            if (mobileControl) {
                //sets angle and direction based on control stick

                myAngle = Mathf.Atan2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical")) * Mathf.Rad2Deg;

                moveDirection = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), 0.0f, CrossPlatformInputManager.GetAxis ("Vertical"));

            } else {
                //set angle and direction based on keyboard controls
                myAngle = Mathf.Atan2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")) * Mathf.Rad2Deg;
                moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0.0f, Input.GetAxis ("Vertical"));
            }
            //sets movement speed
            moveDirection *= speed;

            moveDirection.y -= gravity * Time.deltaTime;
            if (myAngle != 0) {
                this.transform.eulerAngles = new Vector3 (0, myAngle, 0);
            } else if (Input.GetAxis ("Vertical") == 1) {
                this.transform.eulerAngles = new Vector3 (0, 0, 0);

            }
            if (mobileControl) {

                if (CrossPlatformInputManager.GetAxis ("Vertical") >.20 |
                    CrossPlatformInputManager.GetAxis ("Vertical") < -.20 |
                    CrossPlatformInputManager.GetAxis ("Horizontal") >.20 |
                    CrossPlatformInputManager.GetAxis ("Horizontal") < -.20) {

                    // Move the controller
                    controller.Move (moveDirection * Time.deltaTime);
                }
            } else { controller.Move (moveDirection * Time.deltaTime); }
 
        } else {
            //if not moving stop animations
            Anim.SetBool ("isWalking", false);
            Anim.SetBool ("isRunning", false);
            walking.Stop ();
            running.Stop ();
            Vector3 stay = new Vector3 (0f, 0f, 0f);
            controller.Move (stay * Time.deltaTime);

        }

    }
}