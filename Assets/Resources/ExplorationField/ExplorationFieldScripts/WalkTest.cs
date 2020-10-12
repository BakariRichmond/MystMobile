//this script is a walking mechanic test script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTest : MonoBehaviour {
	public Animator anim;
	

	//public float Speed = 3.0F;
    // public float RotateSpeed = 3.0F;

	   public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
	float myAngle;

    private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
   
         CharacterController controller = GetComponent<CharacterController>();
         if (transform != null)
         {
			 //sets movement angles to input axis
			
			 float temp = myAngle;
			 myAngle = Mathf.Atan2 (Input.GetAxis ("Horizontal"),Input.GetAxis ("Vertical")) * Mathf.Rad2Deg;
			
			
			if(myAngle != 0){
			this.transform.eulerAngles = new Vector3 (0, myAngle, 0);
			}
			 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;
     
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);



		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");
		
		if(Input.GetAxis("Vertical") == 0 & Input.GetAxis("Horizontal") == 0){

		}
		else{
			print("|Vertical: "+ vertical + ", Horizontal: " + horizontal + "|");
			print(myAngle);
		}

		
		

         }
	}
     
}
