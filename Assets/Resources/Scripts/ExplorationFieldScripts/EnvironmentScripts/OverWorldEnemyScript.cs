//controls movement of enemies in the overworld
using System.Collections;
using System.Collections.Generic;
using CustomClasses;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class OverWorldEnemyScript : MonoBehaviour {
    public int EnemyExp;
    [SerializeField] public string EnemyName;
    [SerializeField] public string Enemy1Name;
    [SerializeField] public int Enemy1Rate;
    [SerializeField] public string Enemy2Name;
    [SerializeField] public int Enemy2Rate;
    [SerializeField] public string Enemy3Name;
    [SerializeField] public int Enemy3Rate;
    [SerializeField] public string Enemy4Name;
    [SerializeField] public int Enemy4Rate;
    [SerializeField] public string Enemy5Name;
    [SerializeField] public int Enemy5Rate;
    [SerializeField] public string Enemy6Name;
    [SerializeField] public int Enemy6Rate;
    [SerializeField] public string Enemy7Name;
    [SerializeField] public int Enemy7Rate;
    [SerializeField] public string Enemy8Name;
    [SerializeField] public int Enemy8Rate;
    [SerializeField] public float speed = 1;
    public GameObject Menu;
    Coroutine pathRoutine;
    public Coroutine waitRoutine;
    public GameObject Player;
    public bool Wander = true;
    bool wait = false;
    Vector3 pos;
    float MoveSpeed = .02f;
    float MoveForce = 15;
    public bool init = false;
    public bool Chase = false;

    public int rate = 50;

    public List<enemy> EnemySeed = new List<enemy> ();
    public bool disableBasicAI;

    void OnTriggerEnter (Collider other) {
        

        if (other.gameObject.tag == "Player") { }
    }
    // Use this for initialization
    void Start () {
        Menu = GameObject.Find ("Menu");

        Player = GameObject.Find ("PlayerCollider");
        //creates new enemys and add them to enemy seed with their spawnrates
        enemy Enemy = new enemy ();
        Enemy.name = EnemyName;
        Enemy.spawnRate = 100;
        EnemySeed.Add (Enemy);
        if (Enemy1Name != null && Enemy1Rate != 0) {
            enemy Enemy1 = new enemy ();
            Enemy1.name = Enemy1Name;
            Enemy1.spawnRate = Enemy1Rate;
            EnemySeed.Add (Enemy1);
        }
        if (Enemy2Name != null && Enemy2Rate != 0) {
            enemy Enemy2 = new enemy ();
            Enemy2.name = Enemy2Name;
            Enemy2.spawnRate = Enemy2Rate;
            EnemySeed.Add (Enemy2);
        }
        if (Enemy3Name != null && Enemy3Rate != 0) {
            enemy Enemy3 = new enemy ();
            Enemy3.name = Enemy3Name;
            Enemy3.spawnRate = Enemy3Rate;
            EnemySeed.Add (Enemy3);
        }
        if (Enemy4Name != null && Enemy4Rate != 0) {
            enemy Enemy4 = new enemy ();
            Enemy4.name = Enemy4Name;
            Enemy4.spawnRate = Enemy4Rate;
            EnemySeed.Add (Enemy4);
        }
        if (Enemy5Name != null && Enemy5Rate != 0) {
            enemy Enemy5 = new enemy ();
            Enemy5.name = Enemy5Name;
            Enemy5.spawnRate = Enemy5Rate;
            EnemySeed.Add (Enemy5);
        }
        if (Enemy6Name != null && Enemy6Rate != 0) {
            enemy Enemy6 = new enemy ();
            Enemy6.name = Enemy6Name;
            Enemy6.spawnRate = Enemy6Rate;
            EnemySeed.Add (Enemy6);
        }
        if (Enemy7Name != null && Enemy7Rate != 0) {
            enemy Enemy7 = new enemy ();
            Enemy7.name = Enemy7Name;
            Enemy7.spawnRate = Enemy7Rate;
            EnemySeed.Add (Enemy7);
        }
        if (Enemy8Name != null && Enemy8Rate != 0) {
            enemy Enemy8 = new enemy ();
            Enemy8.name = Enemy8Name;
            Enemy8.spawnRate = Enemy8Rate;
            EnemySeed.Add (Enemy8);
        }
        if (!disableBasicAI) {
        waitRoutine = StartCoroutine (WaitCoroutine ());
        }

    }

    // Update is called once per frame
    void Update ()

    {
        if (!disableBasicAI) {
            if (Chase == true & Menu.GetComponent<PauseToggle> ().open == false) {
                //chases player
                transform.LookAt (Player.transform);
                transform.position = Vector3.MoveTowards (transform.position, Player.transform.position, .03f);
            }
        }

    }
    IEnumerator WaitCoroutine () {
        //pauses for 1 second
        yield return new WaitForSeconds (1);
        pos = transform.position;
        Wander = true;
        pathRoutine = StartCoroutine (PathCoroutine ());

    }
    IEnumerator PathCoroutine () {

        while (Wander == true) {
            //move in random directions
            int rndm = (int) Mathf.Round (Random.Range (0.5f, 6f));

            if (init == false) {
                Chase = false;
                if (rndm == 1) {
                    
                    //forward
                    float time = 0;
                    this.transform.eulerAngles = new Vector3 (0, 90, 0);

                    while (time < 5 && init == false) {
                        time += Time.deltaTime;

                        
                        if (Menu.GetComponent<PauseToggle> ().open == false) {

                            GetComponent<Rigidbody> ().AddForce (transform.forward * MoveForce);
                        }

                        
                        yield return null;
                    }

                } else if (rndm == 2) {
                    
                    //left
                    float time = 0;
                    this.transform.eulerAngles = new Vector3 (0, -90, 0);

                    while (time < 5 && init == false) {
                        time += Time.deltaTime;

                        
                        if (Menu.GetComponent<PauseToggle> ().open == false) {
                            GetComponent<Rigidbody> ().AddForce (transform.forward * MoveForce);
                        }

                        
                        yield return null;
                    }

                }
                if (rndm == 3) {
                    
                    //back
                    float time = 0;
                    this.transform.eulerAngles = new Vector3 (0, 180, 0);

                    while (time < 5 && init == false) {
                        time += Time.deltaTime;

                        
                        if (Menu.GetComponent<PauseToggle> ().open == false) {
                            GetComponent<Rigidbody> ().AddForce (transform.forward * MoveForce);
                        }

                        
                        yield return null;
                    }

                }
                if (rndm == 4) {
                    

                    //right
                    float time = 0;
                    this.transform.eulerAngles = new Vector3 (0, 0, 0);

                    while (time < 5 && init == false) {
                        time += Time.deltaTime;
                        if (Menu.GetComponent<PauseToggle> ().open == false) {
                            
                            GetComponent<Rigidbody> ().AddForce (transform.forward * MoveForce);
                        }

                        //transform.position = pos;
                        yield return null;
                    }

                }
                if (rndm >= 5) {
                    //waits
                    yield return new WaitForSeconds (2);
                }
            } else {
                Chase = true;
            }

            if (init == false) {
                yield return new WaitForSeconds (1);
            } else { yield return null; }

        }

    }
}