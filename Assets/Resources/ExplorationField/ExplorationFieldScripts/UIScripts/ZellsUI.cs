//this script updates money in UI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZellsUI : MonoBehaviour
{
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //sets zells text box to current zell amount
        gameObject.GetComponent<Text>().text = PlayerStats.Zells.ToString();

        
    }
}
