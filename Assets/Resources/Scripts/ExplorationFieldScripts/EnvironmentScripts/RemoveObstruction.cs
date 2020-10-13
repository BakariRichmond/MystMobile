//this script culls objects in the way of the camera
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObstruction : MonoBehaviour
{
    bool entered;
    public GameObject Obstruction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            entered = true;
            //disables object render
            Obstruction.GetComponent<Renderer>().enabled = false;
            

        }
    }
     void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Player") {
            entered = false;
            //enables object render
            Obstruction.GetComponent<Renderer>().enabled = true;
            

        }
    }
}
