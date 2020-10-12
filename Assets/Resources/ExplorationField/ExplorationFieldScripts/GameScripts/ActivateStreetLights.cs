//controls street lights active
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStreetLights : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {//sets lights intensity based on game time
        if(TimeTrack.GameTime >= 660 & TimeTrack.GameTime <= 1380){
            gameObject.GetComponent<Light>().intensity = 0;
        }
        else{
            gameObject.GetComponent<Light>().intensity = 7;
        }

        
    }
}
