//sets UI quest details in game menu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class UIQuestScript : MonoBehaviour {
    public string UIName;
    public string UIDesc;
    public string UIReward;
    public string UIProgress;
    public string UIProctor;
    public int UIPayment;

    public bool UICompleted;
    public List<bool> UITalkToProgress = new List<bool> ();
    public List<string> UITalkTo = new List<string> ();
    public List<string> UIDefeat = new List<string> ();
    public List<int> UIDefeatAmount = new List<int> ();
    public List<int> UIDefeatProgress = new List<int> ();

    public GameObject QuestName;
    public GameObject QuestDesc;
    public GameObject QuestProgress;
    public GameObject QuestReward;
    public Vector3 closed;
    static public bool hideText;
    // Start is called before the first frame update
    void Start () {
        closed = new Vector3 (0f, 0f, 0);
        
        string UIProgressTemp = "";
        
        string returnString = "Return to " + UIProctor;
      
        if (UIProgress == returnString | UIProgress == "Completed") {
            UIProgressTemp = UIProgress;
            

        } else {
            
            bool allDone = true;
            for (int i = 0; i < UITalkToProgress.Count; i++) {

                if (UITalkToProgress[i]) {
                    //if quest complete, set quest details + completed

                    UIProgressTemp = UIProgressTemp + "Talk to " + UITalkTo[i] + " [Complete]\n";
                } else {
                    //if not complete, show quest details + in prgress
                    allDone = false;
                    UIProgressTemp = UIProgressTemp + "Talk to " + UITalkTo[i] + " [In Progress]\n";

                }

            }
            for (int i = 0; i < UIDefeat.Count; i++) {
                //sets progress for defeat x enemies quests
                UIProgressTemp = UIProgressTemp + UIDefeat[i] + ":" + UIDefeatProgress[i] + "/" + UIDefeatAmount[i] + "\n";
            }
            if (UIPayment != 0){
                //sets progress for payment quests
                
             UIProgressTemp = UIProgressTemp + PlayerStats.Zells + "/" + UIPayment + "z\n";
             }
           
        }
        //displays quest details
        QuestName.GetComponentInChildren<Text> ().text = UIName;
        QuestDesc.GetComponentInChildren<Text> ().text = UIDesc;
        QuestReward.GetComponentInChildren<Text> ().text = UIReward;
        QuestProgress.GetComponentInChildren<Text> ().text = UIProgressTemp;

    }

    // Update is called once per frame
    void Update () {
        //hides text that shouldnt be viewed
           if(gameObject.transform.GetChild (2).gameObject.transform.localScale != closed & hideText == false){
               
               hideText = true;
        }
        else if(CrossPlatformInputManager.GetButtonDown ("Back") & hideText){
            hideText=false;
        }

        if(hideText){
            
            gameObject.transform.GetChild (0).gameObject.GetComponent<Text>().enabled = false;
            gameObject.GetComponent<Image>().enabled = false;

        }
        else{
             gameObject.transform.GetChild (0).gameObject.GetComponent<Text>().enabled = true;
              gameObject.GetComponent<Image>().enabled = true;
        }

        


    }
}