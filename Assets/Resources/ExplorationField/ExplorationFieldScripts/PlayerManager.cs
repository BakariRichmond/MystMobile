//manages player stats for UI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
	 public GameObject HP;
	 public GameObject Str;
	 public GameObject Status;
	 public GameObject Difficulty;
	 public GameObject FireRes;
	 public GameObject WaterRes;
	 public GameObject WindRes;
	 public GameObject EarthRes;
	 public GameObject ElectricRes;
	 public GameObject DarkRes;
	 public GameObject LightRes;
	 public GameObject Lvl;
	 public GameObject CurrentHP;

	// Use this for initialization
	void Start () {
		HP = GameObject.Find("HP");
		Str = GameObject.Find("Str");
		Status = GameObject.Find("Status");
		Difficulty = GameObject.Find("Difficulty");
		FireRes = GameObject.Find("FireRes");
		WaterRes = GameObject.Find("WaterRes");
		WindRes = GameObject.Find("WindRes");
		EarthRes = GameObject.Find("EarthRes");
		ElectricRes = GameObject.Find("ElectricRes");
		DarkRes = GameObject.Find("DarkRes");
		LightRes = GameObject.Find("LightRes");
		Lvl = GameObject.Find("Lvl");
		CurrentHP = GameObject.Find("CurrentHP");
	}
	
	// Update is called once per frame
	void Update () {
		//sets player stats in UI to players stats
		HP.GetComponent<Text>().text = PlayerStats.HP.ToString();
		Str.GetComponent<Text>().text = PlayerStats.BasePower.ToString();
		FireRes.GetComponent<Text>().text = PlayerStats.FireRes.ToString();
		WaterRes.GetComponent<Text>().text = PlayerStats.WaterRes.ToString();
		WindRes.GetComponent<Text>().text = PlayerStats.WindRes.ToString();
		EarthRes.GetComponent<Text>().text = PlayerStats.EarthRes.ToString();
		ElectricRes.GetComponent<Text>().text = PlayerStats.ElectricRes.ToString();
		DarkRes.GetComponent<Text>().text = PlayerStats.DarkRes.ToString();
		LightRes.GetComponent<Text>().text = PlayerStats.LightRes.ToString();
		Lvl.GetComponent<Text>().text = PlayerStats.Level.ToString();
		CurrentHP.GetComponent<Text>().text = PlayerStats.CurrentHP.ToString();
	}
}
