 using System.Collections;
 using UnityEngine.UI;
 using UnityEngine;
 using System;

 public class OrbitController : MonoBehaviour {

     
     private bool isRotating;
     public GameObject Button;
     private Vector3 initialPos;
     private float distance;
     public float newRotation;
     public float rotUpdate;
     public float rotSpeed = 3;
      private Vector3 currentAngle;

     private bool init;

     void Start () {
        newRotation = 90*15;
         currentAngle = transform.eulerAngles;
     }

     void Update () {
         if (Button.GetComponent<ButtonDetector> ().buttonPressed == true) {

             
             isRotating = true;
             if (!init) {
                 init = true;
                 initialPos = Input.mousePosition;
             }

            

             // store mouse
            
         } else if (Button.GetComponent<ButtonDetector> ().buttonPressed == false) {
             print ("mouseup");
             init = false;
             newRotation = rotUpdate;
             initialPos.x = 0;
             isRotating = false;
         }
         if (isRotating) {
             
                          

             distance = Math.Abs(initialPos.x - Input.mousePosition.x);
             print(distance + "-distance");
             if (initialPos.x > Input.mousePosition.x) {
                 
                 //newRotation -= distance;
                 rotUpdate = newRotation - distance;
             } else if (initialPos.x < Input.mousePosition.x) {
                  
                 // newRotation+=distance;
                  rotUpdate = newRotation + distance;

             }
            if(newRotation > 15*360){
                //newRotation -= 15*360;
                rotUpdate = newRotation - (15*360);
            }
            else if(newRotation < 0){
                //newRotation += 15*360;
                rotUpdate = newRotation + (15*360);
            }
            

             this.transform.eulerAngles = new Vector3 (0, rotUpdate/15, 0);
    

         }
     }

 }