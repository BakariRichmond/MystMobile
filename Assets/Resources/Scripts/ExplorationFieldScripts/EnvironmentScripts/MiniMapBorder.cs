//this script scales down gameObject to create a border for their parent object
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapBorder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale= new Vector3(.7f,.7f,.7f);
        
    }
}
