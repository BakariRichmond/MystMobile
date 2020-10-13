//controls saving and loading data
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomClasses;

[System.Serializable]
public class Game : MonoBehaviour {
	public int saveIndex = 0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	
	private Save CreateSaveGameObject () {
		Save save = new Save ();
		//convert playerstats to save
		
		
		
		save.SpellNames = PlayerStats.SpellNames;
		save.LootSpell = PlayerStats.LootSpell;
		save.EquipIndex = PlayerStats.EquipIndex;
		save.StarterSpellsInit = InventoryManager.init;
		save.Zells = PlayerStats.Zells;
		save.Quests = QuestManager.Quests;
		
	
		save.SaveTime = System.DateTime.Now.ToString();
		save.PlayTime = PlayerStats.Playtime;
		save.SerializedPlayerPos = new SerializableVector3(PlayerStats.PlayerPos.x,PlayerStats.PlayerPos.y,PlayerStats.PlayerPos.z);
		save.CurrentScene = PlayerStats.CurrentScene;
		save.lvlUp = PlayerStats.lvlUp;
		save.HP = PlayerStats.HP;
		save.CurrentHP = PlayerStats.CurrentHP;
		save.BasePower = PlayerStats.BasePower;
		save.Level = PlayerStats.Level;
		save.Experience = PlayerStats.Experience;
		save.Luck = PlayerStats.Luck;
		save.MaxExperience = PlayerStats.MaxExperience;
		save.MoveSpeed = PlayerStats.MoveSpeed;
		save.AttackSpeed = PlayerStats.AttackSpeed;
		//resistances
		save.FireRes = PlayerStats.FireRes;
		save.WaterRes = PlayerStats.WaterRes;
		save.WindRes = PlayerStats.WindRes;
		save.EarthRes = PlayerStats.EarthRes;
		save.ElectricRes = PlayerStats.ElectricRes;
		save.LightRes = PlayerStats.LightRes;
		save.DarkRes = PlayerStats.DarkRes;
		//abilities
		save.Scan = PlayerStats.Scan;
		save.ScanPlus = PlayerStats.ScanPlus;
		save.BattleSeed = PlayerStats.BattleSeed;
		save.SpellRefs = PlayerStats.SpellRefs;
		save.ItemList = ItemPickup.ItemList;
		save.lightLevel = TimeTrack.lightLevel;
		save.GameTime = TimeTrack.GameTime;
		
		

		return save;
	}
	
	public void SaveGame () {
		//saves game data
		// 1
		Save save = CreateSaveGameObject ();

		// 2

	

		
		BinaryFormatter bf = new BinaryFormatter ();
		
		
		
		string saveString = ("/gamesave" + saveIndex + ".save" );
		
		FileStream file = File.Create (Application.persistentDataPath + saveString);
		bf.Serialize (file, save);
		file.Close ();

		Debug.Log ("Game Saved");
	}
		
	public void LoadGame () {
		// loads data from a previous save file
		string saveString = ("/gamesave" + saveIndex + ".save" );
		
		if (File.Exists (Application.persistentDataPath + saveString)) {
			//clear stuff

			// 2
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + saveString, FileMode.Open);
			Save save = (Save) bf.Deserialize (file);
			file.Close ();

			//convert save to playerstats
			
			
			PlayerStats.Playtime = save.PlayTime;
			InventoryManager.init = save.StarterSpellsInit;
			
			PlayerStats.SaveFileIndex = saveIndex;
			PlayerStats.CurrentScene = save.CurrentScene;
			PlayerStats.Zells = save.Zells;
			QuestManager.Quests = save.Quests;
			
			PlayerStats.SpellNames = save.SpellNames;
			PlayerStats.LootSpell = save.LootSpell;
			PlayerStats.EquipIndex = save.EquipIndex;
			PlayerStats.PlayerPos = save.SerializedPlayerPos;
			
			PlayerStats.lvlUp = save.lvlUp;
			PlayerStats.HP = save.HP;
			PlayerStats.CurrentHP = save.CurrentHP;
			PlayerStats.BasePower = save.BasePower;
			PlayerStats.Level = save.Level;
			PlayerStats.Experience = save.Experience;
			PlayerStats.Luck = save.Luck;
			PlayerStats.MaxExperience = save.MaxExperience;
			PlayerStats.MoveSpeed = save.MoveSpeed;
			PlayerStats.AttackSpeed = save.AttackSpeed;
			//resistances
			PlayerStats.FireRes = save.FireRes;
			PlayerStats.WaterRes = save.WaterRes;
			PlayerStats.WindRes = save.WindRes;
			PlayerStats.EarthRes = save.EarthRes;
			PlayerStats.ElectricRes = save.ElectricRes;
			PlayerStats.LightRes = save.LightRes;
			PlayerStats.DarkRes = save.DarkRes;
			//abilities
			PlayerStats.Scan = save.Scan;
			PlayerStats.ScanPlus = save.ScanPlus;
			PlayerStats.BattleSeed = save.BattleSeed;
			PlayerStats.SpellRefs = save.SpellRefs;
			ItemPickup.ItemList = save.ItemList;
			TimeTrack.lightLevel = save.lightLevel;
			TimeTrack.GameTime = save.GameTime;
			
			Debug.Log ("Game Loaded");
			//loads scene player last was at
			StartCoroutine(LoadNewScene());
			

		} else {
			Debug.Log ("No save data found!");
		}

	}
	  IEnumerator LoadNewScene() {

       
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = Application.LoadLevelAsync(PlayerStats.CurrentScene);//, LoadSceneMode.Single

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone) {
            yield return null;
        }

    }
}