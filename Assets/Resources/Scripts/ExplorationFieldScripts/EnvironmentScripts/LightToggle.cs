//This scrippt toggles players light source
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour {
	public GameObject LightSource;
	public float Brightness;
	public float Range;

	// Use this for initialization
	void Start () {
		LightSource = GameObject.Find("PlayerLight");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
		//sets brightness to new levels
		LightSource.GetComponent<Light>().intensity = Brightness;
		LightSource.GetComponent<Light>().range = Range;
		}


	}
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			//reverts brightness
		LightSource.GetComponent<Light>().intensity = 2;
		LightSource.GetComponent<Light>().range = 10;
		}
		
	}
}
