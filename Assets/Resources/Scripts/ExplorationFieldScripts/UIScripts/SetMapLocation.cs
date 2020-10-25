using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMapLocation : MonoBehaviour
{
    public string location;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Text>().text = location;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
