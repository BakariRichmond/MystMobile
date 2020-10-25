using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class DaylightTransition : MonoBehaviour
{
    public int nightTime;
    public int dayTime;

       GameObject TransitionBG;
    GameObject LoadingBG;
    GameObject daylightBG;
    bool init = false;
    
    // Start is called before the first frame update
    void Start()
    {
         TransitionBG = GameObject.Find("TransitionBG");
        LoadingBG = GameObject.Find("LoadingBG");
        daylightBG = GameObject.Find("DaylightBG");

        
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeTrack.GameTime == nightTime & !init){
            init = true;
            daylightBG.GetComponentInChildren<Text>().text = "Night Time";
             daylightBG.transform.localScale = new Vector3 (1f,1f,1);
            
            StartCoroutine (dayFadeInRoutine ());
        }
        else if( TimeTrack.GameTime == dayTime & !init){
            init = true;
            daylightBG.GetComponentInChildren<Text>().text = "Day Time";
             daylightBG.transform.localScale = new Vector3 (1f,1f,1);
             
             StartCoroutine (dayFadeInRoutine ());

        }
        
    }
    IEnumerator dayFadeInRoutine () {
        PlayerStats.PlayerPos = GameObject.Find("OverWorldPlayer").transform.position;



        yield return new WaitForSeconds (1);
        
    
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
