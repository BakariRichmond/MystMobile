using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour {
    // Scroll main texture based on time

    public float scrollSpeed = 0.01f;
    Renderer rend;
    public Material mat;
    float offset;
    public float startingOffset;

    public float resetOffset;
    public bool reverseScrollVertical;
    public bool ScrollHorizontal;
    public float defaultOffset;

    void Start () {
        //rend = GetComponent<Renderer> ();
        //mat = gameObject.GetComponent<Material> ();
        //ScrollCoroutine = StartCoroutine (ScrollCoroutine ());
        // mat.SetTextureOffset("_MainTex", new Vector2(0, startingOffset));

    }

    void Update () {
        // = Time.time * scrollSpeed;
        if (reverseScrollVertical) { 

            offset += scrollSpeed;

            if (offset >= startingOffset) { //resetOffset){
                offset = resetOffset; //startingOffset;

            }

            


        } else {
            offset -= scrollSpeed;

            if (offset <= resetOffset) { //resetOffset){
                offset = startingOffset; //startingOffset;

            }

            
        }
        if(ScrollHorizontal){
            mat.SetTextureOffset ("_MainTex", new Vector2 (offset, defaultOffset));
        }
        else{
            mat.SetTextureOffset ("_MainTex", new Vector2 (defaultOffset, offset));
        }

       

    }

}