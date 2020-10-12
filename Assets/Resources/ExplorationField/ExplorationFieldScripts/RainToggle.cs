//this script toggles rain animation on and off
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainToggle : MonoBehaviour
{
    public static bool raining;
    public bool devMode;
    public bool rainToggle;
    public AudioSource rainingSFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (rainToggle & devMode){
            raining = true;
        }
        else if (!rainToggle & devMode){
            raining = false;
        }

        if(raining){
            //plays rain effect over player
            gameObject.GetComponent<ParticleSystem>().Play();
            if(!rainingSFX.isPlaying){
            rainingSFX.Play ();}
        }
        else{
            //stops rain effect
            gameObject.GetComponent<ParticleSystem>().Stop();
            rainingSFX.Stop ();
        }
        
    }
}
