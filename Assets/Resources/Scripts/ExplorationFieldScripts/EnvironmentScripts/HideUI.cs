//this script moves a UI element to a position off screen and back
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour {
    Vector3 pos;
    bool closeElement = false;
    public float speed = 1f;
    public GameObject Button;
    private Vector3 start1;
    private Vector3 des1;
    private Vector3 start2;
    private Vector3 des2;
    private float fraction = 0;

    // Start is called before the first frame update
    void Start () {
        
        Button=GameObject.Find("MiniMapArrow");
        pos = gameObject.transform.position;
        
        //sets psitions to move from
        start1 = new Vector3 (6, transform.position.y, transform.position.z);
        des1 = new Vector3 (-100, transform.position.y, transform.position.z);
        start2 = new Vector3 (-100, transform.position.y, transform.position.z);
        des2 = new Vector3 (6, transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void Update () {

        pos = gameObject.transform.position;
        //if element is done moving and button press, toggles close element
        if (Input.GetKeyDown ("m") & fraction >= 1) {
            if (closeElement) {
                closeElement = false;
                fraction = 0;

            } else {
                closeElement = true;
                fraction = 0;

            }

        }
        if (closeElement) {
            //if close element, moves to position 1
            Button.transform.eulerAngles = new Vector3(0,0,180);
           

            if (fraction < 1) {
                fraction += Time.deltaTime * speed;
                transform.position = Vector3.Lerp (start1, des1, fraction);
            }

        
        } else if (!closeElement) {
             //if close is false element, moves to position 2
            Button.transform.eulerAngles = new Vector3(0,0,0);

            if (fraction < 1) {
                fraction += Time.deltaTime * speed;
                transform.position = Vector3.Lerp (start2, des2, fraction);

         
            }
        }
      

    }
    public void OnClick () {
        print ("clicked");
        //if element not moving, toggles closeElement
        if (fraction >= 1) {
            if (closeElement) {
            
                closeElement = false;

                fraction = 0;

            } else {
                closeElement = true;
                fraction = 0;

            }
        }

    }
}