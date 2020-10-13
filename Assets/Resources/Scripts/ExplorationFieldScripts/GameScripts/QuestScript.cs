//this script handles initiating quests
using System.Collections;
using System.Collections.Generic;
using CustomClasses;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour {
    public GameObject QuestGridPanel;

    public TextAsset TextRes;
    public TextAsset TextRepeatRes;
    public TextAsset CompleteTextRes;

    string Text;
    string TextRepeat;
    string CompleteText;

    public string Proctor;
    public string Name;
    public string Desc;
    public string Reward;
    public int Payment;
    public GameObject RewardObject;

    public List<string> TalkTo = new List<string> ();
    public List<bool> BoolListInit = new List<bool> ();
    public List<int> IntListInit = new List<int> ();
    public List<TextAsset> TalkToText = new List<TextAsset> ();
    public List<string> TalkToString = new List<string> ();
    public List<string> Defeat = new List<string> ();
    public List<int> DefeatAmount = new List<int> ();
    static public List<string> QuestProctors = new List<string> ();
    public bool hasQuest;
    public Vector3 closed;
    

    // Start is called before the first frame update
    void Start () {
        closed = new Vector3 (0f, 0f, 0);
        if (hasQuest) {
            //if there is a quuest, converts Text files to string
            Text = TextRes.ToString ();
            TextRepeat = TextRepeatRes.ToString ();
            CompleteText = CompleteTextRes.ToString ();
            //adds quest targets to string
            for (int i = 0; i < TalkToText.Count; i++) {
                TalkToString.Add (TalkToText[i].ToString ());

            }
        }

        Proctor = gameObject.transform.parent.name;
        //checks if proctor is in QuestProctors list and disbles quest if so
        for (int i = 0; i < QuestProctors.Count; i++) {
            print ("comparing " + Proctor + " to " + QuestProctors[i]);
            if (Proctor == QuestProctors[i]) {
                print ("found proc match");
                hasQuest = false;
            }
        }

        QuestGridPanel = GameObject.Find ("QuestGridPanel");
        Proctor = gameObject.transform.parent.name;
        if (hasQuest) {
            //if has a quest, sets text to quest text and set active quest level

            gameObject.GetComponent<SpeechController> ().SpeechText = Text;
            GameObject.Find (Proctor).GetComponentInChildren<Indicator> ().ActiveQuest = 4;
        }
        else{
            GameObject.Find (Proctor).GetComponentInChildren<Indicator> ().ActiveQuest = 0;

        }

    }

    // Update is called once per frame
    public void hideText(){
        //hides text if child at bucket 2 is not closed
         if(gameObject.transform.GetChild (2).gameObject.transform.localScale != closed){
            gameObject.transform.GetChild (0).gameObject.GetComponent<Text>().enabled = false;

        }
        else{
            gameObject.transform.GetChild (0).gameObject.GetComponent<Text>().enabled = true;

        }


    }

    void Update () {
      
       

    }
    public void QuestTrigger () {

        if (hasQuest) {
            //if has quest, sets quest parameters and adds proctor to proctorlist
            bool f = false;
            int n = 0;
            QuestProctors.Add (Proctor);
            hasQuest = false;

            Quest newQuest = new Quest ();

            newQuest.QuestName = Name;
            newQuest.QuestDesc = Desc;
            newQuest.QuestReward = Reward;
            newQuest.QuestText = Text;
            newQuest.QuestTextRepeat = TextRepeat;
            newQuest.QuestCompleteText = CompleteText;
            newQuest.QuestProctor = Proctor;
            newQuest.QuestPayment = Payment;
            newQuest.QuestTalkTo = TalkTo;
            newQuest.QuestTalkToText = TalkToString;
            newQuest.QuestDefeat = Defeat;
            newQuest.QuestDefeatAmount = DefeatAmount;

            newQuest.QuestTalkToProgress = null;
            for (int i = 0; i < newQuest.QuestTalkTo.Count; i++) {
                //creates a bool list with members equal to the questTalkTo list
                BoolListInit.Add (f);

            }
            newQuest.QuestTalkToProgress = BoolListInit;
            
            for (int i = 0; i < newQuest.QuestDefeat.Count; i++) {
                //creates int list with members equal to QuestDefeat list
                IntListInit.Add (n);

            }
            newQuest.QuestDefeatProgress = IntListInit;

            print ("Quest triggered!");
            //adds quest to Quests list
            QuestManager.Quests.Add (newQuest);

            for (int i = 0; i < TalkTo.Count; i++) {
                //sets quest text and active quest to all objects that are part of the TalkTo list
                GameObject.Find (TalkTo[i]).GetComponentInChildren<Indicator> ().ActiveQuest = 1;
                GameObject.Find (TalkTo[i]).GetComponentInChildren<SpeechController> ().SpeechText = TalkToString[i];
                
            }
            if (PlayerStats.Zells <= Payment | Payment == 0) {
                //if player cant afford quest payment amount, have NPC set to repeat and wait for player to get more money
                print ("in active 2");
                GameObject.Find (Proctor).GetComponentInChildren<Indicator> ().ActiveQuest = 2;
                GameObject.Find (Proctor).GetComponentInChildren<SpeechController> ().SpeechText = TextRepeat;
            } else {
                //set NPC to completed quest text and set quest tasks to completed
                print ("in active 3");
                GameObject.Find (Proctor).GetComponentInChildren<Indicator> ().ActiveQuest = 3;
                GameObject.Find (Proctor).GetComponentInChildren<SpeechController> ().SpeechText = CompleteText;
                newQuest.CompletedTasks = true;
                GameObject.Find (Proctor).GetComponentInChildren<SpeechController> ().SpeechText = newQuest.QuestCompleteText;
                //update quest progress text
                newQuest.QuestProgress = "Return to " + Proctor;

            }

            //update UIQuest

            GameObject UIQuest;
            //load a UI quest placeholder and set its elements based on current quest
            UIQuest = Instantiate (Resources.Load ("ExplorationField/ExplorePrefabs/UIPrefabs/QuestPlaceholder")) as GameObject;
            UIQuest.transform.SetParent (QuestGridPanel.transform, false);

            UIQuest.transform.localScale = new Vector3 (1f, 1f, 1);
            UIQuest.GetComponentInChildren<Text> ().text = Name;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIName = newQuest.QuestName;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIDesc = newQuest.QuestDesc;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIReward = newQuest.QuestReward;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UITalkToProgress = newQuest.QuestTalkToProgress;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIProgress = newQuest.QuestProgress;

            UIQuest.GetComponentInChildren<UIQuestScript> ().UITalkTo = newQuest.QuestTalkTo;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIDefeat = newQuest.QuestDefeat;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIDefeatAmount = newQuest.QuestDefeatAmount;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIDefeatProgress = newQuest.QuestDefeatProgress;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIProctor = newQuest.QuestProctor;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIPayment = newQuest.QuestPayment;

            UIQuest.GetComponentInChildren<UIQuestScript> ().UICompleted = false;

        }
    }
}