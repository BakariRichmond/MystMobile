//This script Keeps track of game time and adjusts light levels
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTrack : MonoBehaviour {
	static public int GameTime = 0;
	static public bool initStartingTime = false;
	public int StartingTime = 600;
	public bool GameTimePause;
	public GameObject Sun;
	static public float lightLevel;
	public byte Rounded;
	public byte RoundedHalf;
	public int GameTimeShow;
	public GameObject TimeBG;
	string cycle;
	float rotX;

	// Use this for initialization
	void Start () {
		if(!initStartingTime){
			initStartingTime = true;
			GameTime = StartingTime;

		}
		TimeBG = GameObject.Find ("TimeBG");

		Sun = GameObject.Find ("Sun");
		InvokeRepeating ("AddValue", 0, 1); // function string, start after float, repeat rate float
		InvokeRepeating ("IncrementTime", 0, 2);

		//initialize light level

		if (lightLevel > 255) {
			lightLevel = 255;
		} else if (lightLevel < 0) {

			lightLevel = 0;

		}

		Rounded = Convert.ToByte (lightLevel);
		if (Rounded > 255) {
			Rounded = 255;
		} else if (Rounded < 0) {

			Rounded = 0;

		}
		//find sun object and set to initial color and intensity

		if (Sun != null) {
			Sun.GetComponent<Light> ().color = new Color32 (Rounded, Rounded, Rounded, 255);
			Sun.GetComponent<Light> ().intensity = ((float) Rounded / 250);
			
			
			if(GameTime >= 720){

				rotX = (GameTime - 720) * .25f;
			}
			else{
				rotX = GameTime * .25f;
			}
			rotX += 90;
			if (rotX >180 ){
				rotX = rotX - 180;
			}
			Sun.transform.eulerAngles = new Vector3(rotX,.20f,0);
		}

	}

	// Update is called once per frame
	void Update () {
		GameTimeShow = GameTime;
		//dev option to skip time by 1 hour, and convert/display time in console w/ 12hr format
		if (Input.GetKeyDown ("t")) {

			if (GameTime < 1380) {
				float DividedInt = GameTime / 60;
				GameTime = (Convert.ToInt32 (Math.Floor (DividedInt)) + 1) * 60;

				if (GameTime / 60 < 12) {
					print ("Skipping forward 1 hour (" + GameTime / 60 + ":00 AM)");
					if (GameTime / 60 < 10) {
						TimeBG.GetComponentInChildren<Text> ().text = "0" + GameTime / 60 + ":00 am";
					} else {
						TimeBG.GetComponentInChildren<Text> ().text = GameTime / 60 + ":00 am";

					}
				} else if (GameTime / 60 == 12) {
					print ("Skipping forward 1 hour (" + ((GameTime / 60)) + ":00 PM)");
					TimeBG.GetComponentInChildren<Text> ().text = GameTime / 60 + ":00 pm";

				} else {
					print ("Skipping forward 1 hour (" + ((GameTime / 60) - 12) + ":00 PM)");
					if (GameTime / 60 < 22) { 
						TimeBG.GetComponentInChildren<Text> ().text = "0" + ((GameTime / 60) - 12) + ":00 pm";
					} else {
						TimeBG.GetComponentInChildren<Text> ().text = ((GameTime / 60) - 12) + ":00 pm";
					}
				}

			} else {
				print ("Skipping forward 1 hour (" + ((GameTime / 60) - 11) + ":00 AM)");
				TimeBG.GetComponentInChildren<Text> ().text =  ((GameTime / 60) - 11) + ":00 am";
				GameTime = 0;
			}
		}

	}
	void AddValue () {
		//increments playtime variable
		PlayerStats.Playtime++;

	}

	void IncrementTime () {
		//if game is not paused, game time increments to 1440 then loops
		if (!GameTimePause) {
			if (GameTime <= 1440) {
				GameTime++;
			} else {
				GameTime = 0;
			}
		//convert GameTime to hours and minutes and sets it to UI display
			float hours = Mathf.FloorToInt (GameTime / 60);

			float mins = Mathf.FloorToInt (GameTime % 60);
			if (hours >= 12) {
				hours -= 12;
				cycle = " pm";

			} else {
				cycle = " am";
			}
			string TimeString = "";
			if (hours == 0) {
				hours = 12;
			}

			if (hours < 10) {
				TimeString += "0";

			}

			TimeString += hours + ":";

			if (mins < 10) {
				TimeString += "0";

			}
			TimeString += mins + cycle;
			if (TimeBG != null) {

				TimeBG.GetComponentInChildren<Text> ().text = TimeString;
			}
			//changes light level based on GameTime var
			if (GameTime <= 720) {
				//light levels stays stagnant up to GameTime 180, then increases normally
				if(GameTime <= 180){
					lightLevel = 509.9999999904f - ((0.35416666666f * 1440));
				}
				else{
				lightLevel = 0.35416666666f * GameTime;}
				
			} else {
				if(GameTime >= 960){
					//light levels stays stagnant from GameTime 720 - 960, then decreases normally
				lightLevel = 509.9999999904f - ((0.35416666666f * GameTime));}
				else{
					lightLevel = 0.35416666666f * 720;

				}
			}
			if (lightLevel > 255) {
				lightLevel = 255;
			} else if (lightLevel < 0) {

				lightLevel = 0;

			}
			//converts lightlevel to a rounded byte
			Rounded = Convert.ToByte (lightLevel);
			if (Rounded > 255) {
				Rounded = 255;
			} else if (Rounded < 0) {

				Rounded = 0;

			}
			//Cuts off RoundedHalf ater rounded is less than 126 to stay at 160(stops RGB value at maximum level of red during sunrise/sunset)
			if (Rounded >= 126) {
				RoundedHalf = Rounded;
				RoundedHalf = 160;

			}
			//sets suns intensity and color based off of obtained values based on GameTime
			Sun.GetComponent<Light> ().color = new Color32 (255, RoundedHalf, Rounded, 255);

			Sun.GetComponent<Light> ().intensity = ((float) Rounded / 250);
			
		if(GameTime >= 720){

				rotX = (GameTime - 720) * .25f;
			}
			else{
				rotX = GameTime * .25f;
			}
			rotX += 90;
			if (rotX >180 ){
				rotX = rotX - 180;
			}
			Sun.transform.eulerAngles = new Vector3(rotX,.20f,0);

		}
	}
}