//this script causes a text scrolling animation
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScroll : MonoBehaviour {
	public GameObject ScrollObject;
	Coroutine ScrollRoutine;
	public bool init = true;
	// Use this for initialization
	void Start () {
		
		
		ScrollObject.GetComponent<ScrollRect> ().horizontalNormalizedPosition = .0f;

	}

	// Update is called once per frame
	void Update () {
		if (init) { ScrollRoutine = StartCoroutine (ScrollCoroutine ()); }

	}
	IEnumerator ScrollCoroutine () {
		init = false;
		
		
		ScrollObject.GetComponent<ScrollRect> ().horizontalNormalizedPosition = 0f;
		yield return new WaitForSeconds (3);
		
		//scrolls by .1 until greater than 1
		while (ScrollObject.GetComponent<ScrollRect> ().horizontalNormalizedPosition <= 1) {
			ScrollObject.GetComponent<ScrollRect> ().horizontalNormalizedPosition = ScrollObject.GetComponent<ScrollRect> ().horizontalNormalizedPosition + .1f;
			yield return new WaitForSeconds (.1f);
		}
		
		
		

		yield return new WaitForSeconds (1);
		init = true;

	}
}