using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class TeleportAfterDialougue : MonoBehaviour {
    GameObject Player;
    public GameObject targetPos;
    bool inTrigger = false;
    public bool resetScene;
        GameObject TransitionBG;
    GameObject LoadingBG;
    GameObject daylightBG;

    // Start is called before the first frame update
    void Start () {
        Player = GameObject.Find ("OverWorldPlayer");
          TransitionBG = GameObject.Find("TransitionBG");
        LoadingBG = GameObject.Find("LoadingBG");
        daylightBG = GameObject.Find("DaylightBG");

    }

    // Update is called once per frame
    void Update () {
       

        
        if (gameObject.GetComponent<SpeechController> ().trigger & inTrigger) {
            print("trigger false");
            gameObject.GetComponent<SpeechController> ().trigger = false;
             if(resetScene){
                 
                print("in reset scene");
                 StartCoroutine (FadeInRoutine ());
                
            }
            else{
                

            
            Player.transform.position = targetPos.transform.position;}
           

        }

    }
    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") { 
            inTrigger = true;
        }
    }
    void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Player") { 
            inTrigger = false;
        }
    }
    IEnumerator FadeInRoutine () {
         print("in Fade Routine");
    
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
          //transport player
          Player.transform.position = targetPos.transform.position;
          PlayerStats.PlayerPos = GameObject.Find("OverWorldPlayer").transform.position;
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
          StartCoroutine (LoadScene (SceneManager.GetActiveScene ().name));
       
        
        
        
    }

     public IEnumerator LoadScene (string scene) {

      
        

        // Start an asynchronous operation
        AsyncOperation async = Application.LoadLevelAsync (scene); //, LoadSceneMode.Single

        // While the asynchronous operation to load the new scene is not complete, continue waiting until its done
        while (!async.isDone) {
            yield return null;
        }

    }

}