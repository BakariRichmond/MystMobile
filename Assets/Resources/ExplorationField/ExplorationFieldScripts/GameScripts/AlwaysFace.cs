//makes rotation stay at one direction
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sets rotation
        transform.eulerAngles = new Vector3 (0f,0f,0);
        
    }
}
