//sets mobile control cursor position
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControl : MonoBehaviour {
	public GameObject Player;
	
	
	GameObject mCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Player){
			//sets cursor position to players world position
		Vector3 pos = Camera.main.WorldToScreenPoint(Player.transform.position);
		transform.position = pos;
		}
		
	}
}
