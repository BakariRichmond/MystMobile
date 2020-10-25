//this script initializes shop items and handles their prices/if buyable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using CustomClasses;
public class ShopItem : MonoBehaviour {
    public GameObject ShopCanvasConfirm;
    public GameObject spell;
    public string name;
    public int price;
    public GameObject nameObj;
    public GameObject priceObj;
    // Start is called before the first frame update
    void Start () {
       ShopCanvasConfirm = GameObject.Find ("ShopCanvasConfirm");
      // ShopCanvasConfirm.transform.localScale = new Vector3 (1f, 1f, 1);

        //sets name and price text of item in UI
        /*
        gameObject.transform.GetChild (0).gameObject.GetComponent<Text> ().text = name;
        gameObject.transform.GetChild (2).gameObject.GetComponent<Text> ().text = price.ToString ();
        */
          nameObj.GetComponent<Text> ().text = name;
        priceObj.GetComponent<Text> ().text = price.ToString ();

    }

    // Update is called once per frame
    void Update () {

    }
    public void onClickItem () {
        //when clicking an item in the shop, sets confirmation text to the items details and pricing

        GameObject.Find ("RecapName").GetComponent<Text> ().text = name;
        GameObject.Find ("RecapImage").GetComponent<GameObjectScript>().RecapSpell = spell;
        GameObject.Find ("RecapAmount").GetComponent<Text> ().text = price.ToString ();

        ShopCanvasConfirm.transform.localScale = new Vector3 (1f, 1f, 1);
        //changes the items color based on if its affordable to the player
        if (PlayerStats.Zells >= price) {

            GameObject.Find ("ShopCanvasConfirmBG").transform.GetChild (2).gameObject.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);

        } else {
            GameObject.Find ("ShopCanvasConfirmBG").transform.GetChild (2).gameObject.GetComponent<Image> ().color = new Color32 (147, 147, 147, 255);

        }

    }

    public void onClickConfirm () {
        //when confirming purchase, checks if player has enough money
        if (PlayerStats.Zells >= Int32.Parse(GameObject.Find ("RecapAmount").GetComponent<Text> ().text)) {

            //add spell to inventory

            SpellRef NewSpell = new SpellRef ();
            NewSpell.name = GameObject.Find ("RecapImage").GetComponent<GameObjectScript>().RecapSpell.name;
            NewSpell.equipped = false;

            GameObject GridPanel = GameObject.Find ("GridPanel");
            GameObject InvSpellClone = Instantiate (Resources.Load ("BattleField/Projectile Models/Spells/" + NewSpell.name)) as GameObject;

            PlayerStats.SpellRefs.Add (NewSpell);
            InvSpellClone.transform.SetParent (GridPanel.transform, false);

            InvSpellClone.transform.localScale = new Vector3 (1f, 1f, 1);
            InvSpellClone.GetComponentInChildren<Text> ().text = InvSpellClone.GetComponent<InvSlot> ().SpellName;

            //subtract price
            PlayerStats.Zells -= Int32.Parse(GameObject.Find ("RecapAmount").GetComponent<Text> ().text);
            ShopCanvasConfirm.transform.localScale = new Vector3 (0f, 0f, 0);
        } else {

        }

    }
    public void onClickCancel () {
        ShopCanvasConfirm.transform.localScale = new Vector3 (0f, 0f, 0);

    }
}