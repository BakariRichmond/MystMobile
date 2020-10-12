//this script spawns enemies for battle areas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	
    [SerializeField] public string Enemy1Name;
    [SerializeField] public int Enemy1Rate;
    [SerializeField] public string Enemy2Name;
    [SerializeField] public int Enemy2Rate;
    [SerializeField] public string Enemy3Name;
    [SerializeField] public int Enemy3Rate;
    [SerializeField] public string Enemy4Name;
    [SerializeField] public int Enemy4Rate;
    [SerializeField] public string Enemy5Name;
    [SerializeField] public int Enemy5Rate;
    [SerializeField] public string Enemy6Name;
    [SerializeField] public int Enemy6Rate;
    [SerializeField] public string Enemy7Name;
    [SerializeField] public int Enemy7Rate;
    [SerializeField] public string Enemy8Name;
    [SerializeField] public int Enemy8Rate;
	GameObject Fence;

	// Use this for initialization
	void Start () {
		  int rndm = (int)Mathf.Round(Random.Range(0, 100));
            //for each enemy, if random number is less than spawn rate, spawn enemy and set random spawn position in range
            if (rndm <= Enemy1Rate)
            {
				 GameObject EnemyClone = Instantiate(Resources.Load("ExplorationField/ExplorationFieldModels/Enemies/" + Enemy1Name)) as GameObject;
				 Vector3 pos = transform.position;
				 
				 pos.x += (int)Mathf.Round(Random.Range(-10, 10));
				
				 pos.z += (int)Mathf.Round(Random.Range(-10, 10));
				 EnemyClone.transform.position = pos;
			}
			  rndm = (int)Mathf.Round(Random.Range(0, 100));
            
            if (rndm <= Enemy2Rate)
            {
				 GameObject EnemyClone = Instantiate(Resources.Load("ExplorationField/ExplorationFieldModels/Enemies/" + Enemy2Name)) as GameObject;
				Vector3 pos = transform.position;
				 pos.x += (int)Mathf.Round(Random.Range(-10, 10));
				 pos.z += (int)Mathf.Round(Random.Range(-10, 10));
				 EnemyClone.transform.position = pos;

			}
			rndm = (int)Mathf.Round(Random.Range(0, 100));
			 if (rndm <= Enemy3Rate)
            {
				 GameObject EnemyClone = Instantiate(Resources.Load("ExplorationField/ExplorationFieldModels/Enemies/" + Enemy3Name)) as GameObject;
				Vector3 pos = transform.position;
				 pos.x += (int)Mathf.Round(Random.Range(-10, 10));
				 pos.z += (int)Mathf.Round(Random.Range(-10, 10));
				 EnemyClone.transform.position = pos;

			}
			rndm = (int)Mathf.Round(Random.Range(0, 100));
			 if (rndm <= Enemy4Rate)
            {
				 GameObject EnemyClone = Instantiate(Resources.Load("ExplorationField/ExplorationFieldModels/Enemies/" + Enemy4Name)) as GameObject;
				Vector3 pos = transform.position;
				 pos.x += (int)Mathf.Round(Random.Range(-10, 10));
				 pos.z += (int)Mathf.Round(Random.Range(-10, 10));
				 EnemyClone.transform.position = pos;

			}
			rndm = (int)Mathf.Round(Random.Range(0, 100));
			 if (rndm <= Enemy5Rate)
            {
				 GameObject EnemyClone = Instantiate(Resources.Load("ExplorationField/ExplorationFieldModels/Enemies/" + Enemy5Name)) as GameObject;
				Vector3 pos = transform.position;
				 pos.x += (int)Mathf.Round(Random.Range(-10, 10));
				 pos.z += (int)Mathf.Round(Random.Range(-10, 10));
				 EnemyClone.transform.position = pos;

			}
			rndm = (int)Mathf.Round(Random.Range(0, 100));
			 if (rndm <= Enemy6Rate)
            {
				 GameObject EnemyClone = Instantiate(Resources.Load("ExplorationField/ExplorationFieldModels/Enemies/" + Enemy6Name)) as GameObject;
				Vector3 pos = transform.position;
				 pos.x += (int)Mathf.Round(Random.Range(-10, 10));
				 pos.z += (int)Mathf.Round(Random.Range(-10, 10));
				 EnemyClone.transform.position = pos;

			}
			rndm = (int)Mathf.Round(Random.Range(0, 100));
			 if (rndm <= Enemy7Rate)
            {
				 GameObject EnemyClone = Instantiate(Resources.Load("ExplorationField/ExplorationFieldModels/Enemies/" + Enemy7Name)) as GameObject;
				Vector3 pos = transform.position;
				 pos.x += (int)Mathf.Round(Random.Range(-10, 10));
				 pos.z += (int)Mathf.Round(Random.Range(-10, 10));
				 EnemyClone.transform.position = pos;

			}
			rndm = (int)Mathf.Round(Random.Range(0, 100));
			 if (rndm <= Enemy8Rate)
            {
				 GameObject EnemyClone = Instantiate(Resources.Load("ExplorationField/ExplorationFieldModels/Enemies/" + Enemy8Name)) as GameObject;
				Vector3 pos = transform.position;
				 pos.x += (int)Mathf.Round(Random.Range(-10, 10));
				 pos.z += (int)Mathf.Round(Random.Range(-10, 10));
				 EnemyClone.transform.position = pos;

			}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
