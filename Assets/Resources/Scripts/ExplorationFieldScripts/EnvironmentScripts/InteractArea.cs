//this script sets interactable button in the area
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractArea : MonoBehaviour {
    private bool entered;
    private bool enteredShop;
    public GameObject InteractTransportButton;
    public GameObject InteractShopButton;
    public GameObject ShopCanvasBG;
    public GameObject Indicator;
    public Transform SetTarget;
    static public Transform Target;
    public string interactText;
    public bool transportMode = true;
    // Start is called before the first frame update
    void Start () {
        entered = false;
        InteractTransportButton = GameObject.Find ("InteractTransportButton");
        InteractShopButton = GameObject.Find ("InteractShopButton");
        ShopCanvasBG = GameObject.Find ("ShopCanvasBG");

    }

    // Update is called once per frame
    void Update () {
       
    }
    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            entered = true;
            
            if (transportMode) {
                //if transport mode, sets sets target and interact text to areas variables
                Indicator.GetComponent<Renderer> ().enabled = true;
                Target = SetTarget;
                //opens interact button text UI
                InteractTransportButton.GetComponentInChildren<Text> ().text = interactText;

                InteractTransportButton.transform.localScale = new Vector3 (1f, 1f, 1);
            } else {
                 //opens interact button text UI
                Indicator.GetComponent<Renderer> ().enabled = true;
                InteractShopButton.GetComponentInChildren<Text> ().text = interactText;
               
                InteractShopButton.transform.localScale = new Vector3 (1f, 1f, 1);

            }

        }
    }

    void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Player") {
            
            entered = false;

            Indicator.GetComponent<Renderer> ().enabled = false;
            if (transportMode) {
                //close transform button

                InteractTransportButton.transform.localScale = new Vector3 (0f, 0f, 0);
            } else {
                //close shop buttons
                ShopCanvasBG.transform.localScale = new Vector3 (0f, 0f, 0);
                InteractShopButton.transform.localScale = new Vector3 (0f, 0f, 0);
            }
        }

    }

}