//indexes save slots in save menu
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class DropDownSaveIndexer : MonoBehaviour {
	public GameObject SaveDesc;
	List<string> Saves = new List<string> ();
	string Save0;
	string Save1;
	string Save2;
	

	// Use this for initialization
	
	void Start () {
		UpdateDropdown();
		
	}

	// Update is called once per frame
	void Update () {

	}
	
	public void UpdateDropdown(){

		//adds saves when dropdown updates

		Saves.Add(Save0);
		Saves.Add(Save1);
		Saves.Add(Save2);

		SaveDesc=GameObject.Find("SaveDescription");
		if (PlayerStats.SaveFileIndex > -1) {
			Dropdown.OptionData optionText = new Dropdown.OptionData ();
			//displays save file index in drop down
			optionText.text = ("Save Slot " + PlayerStats.SaveFileIndex + "*");
			gameObject.GetComponent<Dropdown> ().options[PlayerStats.SaveFileIndex] = optionText;
		}

		for (int i = 0; i < 3; i++) {

			string saveString = ("/gamesave" + i + ".save");

			if (File.Exists (Application.persistentDataPath + saveString)) {
			
				//gets data from save file
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + saveString, FileMode.Open);
				Save save = (Save) bf.Deserialize (file);
				file.Close ();
				
				float hours = Mathf.FloorToInt(save.PlayTime/3600);
				float mins = Mathf.FloorToInt((save.PlayTime%3600)/60);
				float secs = Mathf.FloorToInt((save.PlayTime%3600)%60);
				//dispalys play time
				string timeString = hours+"h "+mins+"m "+secs+"s";
				Saves[i] = "Level:" + save.Level + "\nPlaytime:" + timeString + "\nCreated:" + save.SaveTime;
				
				
				
			
			}
			else{
				Saves[i] = "No Save Data";

			}
		}
		//sets save description
		SaveDesc.GetComponentInChildren<Text>().text = Saves[GetComponent<Dropdown>().value];


	}
	public void UpdateSaveDesc(){
		
		SaveDesc.GetComponentInChildren<Text>().text = Saves[GetComponent<Dropdown>().value];

	}
}