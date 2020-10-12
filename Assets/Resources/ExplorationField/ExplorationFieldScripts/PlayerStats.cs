//handles player characters stats
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomClasses;
using UnityEngine.SceneManagement;


	public class PlayerStats : MonoBehaviour {


		//basic stats

		static public string CurrentScene = "CityScene";
		static public float Playtime;
		static public int Zells;

		static public List<GameObject> Spells = new List<GameObject> ();
		static public List<string> SpellNames = new List<string> ();
		static public int SaveFileIndex = -1;
		static public string LootSpell = "null";
		static public List<bool> EquipIndex = new List<bool> ();
		static public Vector3 PlayerPos = new Vector3 (123.17f, -25.72f, 50.96f);
		static public bool lvlUp = false;
		static public int HP = 500;
		static public int CurrentHP = 500;
		static public int BasePower = 1;
		static public int Level = 1;
		static public int Experience = 0;
		static public int Luck = 30;
		static public float MaxExperience = 10;
		static public int MoveSpeed;
		static public float AttackSpeed = .1f;
		//resistances
		static public float FireRes = 0;
		static public float WaterRes = 0;
		static public float WindRes = 0;
		static public float EarthRes = 0;
		static public float ElectricRes = 0;
		static public float LightRes = 0;
		static public float DarkRes = 0;
		//abilities
		static public bool Scan = false;
		static public bool ScanPlus = false;
		static public List<enemy> BattleSeed = new List<enemy> ();
		static public List<SpellRef> SpellRefs = new List<SpellRef> ();
		static public List<string> TempBattleEnemies = new List<string>();

	
	
		// Use this for initialization
		void Start () {

			
		}

		// Update is called once per frame
		void Update () {
		//checks if leveld up and increases stats based on level
			if (Experience >= MaxExperience) {

				Level = Level + 1;
				MaxExperience = MaxExperience + (MaxExperience * .10f);
				Experience = 0;
				HP = HP + 20;
				BasePower = BasePower + 2;

				/*!*/
				if (Level == 2) { Scan = true; } 
				else if (Level == 3) { FireRes += .01f; } 
				else if (Level == 4) { } 
				else if (Level == 5) { } 
				else if (Level == 6) { WaterRes += .01f; } 
				else if (Level == 7) { } 
				else if (Level == 8) { } 
				else if (Level == 9) { WindRes += .01f; } 
				else if (Level == 10) { } 
				else if (Level == 11) { } 
				else if (Level == 12) { EarthRes += .01f; } 
				else if (Level == 13) { } 
				else if (Level == 14) { } 
				else if (Level == 15) { ElectricRes += .01f; } 
				else if (Level == 16) { } 
				else if (Level == 17) { } 
				else if (Level == 18) { FireRes += .01f; } 
				else if (Level == 19) { } 
				else if (Level == 20) { ScanPlus = true; } 
				else if (Level == 21) { WaterRes += .01f; } 
				else if (Level == 22) { } 
				else if (Level == 23) { } 
				else if (Level == 24) { WindRes += .01f; } 
				else if (Level == 25) { } 
				else if (Level == 26) { } 
				else if (Level == 27) { EarthRes += .01f; } 
				else if (Level == 28) { } 
				else if (Level == 29) { } 
				else if (Level == 30) { ElectricRes += .01f; } 
				else if (Level == 31) { } 
				else if (Level == 32) { } 
				else if (Level == 33) { FireRes += .01f; } 
				else if (Level == 34) { } 
				else if (Level == 35) { } 
				else if (Level == 36) { WaterRes += .01f; } 
				else if (Level == 37) { } 
				else if (Level == 38) { } 
				else if (Level == 39) { WindRes += .01f; } 
				else if (Level == 40) { } 
				else if (Level == 41) { } 
				else if (Level == 42) { EarthRes += .01f; } 
				else if (Level == 43) { } 
				else if (Level == 44) { } 
				else if (Level == 45) { ElectricRes += .01f; } 
				else if (Level == 46) { } 
				else if (Level == 47) { } 
				else if (Level == 48) { DarkRes += .01f; } 
				else if (Level == 49) { } 
				else if (Level == 50) { } 
				else if (Level == 51) { FireRes += .01f; } 
				else if (Level == 52) { } 
				else if (Level == 53) { } 
				else if (Level == 54) { WaterRes += .01f; } 
				else if (Level == 55) { } 
				else if (Level == 56) { } 
				else if (Level == 57) { WindRes += .01f; } 
				else if (Level == 58) { } 
				else if (Level == 59) { } 
				else if (Level == 60) { EarthRes += .01f; } 
				else if (Level == 61) { } 
				else if (Level == 62) { } 
				else if (Level == 63) { ElectricRes += .01f; } 
				else if (Level == 64) { } 
				else if (Level == 65) { } 
				else if (Level == 66) { DarkRes += .01f; } 
				else if (Level == 67) { } 
				else if (Level == 68) { } 
				else if (Level == 69) { FireRes += .01f; } 
				else if (Level == 70) { } 
				else if (Level == 71) { } 
				else if (Level == 72) { WaterRes += .01f; } 
				else if (Level == 73) { } 
				else if (Level == 74) { } 
				else if (Level == 75) { WindRes += .01f; } 
				else if (Level == 76) { } 
				else if (Level == 77) { } 
				else if (Level == 78) { EarthRes += .01f; } 
				else if (Level == 79) { } 
				else if (Level == 80) { } 
				else if (Level == 81) { ElectricRes += .01f; } 
				else if (Level == 82) { } 
				else if (Level == 83) { } 
				else if (Level == 84) { DarkRes += .01f; } 
				else if (Level == 85) { } 
				else if (Level == 86) { } 
				else if (Level == 87) { LightRes += .01f; } 
				else if (Level == 88) { } 
				else if (Level == 89) { FireRes += .01f; } 
				else if (Level == 90) { } 
				else if (Level == 91) { WaterRes += .01f; } 
				else if (Level == 92) { } 
				else if (Level == 93) { WindRes += .01f; } 
				else if (Level == 94) { } 
				else if (Level == 95) { EarthRes += .01f; } 
				else if (Level == 96) { } 
				else if (Level == 97) { ElectricRes += .01f; } 
				else if (Level == 98) { } 
				else if (Level == 99) {DarkRes += .01f; LightRes += .01f;}

				lvlUp = true;
			}

		}
	}
