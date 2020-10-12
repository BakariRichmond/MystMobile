//this script manages battlefield enemy movements
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public GameObject Enemy;
    public GameObject Field;
    public GameObject Player;
    public GameObject EnemyTempModelHP;
    public Rigidbody PillBlast;
    [SerializeField] public float AttackHeight = .2f;
    private Vector3 loc;

    [SerializeField] private int health = 250;
    [SerializeField] public int power = 20;

    public string coord = "";
    [Tooltip ("a=1, b=2, c=3, d=4, e=5, f=6")]
    [SerializeField] public int x = 6;
    [SerializeField] public int y = 2;
    [SerializeField] private float MoveSpeedmin;
    [SerializeField] private float MoveSpeedmax;
    float MoveSpeed;
    [Tooltip ("Attack recharge speed, lower number = higher speed")]

    [SerializeField] private float AttackSpeed;
    [Tooltip ("Reccomended 1.5; Higher number = higher attack frequency;anything less than 1 will not launch")]
    [SerializeField] private float AttackRate = 1.5f;
    [Tooltip ("Be sure to only select 1 pattern")]
    [SerializeField] private bool VerticlePattern;

    [SerializeField] private bool HorizontalPattern;

    [SerializeField] private bool AgressivePattern;

    [SerializeField] private bool RandomPattern;

    [SerializeField] private bool StationaryPattern;

    [Tooltip ("Elements can be fire, water, ground, or air")]
    [SerializeField] private string EnemyElement;

    [Tooltip ("Elements can be fire, water, ground, or air")]
    [SerializeField] private string AttackElement;
    public bool EnemyAttacking = true;
    Coroutine attackRoutine;
    Coroutine pathRoutine;
    bool started = false;
    public bool reverseShot;

    // Use this for initialization
    void Start () {
        Field = GameObject.Find ("Field");
        EnemyTempModelHP.GetComponent<TextMesh> ().text = health.ToString ();
        MoveSpeed = Random.Range (MoveSpeedmin, MoveSpeedmax);
        Player = GameObject.Find ("BasicTestModel");
        print ("start");
        string coordStart = "" + x + y;
        this.transform.position = GameObject.Find (coordStart).transform.position;

        attackRoutine = StartCoroutine (AttackCoroutine ());
        pathRoutine = StartCoroutine (PathCoroutine ());

    }

    IEnumerator AttackCoroutine () {
        //enemy is in attacking mode
        PlayerMovement PlayerScript = Player.GetComponent<PlayerMovement> ();

        while (EnemyAttacking == true) {
            //The following states recieve random numbers between specified values, and choose a direction based on those values

            int rndm = (int) Mathf.Round (Random.Range (-.5f, AttackRate));
            if (rndm == 0) { } else if (rndm >= 1 & Time.timeScale == 1f) {
                //checks if player is in view
                if (PlayerScript.y == y) {
                    //launch attack
                    Rigidbody PillBlastClone;
                    loc = new Vector3 (transform.position.x, transform.position.y + AttackHeight, transform.position.z);

                    PillBlastClone = Instantiate (PillBlast, loc, transform.rotation);
                    if (!reverseShot) {
                        PillBlastClone.velocity = transform.TransformDirection (Vector3.back * 15);
                    } else {
                        PillBlastClone.velocity = transform.TransformDirection (Vector3.forward * 15);
                    }

                }
            }
            //checks if player is alive, id so enemy stops attacking
            if (Field.GetComponent<FieldScript> ().PlayerDied == true) {
                print ("dead");
                EnemyAttacking = false;
                AgressivePattern = false;

            }
            yield return new WaitForSeconds (AttackSpeed);
        }

    }
    IEnumerator PathCoroutine () {
        //enemy is moving in a designated pattern
        yield return new WaitForSeconds (.5f);
        PlayerMovement PlayerScript = Player.GetComponent<PlayerMovement> ();

        FieldScript fieldscript = Field.GetComponent<FieldScript> ();

        //Toggles stationary movement
        while (StationaryPattern == true) {

            yield return new WaitForSeconds (MoveSpeed);
        }
        //Toggles verticle movement
        while (VerticlePattern == true) {

            int rndm = (int) Mathf.Round (Random.Range (-.5f, 2.5f));

            if (rndm == 0) {

                if (x > 4) {
                    x = x - 1;
                    coord = "" + x + y;
                    GridDetection Gridscript = GameObject.Find (coord).GetComponent<GridDetection> ();
                    if (Gridscript.EnemyHere == false) {
                        gameObject.transform.position = GameObject.Find (coord).transform.position;
                    } else if (Gridscript.EnemyHere == true) {
                        x = x + 1;
                        coord = "" + x + y;
                    }

                }
            }
            if (rndm == 1) {
                if (x < 6) {
                    x = x + 1;
                    coord = "" + x + y;
                    GridDetection Gridscript = GameObject.Find (coord).GetComponent<GridDetection> ();
                    if (Gridscript.EnemyHere == false) {
                        gameObject.transform.position = GameObject.Find (coord).transform.position;
                    } else if (Gridscript.EnemyHere == true) {
                        x = x - 1;
                        coord = "" + x + y;
                    }
                }
            }

            yield return new WaitForSeconds (MoveSpeed);
        }
        //Toggles horizontal movement
        while (HorizontalPattern == true) {
            int rndm = (int) Mathf.Round (Random.Range (-.5f, 2.5f));

            if (rndm == 0) {

                if (y < 3) {
                    y = y + 1;
                    coord = "" + x + y;
                    GridDetection Gridscript = GameObject.Find (coord).GetComponent<GridDetection> ();
                    //if (!fieldscript.EnemyLocation.Contains(coord))
                    if (Gridscript.EnemyHere == false) {

                        gameObject.transform.position = GameObject.Find (coord).transform.position;
                    } else if (Gridscript.EnemyHere == true) {
                        print ("blocked");
                        y = y - 1;
                        coord = "" + x + y;
                    }
                }
            }
            if (rndm == 1) {
                if (y > 1) {
                    y = y - 1;
                    coord = "" + x + y;
                    print (coord);
                    GridDetection Gridscript = GameObject.Find (coord).GetComponent<GridDetection> ();

                    if (Gridscript.EnemyHere == false) {
                        gameObject.transform.position = GameObject.Find (coord).transform.position;
                    } else if (Gridscript.EnemyHere == true) {
                        y = y + 1;
                        coord = "" + x + y;
                    }
                }
            }

            yield return new WaitForSeconds (MoveSpeed);

        }
        //toggle random pattern
        while (RandomPattern == true) {
            int rndm = (int) Mathf.Round (Random.Range (0f, 5f));

            if (rndm == 1) {

                if (y < 3) {
                    y = y + 1;
                    coord = "" + x + y;
                    GridDetection Gridscript = GameObject.Find (coord).GetComponent<GridDetection> ();
                    if (Gridscript.EnemyHere == false) {
                        gameObject.transform.position = GameObject.Find (coord).transform.position;
                    } else if (Gridscript.EnemyHere == true) {
                        y = y - 1;
                        coord = "" + x + y;
                    }

                }
            } else if (rndm == 2) {
                if (y > 1) {
                    y = y - 1;
                    coord = "" + x + y;
                    GridDetection Gridscript = GameObject.Find (coord).GetComponent<GridDetection> ();
                    if (Gridscript.EnemyHere == false) {
                        gameObject.transform.position = GameObject.Find (coord).transform.position;
                    } else if (Gridscript.EnemyHere == true) {
                        y = y + 1;
                        coord = "" + x + y;
                    }
                }
            }
            if (rndm == 3) {

                if (x > 4) {
                    x = x - 1;
                    coord = "" + x + y;
                    GridDetection Gridscript = GameObject.Find (coord).GetComponent<GridDetection> ();
                    if (Gridscript.EnemyHere == false) {
                        gameObject.transform.position = GameObject.Find (coord).transform.position;
                    } else if (Gridscript.EnemyHere == true) {
                        x = x + 1;
                        coord = "" + x + y;
                    }

                }
            }
            if (rndm == 4) {
                if (x < 6) {
                    x = x + 1;
                    coord = "" + x + y;
                    GridDetection Gridscript = GameObject.Find (coord).GetComponent<GridDetection> ();
                    if (Gridscript.EnemyHere == false) {
                        gameObject.transform.position = GameObject.Find (coord).transform.position;
                    } else if (Gridscript.EnemyHere == true) {
                        x = x - 1;
                        coord = "" + x + y;
                    }
                }
            }

            yield return new WaitForSeconds (MoveSpeed);

        }
        //toggles agressive pattern
        while (AgressivePattern == true) {
            int rndm = (int) Mathf.Round (Random.Range (0f, 5f));

            if (rndm < 3) {
                if (PlayerScript.y > y) {
                    if (y < 3) {
                        y = y + 1;
                        coord = "" + x + y;
                        GridDetection Gridscript = GameObject.Find (coord).GetComponent<GridDetection> ();
                        if (Gridscript.EnemyHere == false) {
                            this.transform.position = GameObject.Find (coord).transform.position;
                        } else if (Gridscript.EnemyHere == true) {
                            y = y - 1;
                            coord = "" + x + y;
                        }
                    }
                } else if (PlayerScript.y < y) {
                    if (y > 1) {
                        y = y - 1;
                        coord = "" + x + y;
                        GridDetection Gridscript = GameObject.Find (coord).GetComponent<GridDetection> ();
                        if (Gridscript.EnemyHere == false) {
                            this.transform.position = GameObject.Find (coord).transform.position;
                        } else if (Gridscript.EnemyHere == true) {
                            y = y + 1;
                            coord = "" + x + y;
                        }

                    }
                }
            }

            if (rndm == 3) {

                if (x > 4) {
                    x = x - 1;
                    coord = "" + x + y;
                    GridDetection Gridscript = GameObject.Find (coord).GetComponent<GridDetection> ();
                    if (Gridscript.EnemyHere == false) {
                        this.transform.position = GameObject.Find (coord).transform.position;
                    } else if (Gridscript.EnemyHere == true) {
                        x = x + 1;
                        coord = "" + x + y;
                    }

                }
            }
            if (rndm == 4) {
                if (x < 6) {
                    x = x + 1;
                    coord = "" + x + y;
                    GridDetection Gridscript = GameObject.Find (coord).GetComponent<GridDetection> ();
                    if (Gridscript.EnemyHere == false) {
                        this.transform.position = GameObject.Find (coord).transform.position;
                    } else if (Gridscript.EnemyHere == true) {
                        x = x - 1;
                        coord = "" + x + y;
                    }
                }
            }

            yield return new WaitForSeconds (MoveSpeed);

        }
    }

    // Update is called once per frame
    void Update () {

    }

    void OnTriggerEnter (Collider other) {

        //registers hp hit
        //if hp is below 0, it is set to 0 and stops all couroutines as well as enables "battlewon" and teleports + destroys enemy object
        if (other.gameObject.tag == "PlayerAttack") {
            PlayerMovement PlayerScript = Player.GetComponent<PlayerMovement> ();
            if (other.gameObject.name == "EnergyBall(Clone)") {
                health = health - PlayerScript.power;
            } else {
                UAttackScript AttackScript = other.gameObject.GetComponent<UAttackScript> ();
                health = health - AttackScript.Power;

            }
            //enemy died
            if (health < 1) {
                health = 0;
                //stops enemies routines
                StopCoroutine (AttackCoroutine ());
                StopCoroutine (PathCoroutine ());
                gameObject.transform.position = new Vector3 (transform.position.x, -10, transform.position.z);
                //lowers enemy count
                Field.GetComponent<FieldScript> ().EnemyCount = Field.GetComponent<FieldScript> ().EnemyCount - 1;
                print ("enemies left: " + Field.GetComponent<FieldScript> ().EnemyCount);
                if (Field.GetComponent<FieldScript> ().EnemyCount < 1) {
                    //if no enemies left, battle won
                    Field.GetComponent<FieldScript> ().BattleWon = true;
                }

                Destroy (gameObject, 0f);

            }
            //displays enemy hp in UI
            EnemyTempModelHP.GetComponent<TextMesh> ().text = health.ToString ();

        }
    }

}