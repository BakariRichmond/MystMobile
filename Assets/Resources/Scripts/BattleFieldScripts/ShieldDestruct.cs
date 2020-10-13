//removes shield object after set time
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDestruct : MonoBehaviour {
	 [SerializeField] private float time = 2;

	// Use this for initialization
	void Start () {
		 Destroy (gameObject, time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
