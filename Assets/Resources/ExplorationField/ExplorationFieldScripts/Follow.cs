//this script causes gameobject to follow target with an offset on an axis
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Player;
    private Vector3 offset;
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("OverWorldPlayer");
        if(offsetY == 0){
            offsetY= -1;
        }
         offset = new Vector3 (offsetX, offsetY, offsetZ);
    }

    // Update is called once per frame
    void Update()
    {
        //setts gameobjects position to player position with offset
        gameObject.transform.position = Player.transform.position + offset;
        
    }
}
