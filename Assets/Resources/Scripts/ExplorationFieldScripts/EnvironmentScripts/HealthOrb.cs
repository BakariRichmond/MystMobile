//this script gives player HP when interacting with object
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour {
	public GameObject Effect;
	public GameObject UIAudio;

	// Use this for initialization
	void Start () {
		UIAudio = GameObject.Find("UIAudio");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
    {
        //adds hp

        if (other.gameObject.tag == "Player")
        {
			//increases player hp by 10 and plays effect+sound,then self destructs

			PlayerStats.CurrentHP += 10;
			if (PlayerStats.CurrentHP >= PlayerStats.HP){PlayerStats.CurrentHP = PlayerStats.HP; }
			GameObject EffectClone = Instantiate (Effect) as GameObject;
		EffectClone.transform.position = gameObject.transform.position;
		//GetComponent<AudioSource>().Play();
		UIAudio.GetComponent<UIAudioHandler>().HPAudio.Play();
		Destroy(EffectClone, 2);
			Destroy(gameObject);
			
		}
	}
}
