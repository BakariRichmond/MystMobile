//controls the save screen UI
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CustomClasses;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]

public class OpenSaveMenu : MonoBehaviour {
	public GameObject spawn;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			if (PlayerStats.SaveFileIndex > -1) {
				//sets save menu value if greater than -1
				GameObject.Find ("SaveDropdown").GetComponent<Dropdown> ().value = PlayerStats.SaveFileIndex;
			}
			GameObject.Find ("Menu").GetComponent<PauseToggle> ().open = true;
			//opens save UI
			GameObject.Find ("SaveBG").transform.localScale = new Vector3 (1f, 1f, 1);
			GameObject.Find ("SaveButton").GetComponentInChildren<Text> ().text = "Save";
			//sets player position to this save spot
			PlayerStats.PlayerPos = spawn.transform.position;

		}

	}
	public void onClick () {
		GameObject.Find ("Menu").GetComponent<PauseToggle> ().open = false;
		GameObject.Find ("SaveBG").transform.localScale = new Vector3 (0f, 0f, 0);

	}
	public void onClickSaved () {
		//sets text to saved
		GameObject.Find ("SaveButton").GetComponentInChildren<Text> ().text = "Saved";

	}
	public void onClickMainMenu () {

		
		string saveString = ("/default.save");

	
			//clears playerstats when going back to main menu
			PlayerStats.Playtime = 0;
			PlayerStats.Zells = 0;
			InventoryManager.init = false;

			PlayerStats.SaveFileIndex = 0;
			PlayerStats.CurrentScene = "OverWorldScene0";
			
			PlayerStats.SpellNames.Clear();
			QuestManager.Quests.Clear();
			PlayerStats.LootSpell = "null";
			PlayerStats.EquipIndex.Clear();
			PlayerStats.PlayerPos = new Vector3 (-3.9f, 0.96f, -55.3f);

			PlayerStats.lvlUp = false;
			PlayerStats.HP = 500;
			PlayerStats.CurrentHP = 500;
			PlayerStats.BasePower = 1;
			PlayerStats.Level = 1;
			PlayerStats.Experience = 0;
			PlayerStats.Luck = 30;
			PlayerStats.MaxExperience = 10;
			
			PlayerStats.AttackSpeed = .1f;
			//resistances
			PlayerStats.FireRes = 0;
			PlayerStats.WaterRes = 0;
			PlayerStats.WindRes = 0;
			PlayerStats.EarthRes = 0;
			PlayerStats.ElectricRes = 0;
			PlayerStats.LightRes = 0;
			PlayerStats.DarkRes = 0;
			//abilities
			PlayerStats.Scan = false;
			PlayerStats.ScanPlus = false;
			PlayerStats.BattleSeed.Clear();
			PlayerStats.SpellRefs.Clear();
			ItemPickup.ItemList.Clear();
			
			Debug.Log ("Refreshed");
			SceneManager.LoadScene ("MenuScene", LoadSceneMode.Single);

		

	}
	public void onCloseLoad () {
		//closes load menu
		Vector3 scale = new Vector3 (0f, 0f, 0);
		if (GameObject.Find ("ConfirmLoadBG").transform.localScale == scale) {

			GameObject.Find ("ConfirmLoadBG").transform.localScale = new Vector3 (1f, 1f, 1);
		} else {
			GameObject.Find ("ConfirmLoadBG").transform.localScale = new Vector3 (0f, 0f, 0);

		}
	}
	public void onCloseSave () {
		//closes save menu
		Vector3 scale = new Vector3 (0f, 0f, 0);
		if (GameObject.Find ("ConfirmSaveBG").transform.localScale == scale) {

			GameObject.Find ("ConfirmSaveBG").transform.localScale = new Vector3 (1f, 1f, 1);
		} else {
			GameObject.Find ("ConfirmSaveBG").transform.localScale = new Vector3 (0f, 0f, 0);

		}

	}
	public void saveSwap () {

		GameObject.Find ("Game").GetComponent<Game> ().saveIndex = GameObject.Find ("SaveDropdown").GetComponent<Dropdown> ().value;

	}
}