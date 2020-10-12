//this script tracks world objects to the UI element
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackToScreen : MonoBehaviour
{
    public GameObject Player;
    public Camera Cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player){
		Vector3 pos = Cam.WorldToScreenPoint(Player.transform.position);
		transform.position = pos;
		}
        
    }
}
