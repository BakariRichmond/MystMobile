//This script sets areas with different zoom levels/culling for the minimap
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetArea : MonoBehaviour {
    public GameObject Sun;
    public float zoom = 50f;
    public string area;
    public GameObject[] MiniMapMembers;
    public Camera camera1;
    public static string currentArea = "Default";
    public static string lastArea;
    private bool init = false;
    private float ratio;
    private float scale;
    private float defaultZoom;
    // Start is called before the first frame update
    void Start () {
        Sun=GameObject.Find("Sun");
        camera1 = GameObject.Find ("Camera1").GetComponent<Camera> ();
        defaultZoom = camera1.orthographicSize;
        //sets scale to scale down minimap elements by the ratio the MiniMap camera is zoomed in
        ratio = zoom/50;
        scale = 8.5f*ratio;
        
    }

    // Update is called once per frame
    void Update () {

    }
    void OnTriggerStay (Collider other) {

        if (other.gameObject.tag == "Player") {
            if (!init) {
                init = true;
               
                //sets last and current area when trigger is entered
                SetArea.lastArea = SetArea.currentArea;
                SetArea.currentArea = area;
                GameObject.Find("MapMenuLocation2Text").GetComponent<Text>().text = area;
                //zooms in minimap
                camera1.orthographicSize = zoom;
                //for all minimap tagged objects, localscale is set to scale variable
                    MiniMapMembers = GameObject.FindGameObjectsWithTag ("MiniMap");

                foreach (GameObject MiniMapItem in MiniMapMembers) {
                    
                    MiniMapItem.transform.localScale= new Vector3(scale,scale,scale);
                }
                //culls default layer and the last area from the minimap, and adds the current area
                
                
                camera1.cullingMask &= ~(1 << LayerMask.NameToLayer ("Default"));
                camera1.cullingMask |= (1 << LayerMask.NameToLayer (SetArea.currentArea));
                camera1.cullingMask &= ~(1 << LayerMask.NameToLayer (SetArea.lastArea));
                //remove default layer from sun
                Sun.GetComponent<Light>().cullingMask &= ~(1 << LayerMask.NameToLayer ("Water"));

            }
        }
    }

    void OnTriggerExit (Collider other) {

        if (other.gameObject.tag == "Player") {
            init = false;
            //upon exiting area, camera zoom is set back to initial level
            camera1.orthographicSize = defaultZoom;
           
                //all minimap tagged objects scaled back to default size
                    MiniMapMembers = GameObject.FindGameObjectsWithTag ("MiniMap");

                foreach (GameObject MiniMapItem in MiniMapMembers) {
                    
                    MiniMapItem.transform.localScale= new Vector3(8.47682f,8.47682f,8.47682f);
                }
            
            SetArea.lastArea = SetArea.currentArea;
            //add default layer to sun
            Sun.GetComponent<Light>().cullingMask |= (1 << LayerMask.NameToLayer ("Water"));
            SetArea.currentArea = "Default";
            GameObject.Find("MapMenuLocation2Text").GetComponent<Text>().text = "";
            //adds deafult layer to minimap camera view
            camera1.cullingMask |= (1 << LayerMask.NameToLayer ("Default"));

        }

    }

}