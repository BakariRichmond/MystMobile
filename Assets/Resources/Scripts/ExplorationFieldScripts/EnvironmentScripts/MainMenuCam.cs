//this script is used to move  the main menu camera from one position/rotation to another
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MainMenuCam : MonoBehaviour {
    public Transform endMarker = null; // create an empty gameobject and assign in inspector
    public Transform endFacing = null;
    public bool init = false;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
    void LateUpdate () {
        if (init) {
            //moves camera to target position/rotation
            transform.position = Vector3.Lerp (transform.position, endMarker.position, Time.deltaTime);
            Quaternion lookOnLook = Quaternion.LookRotation (endFacing.transform.position - transform.position);

            transform.rotation = Quaternion.Slerp (transform.rotation, lookOnLook, Time.deltaTime);
        }

    }
    public void onClick () {
        //changes camera's post processing settings when clicked
        init = true;
        PostProcessVolume volume = GameObject.Find ("PostProcessing").GetComponent<PostProcessVolume> ();
        if (volume != null) {
            DepthOfField DOF;
            volume.profile.TryGetSettings (out DOF);
            if (DOF != null) {
                DOF.focusDistance.value = 2;
                DOF.aperture.value = 5.6f;
                DOF.focalLength.value = 60;

            }
            Vignette vignette;
            volume.profile.TryGetSettings (out vignette);
            if (vignette != null) { vignette.intensity.value = 0.41f; }
        }
       
    }
}