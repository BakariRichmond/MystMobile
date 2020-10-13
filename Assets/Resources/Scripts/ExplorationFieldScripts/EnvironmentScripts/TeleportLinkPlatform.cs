//this script is used for transporting player between two linked objects
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportLinkPlatform : MonoBehaviour
{
    public GameObject Player;
    public Transform Target;
    public GameObject Joystick;
    GameObject TransitionBG;
    Coroutine resetJoystick;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("OverWorldPlayer");
        Joystick = GameObject.Find("MobileJoystick");
        TransitionBG = GameObject.Find("TransitionBG");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            
             resetJoystick = StartCoroutine (ResetJoystick (Target));
        }


    }
    public void transport(Transform target){

        
        resetJoystick = StartCoroutine (ResetJoystick (target));

    }
    IEnumerator ResetJoystick (Transform target) {
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
       
        //teleport player
        Player.transform.position = target.position;
         
        

        
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
