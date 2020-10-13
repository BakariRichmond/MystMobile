//this script controls the vision of an enemy
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {
	public GameObject Player;
	public GameObject Enemy;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find("OverWorldPlayer");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	  void OnTriggerEnter(Collider other)
    {
        

		
        if (other.gameObject.tag == "Player")
        { 
			//if player enters vision collider, initiates enemy script value
			OverWorldEnemyScript ES = Enemy.GetComponentInParent<OverWorldEnemyScript>();
			print("spotted!");
			ES.init = true;

			
		}
    }
	 void OnTriggerExit(Collider other)
    {
       
        if (other.gameObject.tag == "Player")
        { 
			//if player exits vision collider, disables enemy script value
			OverWorldEnemyScript ES = Enemy.GetComponentInParent<OverWorldEnemyScript>();
			ES.init = false;
			//ES.Wander = true;
			
		}
    }
}
