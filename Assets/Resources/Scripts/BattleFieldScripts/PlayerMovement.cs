//This script manages battle field player controls
using System.Collections;
using System.Collections.Generic;
using CustomClasses;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {
    public GameObject MultiText;
    public int Mana = 0;
    //hit on enemy
    public bool Hit = false;
    public int ManaInit = 0;

    public List<GameObject> BattleSpells = new List<GameObject> ();

    public List<GameObject> RandomSpells = new List<GameObject> ();

    public List<GameObject> ActiveSpells = new List<GameObject> ();

    public GameObject[] SpellArray = new GameObject[8];
    public int SpellArrayElements = 0;
    public int ComboMulti = 0;
    public Rigidbody EnergyBall;
    public Rigidbody Shield;
    public Rigidbody Spell1;
    public Rigidbody Spell2;
    public Rigidbody Spell3;
    public GameObject Spell4;
    public GameObject Spell5;
    public GameObject Spell6;

    public GameObject Field;
    public GameObject PlayerHP;
    public GameObject Player;
    public GameObject EnemyTempModel;
    public GameObject SpellMenu;
    public GameObject SpellPanel;

    [SerializeField] public int health = 100;
    private Vector3 loc;
    public string coord = "";
    public int x = 1;
    public int y = 2;
    public int power = 5;

    public float ChargeSpeed;
    public float ShieldChargeSpeed = .1f;
    private bool charged = true;
    private bool ShieldCharged = true;
    private int SpellIndex = 0;
    public bool SpellReady = false;
    public int ComboDisplay;
    public Animator Anim;

    // Use this for initialization
    void Start () {

        Anim = gameObject.GetComponent<Animator> ();
        Anim.SetBool ("BattleMode", true);
        SpellMenu = GameObject.Find ("Confirm");
        MultiText = GameObject.Find ("Multiplier");
        SpellPanel = GameObject.Find ("SpellPanel");

        for (int i = 0; i < PlayerStats.SpellRefs.Count; i++) {

            if (PlayerStats.SpellRefs[i].equipped) {
                //instantiates equipped spells and adds them to battle spells
                GameObject SpellClone = Instantiate (Resources.Load ("BattleField/Projectile Models/Spells/" + PlayerStats.SpellRefs[i].name)) as GameObject;
                
                BattleSpells.Add (SpellClone);
            }
        }

        for (int i = 0; i < BattleSpells.Count; i = i + 0) {
            //randomizes the order of battle spells and puts them into new list
            int rndm = (int) Mathf.Round (Random.Range (-0.49f, BattleSpells.Count - .51f));
            RandomSpells.Add (BattleSpells[rndm]);
            BattleSpells.RemoveAt (rndm);
        }
        print ("randomspells count = " + RandomSpells.Count);
        

        power = PlayerStats.BasePower;

        health = PlayerStats.CurrentHP;

        PlayerHP.GetComponent<Text> ().text = health.ToString ();

    }

    void OnTriggerEnter (Collider other) {
        //reduces health if collided with enemy attack. if hp is less than 0 it becomes 0, object teleports and destructs

        if (other.gameObject.tag == "EnemyAttack") {
            EnemyMove EnemyScript = EnemyTempModel.GetComponent<EnemyMove> ();
            health = health - EnemyScript.power;
            if (health < 1) {
                health = 0;
                gameObject.transform.position = new Vector3 (transform.position.x, -10, transform.position.z);
                Field.GetComponent<FieldScript> ().PlayerDied = true;
                PlayerStats.LootSpell = "null";
                Destroy (gameObject, 1);

            }
            PlayerHP.GetComponent<Text> ().text = health.ToString ();
        }
    }

    // Update is called once per frame
    void Update () {

        //Spawns and sends projectile
        if ((Input.GetKeyDown ("space") | CrossPlatformInputManager.GetButtonUp ("Shoot")) & Time.timeScale == 1f) {
            if (charged & ShieldCharged) {
                Anim.SetTrigger ("MagicBasic");
                Rigidbody EnergyBallClone;
                loc = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);

                EnergyBallClone = Instantiate (EnergyBall, loc, transform.rotation);
                EnergyBallClone.velocity = transform.TransformDirection (Vector3.forward * 15);
                charged = false;
                StartCoroutine (RechargeCoroutine ());
            }
        }
        //Teleports player to Field grid coordinates based on input
        //moves up
        if ((Input.GetKeyDown (KeyCode.W) | CrossPlatformInputManager.GetButtonDown ("Forward")) & Time.timeScale == 1f) {
            if (x < 3 & ShieldCharged) {
                x = x + 1;

                coord = "" + x + y;
                Player.transform.position = GameObject.Find (coord).transform.position;
            }
        }
        //moves left
        if ((Input.GetKeyDown (KeyCode.A) | CrossPlatformInputManager.GetButtonDown ("Left")) & Time.timeScale == 1f) {
            if (y > 1 & ShieldCharged) {

                y = y - 1;
                coord = "" + x + y;
                Player.transform.position = GameObject.Find (coord).transform.position;
            }

        }
        //moves down
        if ((Input.GetKeyDown (KeyCode.S) | CrossPlatformInputManager.GetButtonDown ("Back")) & Time.timeScale == 1f) {
            if (x > 1 & ShieldCharged) {
                x = x - 1;

                coord = "" + x + y;
                Player.transform.position = GameObject.Find (coord).transform.position;
            }

        }
        //moves right
        if ((Input.GetKeyDown (KeyCode.D) | CrossPlatformInputManager.GetButtonDown ("Right")) & Time.timeScale == 1f) {
            if (y < 3 & ShieldCharged) {

                y = y + 1;
                coord = "" + x + y;
                Player.transform.position = GameObject.Find (coord).transform.position;
            }
        }
        

        if ((Input.GetKeyDown (KeyCode.R) | CrossPlatformInputManager.GetAxis ("Vertical") >= .5) & Time.timeScale == 1f)

        {
            //if spell button pressed, instantiate spell
            if (charged & ShieldCharged) {
                print ("CASTING");

                Rigidbody SpellClone;
                loc = new Vector3 (transform.position.x, transform.position.y + .5f, transform.position.z + 1);

                if (ActiveSpells.Count > 0) {
                    SpellClone = Instantiate (ActiveSpells[0].GetComponent<InvSlot> ().Spell, loc, transform.rotation);
                    SpellClone.velocity = transform.TransformDirection (Vector3.forward * 15);
                    ActiveSpells.RemoveAt (0);
                    Destroy (SpellPanel.transform.GetChild (0).gameObject);

                    SpellIndex = SpellIndex + 1;
                    Anim.SetTrigger ("MagicSpecial");
                    charged = false;
                    StartCoroutine (RechargeCoroutine ());
                } else { print ("out of casts!"); }

            }

        }
        if ((Input.GetKeyDown (KeyCode.F) | CrossPlatformInputManager.GetAxis ("Vertical") <= -.5) & Time.timeScale == 1f) {
            //if shield  button pressed, instantiate shield
            if (ShieldCharged) {
                print ("SHIELDING");
                Rigidbody ShieldClone;
                loc = new Vector3 (transform.position.x, transform.position.y + .5f, transform.position.z + .5f);

                ShieldClone = Instantiate (Shield, loc, transform.rotation);

                ShieldCharged = false;
                StartCoroutine (ShieldRechargeCoroutine ());
            }
        }
        if (SpellReady) {

            string CurrentSlot = "0";
            GameObject SlotObj = GameObject.Find ("0");

            for (int i = 0; i < 8; i++) {
                CurrentSlot = i.ToString ();

                if (RandomSpells.Count > 0) {
                    SpellArray[i] = RandomSpells[0];
                    //remove spell form randomspell list
                    RandomSpells.RemoveAt (0);
                    SlotObj = GameObject.Find (CurrentSlot);

                    SlotObj.GetComponent<Text> ().text = SpellArray[i].name;

                } else {
                    SlotObj = GameObject.Find (CurrentSlot);

                    SlotObj.GetComponent<Text> ().text = "null";
                }

            }

            print (SpellArray.Length);
            for (int i = 0; i < SpellArray.Length - 1; i++) {
                

                if (SpellArray[i] != null) { SpellArrayElements = SpellArrayElements + 1; }
            }
            SpellReady = false;
        }

        if (SpellMenu.GetComponent<SpellMenu> ().RuneActive) {
            SpellIndex = 0;
            for (int i = 0; ActiveSpells.Count > 0; i = i + 0) {
                //clears active spells
                ActiveSpells.RemoveAt (0);
            }
            string CurrentSlot = "0";

            GameObject SlotObj = GameObject.Find ("0");

            for (int i = 0; i < 8; i++) {
                CurrentSlot = i.ToString ();
                if (SpellArray[i] == null) {
                    if (RandomSpells.Count > 0) {
                        SpellArray[i] = RandomSpells[0];
                        print (RandomSpells[0] + "rnd");
                        RandomSpells.RemoveAt (0);
                        SlotObj = GameObject.Find (CurrentSlot);
                        
                        SlotObj.GetComponent<Text> ().text = SpellArray[i].name;

                    } else {
                        SlotObj = GameObject.Find (CurrentSlot);

                        SlotObj.GetComponent<Text> ().text = "null";
                    }
                }

            }

           
            for (int i = 0; i < SpellArray.Length - 1; i++) {

                if (SpellArray[i] != null) { SpellArrayElements = SpellArrayElements + 1; }
            }
            //pasues time
            Time.timeScale = 0f;

            SpellMenu.GetComponent<SpellMenu> ().RuneActive = false;
        }

    }
    //causes wait time to shoot again
    IEnumerator RechargeCoroutine () {
        print ("charging");
        yield return new WaitForSeconds (ChargeSpeed);
        print ("finished charging after seconds:" + ChargeSpeed);
        charged = true;
    }
    IEnumerator ShieldRechargeCoroutine () {

        yield return new WaitForSeconds (ShieldChargeSpeed);
        ShieldCharged = true;
    }
}