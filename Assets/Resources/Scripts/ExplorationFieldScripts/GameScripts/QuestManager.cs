//this script controls and manages the quests the player has
using System;
using System.Collections;
using System.Collections.Generic;
using CustomClasses;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {
    static public List<Quest> Quests = new List<Quest> ();
    public GameObject QuestGridPanel;
    // Start is called before the first frame update
    void Start () {
        QuestGridPanel = GameObject.Find ("QuestGridPanel");
        //update quest UI
        QuestDefeatUpdate ();
        QuestAmountUpdate ();

        UIQuestUpdate ();

    }

    // Update is called once per frame
    void Update () {

    }

    public void QuestUpdate (string name) {
        //updates quest details

        for (int i = 0; i < Quests.Count; i++) {
            //checks each quest for completion
            if (Quests[i].Completed == false) {
                for (int j = 0; j < Quests[i].QuestTalkTo.Count; j++) {
                    if (Quests[i].QuestTalkTo[j] == name) {

                        Quests[i].QuestTalkToProgress[j] = true;

                    }
                    print ("Quest at " + i + " = " + Quests[i].QuestTalkToProgress[j]);
                    bool allDone = true;
                    for (int k = 0; k < Quests[i].QuestTalkToProgress.Count; k++) {
                        if (!Quests[i].QuestTalkToProgress[k]) {
                            //quest determined incomplete
                            allDone = false;

                        }
                    }
                    if (allDone) {
                        //if quest complete, seet tasks to complete and change quest text/active quest level
                        Quests[i].CompletedTasks = true;
                        GameObject.Find (Quests[i].QuestProctor).GetComponentInChildren<SpeechController> ().SpeechText = Quests[i].QuestCompleteText;
                        GameObject.Find (Quests[i].QuestProctor).GetComponentInChildren<Indicator> ().ActiveQuest = 3;
                        print ("questupdate function");
                        //update quest progress
                        Quests[i].QuestProgress = "Return to " + Quests[i].QuestProctor;

                    }
                }
            }
        }
    }
    public void QuestDefeatUpdate () {
        //updates quests which require defeating X enemies

        for (int i = 0; i < Quests.Count; i++) {

            bool allDone = false;
            for (int j = 0; j < Quests[i].QuestDefeat.Count; j++) {
                allDone = true;
                for (int k = 0; k < PlayerStats.TempBattleEnemies.Count; k++) {
                    if (Quests[i].QuestDefeat[j] == PlayerStats.TempBattleEnemies[k]) {
                        //increases progress by 1
                        Quests[i].QuestDefeatProgress[j]++;
                        if (Quests[i].QuestDefeatProgress[j] < Quests[i].QuestDefeatAmount[j] | Quests[i].QuestDefeatAmount[j] == 0) {
                            //sets as incomplete
                            allDone = false;
                        }

                    }
                }

            }
            if (allDone) {
                //if quest complete, seet tasks to complete and change quest text/active quest level
                Quests[i].CompletedTasks = true;
                GameObject.Find (Quests[i].QuestProctor).GetComponentInChildren<SpeechController> ().SpeechText = Quests[i].QuestCompleteText;
                GameObject.Find (Quests[i].QuestProctor).GetComponentInChildren<Indicator> ().ActiveQuest = 3;
                print ("questdefeatupdate function");
                //update quest progress
                Quests[i].QuestProgress = "Return to " + Quests[i].QuestProctor;

            } else {
                if (Quests[i].QuestDefeatAmount.Count != 0) {
                    GameObject.Find (Quests[i].QuestProctor).GetComponentInChildren<Indicator> ().ActiveQuest = 2;
                }
            }

        }
        PlayerStats.TempBattleEnemies.Clear ();

    }
    public void QuestAmountUpdate () {
        //quest update for money quests
        for (int i = 0; i < Quests.Count; i++) {
            if (PlayerStats.Zells >= Quests[i].QuestPayment & Quests[i].QuestPayment != 0) {
                //quest set to complete if player has enough money
                Quests[i].CompletedTasks = true;
                GameObject.Find (Quests[i].QuestProctor).GetComponentInChildren<SpeechController> ().SpeechText = Quests[i].QuestCompleteText;
                GameObject.Find (Quests[i].QuestProctor).GetComponentInChildren<Indicator> ().ActiveQuest = 3;
                //update quest progress
                Quests[i].QuestProgress = "Return to " + Quests[i].QuestProctor;

            }
        }

    }

    public void UIQuestUpdate () {
        for (int i = 0; i < QuestGridPanel.transform.childCount; i++) {
            //clears questgrid children
            Destroy (QuestGridPanel.transform.GetChild (i).gameObject);
        }

        for (int i = 0; i < Quests.Count; i++) {
            //for each quest, sets details accordingly in UI menu

            GameObject UIQuest;
            UIQuest = Instantiate (Resources.Load ("ExplorationField/ExplorePrefabs/UIPrefabs/QuestPlaceholder")) as GameObject;
            UIQuest.transform.SetParent (QuestGridPanel.transform, false);

            UIQuest.transform.localScale = new Vector3 (1f, 1f, 1);

            UIQuest.GetComponentInChildren<Text> ().text = Quests[i].QuestName;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIName = Quests[i].QuestName;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIDesc = Quests[i].QuestDesc;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIReward = Quests[i].QuestReward;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UITalkToProgress = Quests[i].QuestTalkToProgress;

            UIQuest.GetComponentInChildren<UIQuestScript> ().UITalkTo = Quests[i].QuestTalkTo;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIDefeat = Quests[i].QuestDefeat;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIProgress = Quests[i].QuestProgress;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIDefeatAmount = Quests[i].QuestDefeatAmount;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIDefeatProgress = Quests[i].QuestDefeatProgress;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIProctor = Quests[i].QuestProctor;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UICompleted = false;
            UIQuest.GetComponentInChildren<UIQuestScript> ().UIPayment = Quests[i].QuestPayment;
        }
        for (int i = 0; i < Quests.Count; i++) {
            if (Quests[i].CompletedTasks) {
                if (Quests[i].Completed == true) {
                    //if task is complete, set UI element to display completed
                    QuestGridPanel.transform.GetChild (i).GetChild (1).gameObject.GetComponentInChildren<Image> ().enabled = true;
                    print (QuestGridPanel.transform.GetChild (i).GetChild (1).gameObject.name + "= object to enable image1");
                    Quests[i].QuestProgress = "Completed";

                } else {
                    //set quest proctor text to complete text
                    GameObject.Find (Quests[i].QuestProctor).GetComponentInChildren<SpeechController> ().SpeechText = Quests[i].QuestCompleteText;
                    GameObject.Find (Quests[i].QuestProctor).GetComponentInChildren<Indicator> ().ActiveQuest = 3;

                    Quests[i].QuestProgress = "Return to " + Quests[i].QuestProctor;
                    if (GameObject.Find (Quests[i].QuestProctor).GetComponentInChildren<SpeechController> ().QuestComplete) {
                        print ("granting reward");
                        //pays amount, and gives quest reward
                        PlayerStats.Zells -= Quests[i].QuestPayment;
                        GameObject Loot;
                        Loot = Instantiate (Resources.Load ("ExplorationField/ExplorePrefabs/Item")) as GameObject;
                        Loot.GetComponent<ItemPickup> ().worldObject = false;
                        Loot.GetComponent<ItemPickup> ().item = true;
                        if (Quests[i].QuestReward[0] != '$') {
                            print (Quests[i].QuestReward[0]);

                            if (Resources.Load ("Projectile Models/Spells/" + Quests[i].QuestReward) != null) {
                                Loot.GetComponent<ItemPickup> ().Spell = (Resources.Load ("BattleField/Projectile Models/Spells/" + Quests[i].QuestReward) as GameObject);
                                Loot.GetComponent<ItemPickup> ().InitLoot ();
                                Loot.GetComponent<ItemPickup> ().itemGet ();
                            } else {
                                print ("No spell found in Spells folder with name " + Quests[i].QuestReward);
                            }

                        } else {

                            print ("this is a cash reward");

                            Loot.GetComponent<ItemPickup> ().Spell = (Resources.Load ("BattleField/Projectile Models/Spells/Fire-ang") as GameObject);
                            string temp = Quests[i].QuestReward;
                            string tempParse = temp.Substring (1);
                            //grants money amount
                            Loot.GetComponent<ItemPickup> ().Amount = Int32.Parse (tempParse);
                            Loot.GetComponent<ItemPickup> ().InitLoot ();
                            Loot.GetComponent<ItemPickup> ().itemGet ();

                        }
                        //sets quest to complete
                        Quests[i].Completed = true;
                        QuestGridPanel.transform.GetChild (i).GetChild (1).gameObject.GetComponentInChildren<Image> ().enabled = true;
                        print (QuestGridPanel.transform.GetChild (i).GetChild (1).gameObject.name + "= object to enable image2");
                        Quests[i].QuestProgress = "Completed";
                        UIQuestUpdate ();

                    }

                }

            } else {
                if (GameObject.Find (Quests[i].QuestProctor) != null) { //sets quest to completion mode 2

                    GameObject.Find (Quests[i].QuestProctor).GetComponentInChildren<Indicator> ().ActiveQuest = 2;
                    GameObject.Find (Quests[i].QuestProctor).GetComponentInChildren<SpeechController> ().SpeechText = Quests[i].QuestTextRepeat;
                }
            }
            for (int j = 0; j < Quests[i].QuestTalkTo.Count; j++) {
                //sets quest to completion mode 1
                if (!Quests[i].QuestTalkToProgress[j] & GameObject.Find (Quests[i].QuestTalkTo[j]) != null) {
                    GameObject.Find (Quests[i].QuestTalkTo[j]).GetComponentInChildren<Indicator> ().ActiveQuest = 1;
                    GameObject.Find (Quests[i].QuestTalkTo[j]).GetComponentInChildren<SpeechController> ().SpeechText = Quests[i].QuestTalkToText[j];

                }

            }
        }

    }
    public void QuestCompleteCheck () {

    }

}