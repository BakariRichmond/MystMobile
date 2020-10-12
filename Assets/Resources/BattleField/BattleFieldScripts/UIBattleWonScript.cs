//this script handles UI elements after battle is complete
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class UIBattleWonScript : MonoBehaviour {
  public GameObject RuneButton;
  public GameObject Field;
  public GameObject Player;
  public GameObject HP;
  public GameObject Power;
  public GameObject Ability;

  public GameObject FireRes;
  public GameObject WaterRes;
  public GameObject WindRes;
  public GameObject EarthRes;
  public GameObject ElectricRes;
  public GameObject DarkRes;
  public GameObject LightRes;
  public GameObject Lvl;
  public GameObject BG;
  public bool Ready = true;
  int lvl = PlayerStats.Level;
  float PreFire = PlayerStats.FireRes * 100;

  public float PreWater;
  public float PreWind;
  public float PreEarth;
  public float PreElectric;
  public float PreDark;
  public float PreLight;
  public bool BattleWon;

  public GameObject PlayerStat;

  // Use this for initialization
  void Start () {
    RuneButton = GameObject.Find ("RuneButton");
    BG = GameObject.Find ("BGLVLUP");
    BG.transform.localScale = new Vector3 (0f, 0f, 0f);
    Field = GameObject.Find ("Field");
   
    Ready = true;

    HP = GameObject.Find ("MaxHPText");
    Power = GameObject.Find ("PowerText");
    Ability = GameObject.Find ("AbilityText");

    FireRes = GameObject.Find ("FireResText");
    WaterRes = GameObject.Find ("WaterResText");
    WindRes = GameObject.Find ("WindResText");
    EarthRes = GameObject.Find ("EarthResText");
    ElectricRes = GameObject.Find ("ElectricResText");
    DarkRes = GameObject.Find ("DarkResText");
    LightRes = GameObject.Find ("LightResText");
    Lvl = GameObject.Find ("LevelText");

  }

  // Update is called once per frame
  void Update () {
    //checks if battle is won
    if (Field.GetComponent<FieldScript> ().BattleWon == true) {
      var Attacks = GameObject.FindGameObjectsWithTag ("EnemyAttack");
      foreach (var Attack in Attacks) {
        //destroy any enemy projectiles still on the field
        Destroy (Attack);
      }
    //brings up battle won screen and removes rune button
      RuneButton.transform.localScale = new Vector3 (0f, 0f, 0f);
      transform.localScale = new Vector3 (1f, 1f, 0);
      
      int EnemyExp = 10;
      
      if (Ready) {
        //prepares player stats
        int lvl = PlayerStats.Level;
        PreFire = PlayerStats.FireRes * 100;

        PreWater = PlayerStats.WaterRes * 100;
        PreWind = PlayerStats.WindRes * 100;
        PreEarth = PlayerStats.EarthRes * 100;
        PreElectric = PlayerStats.ElectricRes * 100;
        PreDark = PlayerStats.DarkRes * 100;
        PreLight = PlayerStats.LightRes * 100;
        PlayerStats.Experience += 10;
        Ready = false;
      }
      if (PlayerStats.lvlUp == true) {
        //if player leveled up, display new stat increases  compared to old stats
     
        BG.transform.localScale = new Vector3 (1f, 1f, 1f);
        Lvl.GetComponent<Text> ().text = ("Level: " + PlayerStats.Level);
       
        HP.GetComponent<Text> ().text = ("Max HP:" + PlayerStats.HP + "(+20)");
        
        Power.GetComponent<Text> ().text = ("Power:" + PlayerStats.BasePower + "(+2)");
        
        FireRes.GetComponent<Text> ().text = ("Fire Res:" + (PlayerStats.FireRes * 100) + "%(+" + ((PlayerStats.FireRes * 100) - PreFire) + ")");
        
        WaterRes.GetComponent<Text> ().text = ("Water Res:" + (PlayerStats.WaterRes * 100) + "%(+" + ((PlayerStats.WaterRes * 100) - PreWater) + ")");
        
        WindRes.GetComponent<Text> ().text = ("Wind Res:" + (PlayerStats.WindRes * 100) + "%(+" + ((PlayerStats.WindRes * 100) - PreWind) + ")");
        
        EarthRes.GetComponent<Text> ().text = ("Earth Res:" + (PlayerStats.EarthRes * 100) + "%(+" + ((PlayerStats.EarthRes * 100) - PreEarth) + ")");
        
        ElectricRes.GetComponent<Text> ().text = ("Electric Res:" + (PlayerStats.ElectricRes * 100) + "%(+" + ((PlayerStats.ElectricRes * 100) - PreElectric) + ")");
        
        DarkRes.GetComponent<Text> ().text = ("Dark Res:" + (PlayerStats.DarkRes * 100) + "%(+" + ((PlayerStats.DarkRes * 100) - PreDark) + ")");
       
        LightRes.GetComponent<Text> ().text = ("Light Res:" + (PlayerStats.LightRes * 100) + "%(+" + ((PlayerStats.LightRes * 100) - PreLight) + ")");
        
        Ability.GetComponent<Text> ().text = ("New Ability:");
        

        PlayerStats.lvlUp = false;
      }

    } else {

      transform.localScale = new Vector3 (0, 0, 0);
    }
    if (Input.GetKeyDown ("space") | CrossPlatformInputManager.GetButtonDown ("Shoot")) {
      //if player presses button to continue, destroys spells, sets player hp and sends back to overworld
      if (Field.GetComponent<FieldScript> ().BattleWon == true & PlayerStats.lvlUp == false) {

        var Spells = GameObject.FindGameObjectsWithTag ("Spell");
        foreach (var Spell in Spells) {
          Destroy (Spell);
        }

        PlayerStats.CurrentHP = Player.GetComponent<PlayerMovement> ().health;
        print ("back to overworld");
        SceneManager.LoadScene (PlayerStats.CurrentScene, LoadSceneMode.Single);
      }
    }
  }
}