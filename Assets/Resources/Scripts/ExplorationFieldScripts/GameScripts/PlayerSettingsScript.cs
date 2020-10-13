//this script sets basic player preferences
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsScript : MonoBehaviour {
	[SerializeField]
	private Toggle MusicToggle;
	public Slider SlideMusic;
	[SerializeField]
	private AudioSource myAudio;
	public AudioClip defaultBGM;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	public void Awake () {
		//save music on
		if (!PlayerPrefs.HasKey ("music")) {
			PlayerPrefs.SetInt ("music", 1);
			MusicToggle.isOn = true;
			myAudio.enabled = true;
			PlayerPrefs.Save ();
		}
		// save music off
		else {
			if (PlayerPrefs.GetInt ("music") == 0) {
				myAudio.enabled = false;
				MusicToggle.isOn = false;
			} else {
				myAudio.enabled = true;
				MusicToggle.isOn = true;
			}
		}
		//save music volume

		if (!PlayerPrefs.HasKey ("musicVol")) {
			PlayerPrefs.SetFloat ("musicVol", myAudio.volume);
			SlideMusic.value = 1;
			
			PlayerPrefs.Save ();
		}
	
		else {
			myAudio.volume = PlayerPrefs.GetFloat ("musicVol");
			SlideMusic.value = PlayerPrefs.GetFloat ("musicVol");
		}

	}
	public void ToggleMusic () {
		//enables music
		if (MusicToggle.isOn) {
			PlayerPrefs.SetInt ("music", 1);
			myAudio.enabled = true;
		} else {
			//disables music
			PlayerPrefs.SetInt ("music", 0);
			myAudio.enabled = false;
		}
		PlayerPrefs.Save ();
	}

	public void MusicSlider () {
		//sets volume to slider value
		myAudio.volume = SlideMusic.value;

		PlayerPrefs.SetFloat ("musicVol", myAudio.volume);
		//SlideMusic.value = PlayerPrefs.GetFloat ("musicVol");
		
		
		

		PlayerPrefs.Save ();
	}
}