using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraView : MonoBehaviour {
  public Camera defaultCam;
  public Camera newCam;
  public GameObject rotButton;
  public bool rotateMode;
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
      if (rotateMode) {
        if (rotButton.transform.localScale != open) {
          rotButton.transform.localScale = new Vector3 (1f, 1f, 1);
        } else if (rotButton.transform.localScale == open) {
          GameObject.Find("CameraOrbitController").transform.eulerAngles = new Vector3 (0, 90, 0);
          rotButton.transform.localScale = new Vector3 (0f, 0f, 0);
        }

      } else {
        defaultCam.tag = "Untagged";
        newCam.tag = "MainCamera";
        newCam.enabled = true;
        defaultCam.enabled = false;
      }
    }
  }
  void OnTriggerExit (Collider other) {
    if (other.gameObject.tag == "Player") {
      //defaultCam.tag = "MainCamera";
      // newCam.tag = "Untagged";
    }
  }
}