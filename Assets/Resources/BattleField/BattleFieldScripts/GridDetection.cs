//this script keeps track of players location and enemies locations
using UnityEngine;

using System.Collections.Generic;
public class GridDetection : MonoBehaviour
{
    public GameObject Field;

    public bool PlayerHere;
    public bool EnemyHere;
    public GameObject EnemyTempModel;



    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {


    }
    //if an object enters, the tile color is set, and playerlocation or enemylocation respectively is given the name added to the list
    void OnTriggerEnter(Collider other)
    {
        var tileRenderer = gameObject.GetComponent<Renderer>();
        FieldScript fieldscript = Field.GetComponent<FieldScript>();


        if (other.gameObject.tag == "Player")
        {
            //if player is on tile, set color to cyan and set location in playerLocation
            PlayerHere = true;
            fieldscript.PlayerLocation = gameObject.name;
            tileRenderer.material.SetColor("_Color", Color.cyan);
            print("player is on space " + gameObject.name);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            //if enemy is on tile, set its color to red and add it to enemyLocation
            EnemyHere = true;

            fieldscript.EnemyLocation.Add(gameObject.name);

            tileRenderer.material.SetColor("_Color", Color.red);
            print("enemy " + other.gameObject.name + " is on space " + gameObject.name);
        }


    }
    //does the opposite of enter, reverts color and removes name
    void OnTriggerExit(Collider other)
    {
        var tileRenderer = gameObject.GetComponent<Renderer>();
        FieldScript fieldscript = Field.GetComponent<FieldScript>();
        if (other.gameObject.tag == "Player")
        {
            PlayerHere = false;
            Color Tcolor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            tileRenderer.material.SetColor("_Color", Tcolor);
            
        }
        else if (other.gameObject.tag == "Enemy")
        {
            EnemyHere = false;
            fieldscript.EnemyLocation.Remove(gameObject.name);
             Color Tcolor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            tileRenderer.material.SetColor("_Color", Tcolor);
        }
    }
}
