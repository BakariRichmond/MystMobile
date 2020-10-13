//culls certain objects in range from camera view
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCulling : MonoBehaviour {
    public Renderer[] Renderers;

    // Start is called before the first frame update
    void Start () {

        TerrainData terrain = Terrain.activeTerrain.terrainData;

    }

    // Update is called once per frame
    void Update () {

    }
    void OnTriggerEnter (Collider other) {
        //disable renders of objects in trigger
        Renderers = other.gameObject.GetComponentsInChildren<Renderer> ();
        foreach (Renderer ren in Renderers)
            ren.enabled = false;

    }
    void OnTriggerExit (Collider other) {
        //reenable renders
        Renderers = other.gameObject.GetComponentsInChildren<Renderer> ();
        foreach (Renderer ren in Renderers)
            ren.enabled = true;

    }
}