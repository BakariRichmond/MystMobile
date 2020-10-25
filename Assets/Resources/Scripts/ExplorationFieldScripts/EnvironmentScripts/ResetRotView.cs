using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotView : MonoBehaviour {
  
  public GameObject rotButton;
  
  Vector3 open;
  // Start is called before the first frame update
  void Start () {
    open = new Vector3 (1f, 1f, 1);

  }

  // Update is called once per frame
  void Update () {

  }
  void OnTriggerEnter (Collider other) {
    if (other.gameObject.tag == "Player") {
      
        
          GameObject.Find("CameraOrbitController").transform.eulerAngles = new Vector3 (0, 90, 0);
          rotButton.transform.localScale = new Vector3 (0f, 0f, 0);
        

    }
  }
  void OnTriggerExit (Collider other) {
    if (other.gameObject.tag == "Player") {
     
    }
  }
}