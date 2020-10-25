using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelocateNPC : MonoBehaviour {
    public bool QuestTarget;
    //public bool QuestProctor;
    public bool setText;
    public TextAsset SetDefaultText;
    public GameObject targetLocation;
    public GameObject rootObject;
    public bool inTrigger;
    public bool active1 = false;
    public GameObject Joystick;
    GameObject TransitionBG;
    Coroutine resetJoystick;
    public GameObject Player;
    // Start is called before the first frame update
    void Start () {
         Joystick = GameObject.Find("MobileJoystick");
        TransitionBG = GameObject.Find("TransitionBG");
         Player = GameObject.Find("OverWorldPlayer");

    }

    // Update is called once per frame
    void Update () {
        if (inTrigger) {
            if (QuestTarget) {
                if (gameObject.GetComponent<Indicator> ().ActiveQuest == 1) {
                     print("in trigger + 1");
                     active1 = true;
                     }
                  

                
            }
 
         
            if(gameObject.GetComponent<SpeechController> ().trigger & active1){
                active1 = false;
                  gameObject.GetComponent<SpeechController> ().trigger = false;
                  resetJoystick = StartCoroutine (ResetJoystick ());
                    
                    if(setText){
                        gameObject.GetComponent<SpeechController> ().SpeechText = SetDefaultText.ToString();
                    }
            }

        }

    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {

            inTrigger = true;

        }
    }
    void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Player") {
            gameObject.GetComponent<SpeechController> ().trigger = false;
            inTrigger = false;

        }
    }

 IEnumerator ResetJoystick () {
       //starts fade transition

         float fadeAmount;
		 float fadeSpeed = 5;
         TransitionBG.GetComponent<Image>().enabled = true;
		  Color tempColor = TransitionBG.GetComponent<Image>().color;
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
       
        //teleport NPC
        rootObject.transform.position = targetLocation.transform.position;
         
        

        
         tempColor = TransitionBG.GetComponent<Image>().color;


        //fade out
        
           while (TransitionBG.GetComponent<Image>().color.a > 0){
               if(TransitionBG.GetComponent<Image>().color.a < 5){
                 fadeSpeed = 5;

             }
			 fadeAmount = tempColor.a - (fadeSpeed * Time.deltaTime);
			 tempColor = new Color(tempColor.r,tempColor.g,tempColor.b,fadeAmount);
		 
          
          TransitionBG.GetComponent<Image>().color = tempColor;
		  yield return null;
		  }
          TransitionBG.GetComponent<Image>().enabled = false;



        yield return new WaitForSeconds (.1f);
        //sets joystick to default position
        Joystick.transform.position = Camera.main.WorldToScreenPoint (Player.transform.position);

    }


}