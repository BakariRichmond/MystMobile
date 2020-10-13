//controls main menu UI options and loading
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	Coroutine animationCoroutine;
	bool loadMode;
	 private bool loadScene = false;
	 private bool started = false;
    public Text loadingText;
	public Text startText;
	public Image LoadingBG;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//checks if scene needs to be loaded
		if (loadScene) {

			//pulse the transparency of the loading text to let the player know that the computer is still working.
			LoadingBG.enabled = true;
			StartCoroutine(FadeRoutine());
			StartCoroutine(MusicFadeRoutine());
			loadingText.enabled = true;
			loadingText.color = new Color (loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong (Time.time, 1));

		}else if (!started){
			//blinks text if started variable is false
			startText.color = new Color (startText.color.r, startText.color.g, startText.color.b, Mathf.PingPong (Time.time, 1));
		}

	}
	   IEnumerator LoadNewScene() {

       
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load CityScene 
        AsyncOperation async = Application.LoadLevelAsync("CityScene");

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done
        while (!async.isDone) {
            yield return null;
        }
     }
	 IEnumerator FadeRoutine() {
		 //fades out
		 float fadeAmount;
		 float fadeSpeed = 1;
		  Color tempColor = LoadingBG.color;

		 while (LoadingBG.color.a < 1){
			 fadeAmount = tempColor.a + (fadeSpeed * Time.deltaTime);
			 tempColor = new Color(tempColor.r,tempColor.g,tempColor.b,fadeAmount);
		 
          
          LoadingBG.color = tempColor;
		  yield return null;
		  }
	 }
	  IEnumerator MusicFadeRoutine() {
		 //music fades out
		 float fadeSpeed = .5f;
		  
		  AudioSource BGM = GameObject.Find("Main Camera").GetComponent<AudioSource>();

		 while (BGM.volume > 0){
			 BGM.volume = BGM.volume - (fadeSpeed * Time.deltaTime);
			 
		 
          
          
		  yield return null;
		  }
	 }

	IEnumerator AnimationCoroutine () {
		GameObject.Find("MainMenuButtonsBG").transform.localScale = new Vector3 (0f, 0f, 0);
		if (!loadMode) {
			//player plays get up animation before loading new scene
			GameObject.Find ("OverWorldPlayer").GetComponent<Animator> ().SetTrigger ("getUp");
			yield return new WaitForSeconds (2);
			loadScene = true;

			
			loadingText.text = "Loading a new adventure...";
			StartCoroutine(LoadNewScene());
			
		} else {
			//player plays get up animation before loading new scene
			GameObject.Find ("OverWorldPlayer").GetComponent<Animator> ().SetTrigger ("getUp");
			yield return new WaitForSeconds (2);

			loadScene = true;

			
			loadingText.text = "Loading where you left off...";

			//loads game file
			GameObject.Find ("Game").GetComponent<Game> ().LoadGame ();

		}

	}
	public void clickNewGame () {
		animationCoroutine = StartCoroutine (AnimationCoroutine ());

	}
	public void LoadGame () {
		loadMode = true;
		animationCoroutine = StartCoroutine (AnimationCoroutine ());

	}
	public void clickLoadGame () {

		Vector3 scale = new Vector3 (0f, 0f, 0);
		if (GameObject.Find ("SaveBG").transform.localScale == scale) {
			//brings up save background and removes main menu buttons

			GameObject.Find ("SaveBG").transform.localScale = new Vector3 (1f, 1f, 1);
			GameObject.Find ("MainMenuButtonsBG").transform.localScale = new Vector3 (0f, 0f, 0);

		} else {
			//removes save background and brings up main menu buttons
			GameObject.Find ("SaveBG").transform.localScale = new Vector3 (0f, 0f, 0);
			GameObject.Find ("MainMenuButtonsBG").transform.localScale = new Vector3 (1f, 1f, 1);


		}

	}
	public void clickSettings () {

	}
	public void clickStart () {
		started = true;
		GameObject.Find ("Panel").transform.localScale = new Vector3 (0f, 0f, 0);
		GameObject.Find ("MainMenuButtonsBG").transform.localScale = new Vector3 (1f, 1f, 1);

	}
}