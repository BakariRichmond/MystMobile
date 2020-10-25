//this script causes a pushable object to be pushed by the player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Push : MonoBehaviour {
    bool inCollider;
    public GameObject MobileJoystick;
    Animator Anim;
    GameObject InteractButton;
    public GameObject Pointer;
    Coroutine PushRoutine;
    public string InteractName;
    public float speed = 1;

    public GameObject targetPos;
    public GameObject OverWorldPlayer;
    public bool moving;
    public GameObject CrateEdge;
    public static Vector3 BoxPosition = new Vector3 (-1.92f, -1.13733f, 0.3370609f);
    public static bool Moved;
    public GameObject GuardObject;
    public GameObject InteractHandler;
    

    // Start is called before the first frame update
    void Start () {
        
        if (Moved) {
            //if the object was already moved, disables collider
            gameObject.GetComponent<BoxCollider> ().enabled = false;

        }
        gameObject.transform.localPosition = BoxPosition;
        InteractHandler = GameObject.Find ("InteractButtonHandler");
        OverWorldPlayer = GameObject.Find ("OverWorldPlayer");
        Anim = OverWorldPlayer.GetComponent<Animator> ();
        InteractButton = GameObject.Find ("InteractPushButton");
        MobileJoystick = GameObject.Find ("MobileJoystick");

    }

    // Update is called once per frame
    void Update () {
        if (inCollider) {
            //checks if there is an object in the way
            if (GuardObject == null) {
            if (gameObject.GetComponent<NotificationTrigger> () != null) {
                gameObject.GetComponent<NotificationTrigger> ().Active = false;

            }
        }

          

        }
        if (moving) {
            if (GuardObject == null) {

               //if moving is set to true, player rotates and begins pushing object

                
                OverWorldPlayer.transform.eulerAngles = new Vector3 (0, 90, 0);
                Anim.SetBool ("isPushing", true);
                gameObject.GetComponent<BoxCollider> ().enabled = false;
                gameObject.GetComponent<MeshCollider> ().enabled = false;
                InteractButton.transform.localScale = new Vector3 (0f, 0f, 0);
                Pointer.GetComponent<MeshRenderer> ().enabled = false;
                MobileJoystick.GetComponent<Image> ().enabled = false;
                float step = speed * Time.deltaTime; // calculate distance to move
                
                OverWorldPlayer.transform.position = CrateEdge.transform.position;
                transform.position = Vector3.MoveTowards (transform.position, targetPos.transform.position, step);
            }
        }
        if (Vector3.Distance (transform.position, targetPos.transform.position) == 0 & !Moved) {
            //when object is at destination, sets moved to true and stops pushing animation
            Moved = true;
            gameObject.GetComponent<MeshCollider> ().enabled = true;
            moving = false;
            Anim.SetBool ("isPushing", false);
            MobileJoystick.GetComponent<Image> ().enabled = true;
            BoxPosition = gameObject.transform.localPosition;
            InteractHandler.GetComponent<InteractHandler> ().ObjectBool = false;
        }

    }
    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            //opens "push" option in UI 
            InteractButton.transform.localScale = new Vector3 (1f, 1f, 1);
            InteractButton.GetComponentInChildren<Text> ().text = InteractName;
            Pointer.GetComponent<MeshRenderer> ().enabled = true;
            GameObject.Find ("InteractButtonHandler").GetComponent<InteractHandler> ().InteractObject = this.gameObject;
            inCollider = true;

        }
    }
    void OnTriggerExit (Collider other) {
        //closes "push" option in UI 

        if (other.gameObject.tag == "Player") {
            InteractButton.transform.localScale = new Vector3 (0f, 0f, 0);
            Pointer.GetComponent<MeshRenderer> ().enabled = false;
            InteractHandler.GetComponent<InteractHandler> ().ObjectBool = false;
            inCollider = false;

        }
    }

}