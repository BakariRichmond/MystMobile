//this script sets up initial battle field state and adds enemies with random locations
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CustomClasses;
using UnityEngine;

public class FieldScript : MonoBehaviour {

    public List<string> EnemyLocation = new List<string> ();
    public List<int[]> CoordList = new List<int[]> ();
    public string PlayerLocation;
    public GameObject Field;
    public bool PlayerDied = false;
    [SerializeField] public int EnemyCount = 0;
    public bool BattleWon = false;
    public bool CountInitialized = true;
    public List<enemy> BattleSeed = new List<enemy> ();
    public int currentExp;

    public GameObject EnemyTemp;

    void Start () {
        //clears tempbattle enemies list and sets hp bar to battle mode
        PlayerStats.TempBattleEnemies.Clear ();
        GameObject.Find ("HPBar").GetComponent<HPbar> ().BattleMode = true;

        currentExp = Field.GetComponent<FieldScript> ().currentExp;

        Quaternion rot = new Quaternion (0, 0, 0, 0);

        //creates random spawn locations for enemies which do not overlap

        int[] rndLocations = new int[9];

        List<int[]> GridLocs = new List<int[]> ();
        GridLocs.Add (new int[] { 4, 1 });
        GridLocs.Add (new int[] { 4, 2 });
        GridLocs.Add (new int[] { 4, 3 });
        GridLocs.Add (new int[] { 5, 1 });
        GridLocs.Add (new int[] { 5, 2 });
        GridLocs.Add (new int[] { 5, 3 });
        GridLocs.Add (new int[] { 6, 1 });
        GridLocs.Add (new int[] { 6, 1 });
        GridLocs.Add (new int[] { 6, 3 });

        for (int j = 0; j <= 8; j++) {

            bool repeatedNum = true;
            while (repeatedNum) {
                int rndmB = (int) Mathf.Round (Random.Range (0.5f, 9.49f));

                repeatedNum = false;

                for (int k = 0; k <= 8; k++) {
                    if (rndLocations[k] == rndmB) {
                        repeatedNum = true;
                    }
                }
                if (repeatedNum == false) {
                    rndLocations[j] = rndmB;
                }

            }

        }
        for (int i = 0; i <= 8; i++) {

            rndLocations[i] = rndLocations[i] - 1;
            print ("array:" + rndLocations[i].ToString () + "===================");
        }

        print (Field.GetComponent<FieldScript> ().BattleSeed.Count + " - COUNT");

        for (int i = 0; i < Field.GetComponent<FieldScript> ().BattleSeed.Count; i++) {
            print (i + "=i");
            //adds enemies based on battle seed
            enemy currentEnemy = Field.GetComponent<FieldScript> ().BattleSeed[i];

            int rndm = (int) Mathf.Round (Random.Range (0, 100));
            print (rndm + ":rndm========================");
            if (rndm <= currentEnemy.spawnRate) {
                //if enemy spawn rate succeeds, enemy is instantiated and enemycount increases
                EnemyCount += 1;

                GameObject EnemyObject = GameObject.Find (currentEnemy.name);

                GameObject EnemyClone = Instantiate (Resources.Load ("Standard Assets/EnemyModels/" + currentEnemy.name)) as GameObject;
                PlayerStats.TempBattleEnemies.Add (currentEnemy.name);

                print (GridLocs[rndLocations[i]][0] + "coord");
                print (GridLocs[rndLocations[i]][1]);
                //sets enemy location
                EnemyClone.GetComponent<EnemyMove> ().x = GridLocs[rndLocations[i]][0];
                EnemyClone.GetComponent<EnemyMove> ().y = GridLocs[rndLocations[i]][1];

            }

        }

    }
    // Update is called once per frame
    void Update () {

    }
}