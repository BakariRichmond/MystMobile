//sets the transform of an object
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransform : MonoBehaviour
{
    public Vector3 location;
    GameObject QuestCenter;
    // Start is called before the first frame update
    void Start()
    {
        QuestCenter = GameObject.Find("QuestCenter");
        location = QuestCenter.transform.position;
       

        
    }

    // Update is called once per frame
    void Update()
    {
        //sets object position to location variable
        gameObject.transform.position = location;
        
    }
}
