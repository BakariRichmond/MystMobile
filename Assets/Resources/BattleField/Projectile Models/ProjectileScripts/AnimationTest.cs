using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour {
	private Animation anim;
	public string Animation1;
	public string Animation2;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animation>();
		anim.Play(Animation1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
