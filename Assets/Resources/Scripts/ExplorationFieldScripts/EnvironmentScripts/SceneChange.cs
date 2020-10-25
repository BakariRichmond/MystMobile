//this script changes to a new scene
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomClasses;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour {
    public string Scene;
    public string setSpawnLoc;
    static public string spawnLoc;
    GameObject Player;
    GameObject Joystick;
    GameObject TransitionBG;
    GameObject LoadingBG;
    bool loadscene;
    
    // Start is called before the first frame update
    void Start () {



        Player = GameObject.Find("OverWorldPlayer");
        Joystick = GameObject.Find("MobileJoystick");
        TransitionBG = GameObject.Find("TransitionBG");
        LoadingBG = GameObject.Find("LoadingBG");

        if (spawnLoc != null) {
            //sets player position to the spawn location variable and move player to that location
            PlayerStats.PlayerPos = GameObject.Find (spawnLoc).transform.position;
            Player.transform.position = GameObject.Find (spawnLoc).transform.position;
            
        }

    }

    // Update is called once per frame
    void Update () {
        if (loadscene) {

			//pulse the transparency of the loading text
			
			Text loadingText = LoadingBG.GetComponentInChildren<Text>();
			loadingText.enabled = true;
			loadingText.color = new Color (loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong (Time.time, 1));

		}
        

    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            loadscene = true;
            spawnLoc = setSpawnLoc;

            
            StartCoroutine (FadeInRoutine ());

        }
    }

    public IEnumerator LoadScene (string scene) {

      
        

        // Start an asynchronous operation
        AsyncOperation async = Application.LoadLevelAsync (scene); //, LoadSceneMode.Single

        // While the asynchronous operation to load the new scene is not complete, continue waiting until its done
        while (!async.isDone) {
            yield return null;
        }

    }

    public IEnumerator FadeInRoutine () {
    
         float fadeAmount;
		 float fadeSpeed = 5;
         Color tempColor = TransitionBG.GetComponent<Image>().color;
          tempColor = new Color(tempColor.r,tempColor.g,tempColor.b,0f);
          TransitionBG.GetComponent<Image>().color = tempColor;
          
         TransitionBG.GetComponent<Image>().enabled = true;
		  
        //fade to black
		 while (TransitionBG.GetComponent<Image>().color.a < 100){
             if(TransitionBG.GetComponent<Image>().color.a > 5){
                 fadeSpeed = 200;

             }
			 fadeAmount = tempColor.a + (fadeSpeed * Time.deltaTime);
			 tempColor = new Color(tempColor.r,tempColor.g,tempColor.b,fadeAmount);
		 
          
          TransitionBG.GetComponent<Image>().color = tempColor;
          
		  yield return null;
		  }
          fadeAmount = 0;
		 fadeSpeed = 5;
         tempColor = LoadingBG.GetComponent<Image>().color;
         tempColor = new Color(tempColor.r,tempColor.g,tempColor.b,0f);
         LoadingBG.GetComponent<Image>().color = tempColor;
         LoadingBG.GetComponent<Image>().enabled = true;
		  tempColor = LoadingBG.GetComponent<Image>().color;
        //fade to black
		 while (LoadingBG.GetComponent<Image>().color.a < 100){
             if(LoadingBG.GetComponent<Image>().color.a > 5){
                 fadeSpeed = 200;

             }
			 fadeAmount = tempColor.a + (fadeSpeed * Time.deltaTime);
			 tempColor = new Color(tempColor.r,tempColor.g,tempColor.b,fadeAmount);
		 
          
          LoadingBG.GetComponent<Image>().color = tempColor;
          
		  yield return null;
		  }
          //load new scene
          StartCoroutine (LoadScene (Scene));
       
        
        
        
    }


    

}