//this script controlls the main camera
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

public GameObject OverWorldPlayer;        //Public variable to store a reference to the player game object
public GameObject CameraLock; 
public bool init = false;
public bool FollowMode;
public Quaternion  rot;
public Transform Marker;
float speed = 6;
public float rotSpeed = 50;
public bool TestOrbit;
public GameObject OrbitLocation;


    private Vector3 offset;        
	    //Private variable to store the offset distance between the player and camera
		

    // Use this for initialization
    void Start () 
    {
        
        rot = new Quaternion(0.2f, 0.0f, 0.0f, 1.0f);
        
        
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        
        offset = new Vector3 (0.0f, 7.254f, -6.36028f);
         
      
    }

    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
		if (FollowMode){
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.

      if(!TestOrbit) {
        transform.position = OverWorldPlayer.transform.position + offset;
        }
        else{
                   transform.position = OrbitLocation.transform.position;
        }
        
        
		}
    
		
       //lerps rotation for smooth transition
       if(!init){
        
        Quaternion lookOnLook = Quaternion.LookRotation(CameraLock.transform.position - transform.position);
 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, rotSpeed * Time.deltaTime);

        if(transform.rotation == lookOnLook){
        rotSpeed = 50;
        init = true;
        }
        }
    }
}