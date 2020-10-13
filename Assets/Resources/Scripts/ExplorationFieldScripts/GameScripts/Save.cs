//this script holds data for saving
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using CustomClasses;




[Serializable]
public class Save {

	
		public int test;
		
		
		public string SaveTime;
		public bool StarterSpellsInit;
		public float PlayTime;
		public List<string> SpellNames = new List<string> ();
		public string LootSpell = "null";
		public string CurrentScene;
		public int SaveFileIndex;
		public List<bool> EquipIndex = new List<bool> ();
		public int GameTime;
		public float lightLevel;
		 public SerializableVector3 SerializedPlayerPos;
		 public List<Quest> Quests = new List<Quest> ();
		 public int Zells;
		 public bool lvlUp = false;
		 public int HP = 500;
		 public int CurrentHP = 500;
		 public int BasePower = 1;
		 public int Level = 1;
		 public int Experience = 0;
		 public int Luck = 30;
		 public float MaxExperience = 10;
		 public int MoveSpeed;
		 public float AttackSpeed = .1f;
		//resistances
		 public float FireRes = 0;
		 public float WaterRes = 0;
		 public float WindRes = 0;
		 public float EarthRes = 0;
		 public float ElectricRes = 0;
		 public float LightRes = 0;
		 public float DarkRes = 0;
		//abilities
		 public bool Scan = false;
		 public bool ScanPlus = false;
		 
		 public List<enemy> BattleSeed = new List<enemy> ();
		 public List<SpellRef> SpellRefs = new List<SpellRef> ();

		 public List<ItemRef> ItemList = new List<ItemRef>();
		 
		 

	

}