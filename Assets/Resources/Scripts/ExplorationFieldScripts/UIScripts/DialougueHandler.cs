//This script parses dialougue from text files into a format for display in-game dialougue system
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougueHandler : MonoBehaviour {
	public TextAsset dialougueRes;
	public string dialougue;
	public List<string> TextList = new List<string> ();
	public int CharCount = 50;
	public GameObject SpeechBG;
	public int TextIndex = 0;
	public int finished = 0;
	public List<string> Speakers = new List<string> ();
	public GameObject NextButton;
	public GameObject CloseButton;

	// Use this for initialization
	void Start () {

		SpeechBG = GameObject.Find ("SpeechBG");
		NextButton = GameObject.Find ("NextButton");
		

	}
	//parses text files to dialougue format
	public void ParseText () {
		TextList.Clear ();
		Speakers.Clear ();
		Speakers.Add ("");

		int counter = 0;
		int placeHolder = 0;
		//creates a list of all words from dialougue reference
		List<string> Words = new List<string> ();
		for (int i = 0; i < dialougue.Length; i++) {
			counter++;
			if (dialougue[i] == ' ') {
				
				Words.Add (dialougue.Substring (placeHolder, counter).Replace ("\n", "").Replace ("\r", ""));
				placeHolder = i + 1;
				counter = 0;
			}

		}

		//resets vars
		int WordsCharacterLength = 0;
		int WordsCounter = 0;
		string TextBlock = "";
		//adds to total character length, sans command /speaker and the subsequent word
		for (int i = 0; i < Words.Count; i++) {

			if (i >= 1) {
				if (Words[i] != "/speaker " & Words[i - 1] != "/speaker ") {
					WordsCharacterLength += Words[i].Length;
				}
			} else {
				if (Words[i] != "/speaker ") {
					WordsCharacterLength += Words[i].Length;
				}

			}
			//checks if /speaker command is used

			if (Words[i] == "/speaker ") {
				Speakers.Add (Words[i + 1]);
				//TextBlock = TextBlock + Words[i+1] and add the subsequent word to an ordered list of speakers;
				print ("changed speaker");
				
				for (int j = WordsCounter; j < i - 1; j++) {
					//add word to TextBlock if not /speaker
					if (Words[j] == "/speaker ") {

						j++;
						
					} else {
						TextBlock = TextBlock + Words[j];
					}

				}
				//adds last word to textblock then adds textblock(single page of dialougue) to the textlist(full list of dialougue pages)
				TextBlock = TextBlock + Words[i - 1];
				WordsCounter = i + 2;
				TextList.Add (TextBlock);
				TextBlock = "";
				WordsCharacterLength = 0;
				i = i + 2;
			//checks if current words count is greater than the Char count space for a given dialouge box, then adds an empty speaker to Speakers list
			
			} else if (WordsCharacterLength >= CharCount) {
				Speakers.Add ("");
				//adds words to textblock up to current word count
				for (int j = WordsCounter; j < i - 1; j++) {

					
					TextBlock = TextBlock + Words[j]; 

				}
				WordsCounter = i - 1;
				TextList.Add (TextBlock);
				TextBlock = "";
				WordsCharacterLength = 0;
				i -= 1;

			}

		}

		for (int j = WordsCounter; j < Words.Count; j++) {
			TextBlock = TextBlock + Words[j];

		}
		TextList.Add (TextBlock);
		//adds text list at current index then increments the index
		SpeechBG.GetComponentInChildren<Text> ().text = TextList[TextIndex];
		TextIndex++;
		//if it is the last text list, remove "next" button and set to finished state 2(final)
		if (TextList.Count == 1) {
			SpeechBG.GetComponentInChildren<Text> ().text = TextList[0];

			TextIndex = 0;
			finished = 2;
			NextButton.transform.localScale = new Vector3 (0f, 0f, 0);
			CloseButton.transform.localScale = new Vector3 (1f, 1f, 1);
			print ("opened close button");

		}

	}

	public void Proceed () {
		//trims any spaces from speakers name
		Speakers[TextIndex] = Speakers[TextIndex].Trim (' '); 


		if (TextIndex < TextList.Count - 1) {
		
			//if speaker is not null, sets speakers name and image in UI dialougue box then moves to next dialougue
			if (Speakers[TextIndex] != "") {

				GameObject.Find ("SpeakerName").GetComponent<Text> ().text = Speakers[TextIndex];

				
				if (Resources.Load ("ExplorationField/ExplorationFieldTextures/SpeakerImages/" + Speakers[TextIndex]) != null) {
					
					GameObject.Find ("SpeakerImage").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("ExplorationField/ExplorationFieldTextures/SpeakerImages/" + Speakers[TextIndex]);
				} else {
					//sets temp image if no reference found
					GameObject.Find ("SpeakerImage").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("ExplorationField/ExplorationFieldTextures/SpeakerImages/PlayerTemp");
				}

			}
			SpeechBG.GetComponentInChildren<Text> ().text = TextList[TextIndex];

			TextIndex++;
		} else {
			//if speaker is not null, sets speakers name and image in UI dialougue box then finishes
			if (Speakers[TextIndex] != "") {
				
				GameObject.Find ("SpeakerName").GetComponent<Text> ().text = Speakers[TextIndex];
				if (Resources.Load ("ExplorationField/ExplorationFieldTextures/SpeakerImages/" + Speakers[TextIndex]) != null) {
					print (Speakers[TextIndex]);
					GameObject.Find ("SpeakerImage").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("ExplorationField/ExplorationFieldTextures/SpeakerImages/" + Speakers[TextIndex]);
				} else {
					GameObject.Find ("SpeakerImage").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("ExplorationField/ExplorationFieldTextures/SpeakerImages/PlayerTemp");
				}
			}
			SpeechBG.GetComponentInChildren<Text> ().text = TextList[TextIndex];

			TextIndex = 0;
			finished = 2;

			NextButton.transform.localScale = new Vector3 (0f, 0f, 0);

		}

	}

	// Update is called once per frame
	void Update () {

	}
}