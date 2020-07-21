//using Boo.Lang;
using UnityEngine;
using System.Collections.Generic;
using System;

public class SpawnEnemy : MonoBehaviour
{
    /// <summary>
    /// Enemy prefab to instantiate
    /// </summary>
    public GameObject Enemy;

    /// <summary>
    /// Gameobject with SpawnPositions in every environment
    /// </summary>
    public GameObject SpawnPositions;

    /// <summary>
    /// No of Enemies to be instantiated
    /// </summary>
    public int NoofEnemies = 5;

    /// <summary>
    /// Variable with properties of particular enemy
    /// </summary>
    public List<SpawnInstruction> spawnInstructions;

    /// <summary>
    /// List of given Walkable Enemies
    /// </summary>
    public String[] WalkableEn;

    /// <summary>
    /// Current environmnent for navigation
    /// </summary>
    public GameObject Environment;

    private void Awake()
    {
        SpawnPositions = GameObject.FindGameObjectWithTag("SpawnPos");    
    }

    private void Start()
    {
        Spawn();
        EventManager.AddReloadWeapontListener(Reset);
        EventManager.AddShootListener(Hide);
    }

    public void Spawn()
    {
        if (NoofEnemies == 0)
            Debug.LogError("Provide No of Enemies");

        for (int i = 0; i < NoofEnemies; i++)
        {
            GameObject Clone = Instantiate(Enemy, SpawnPositions.transform.GetChild(i).position,Quaternion.identity,this.transform);
            Clone.name = spawnInstructions[i].Name;
            //Adding Burger script with its value
            Clone.AddComponent<Burgler>().m_value = i+1;

            //Assigning waypoint for movement
            int length = spawnInstructions[i].Targetpoints.Count;

            for (int j = 0; j < length; j++)
            {
                int trans = spawnInstructions[i].Targetpoints[j];
                Clone.GetComponent<CustomAgent>().GoalPoints[j] = SpawnPositions.transform.GetChild(trans).transform;
            }

            // Giving boolean true value which characters are walkable
            for (int k = 0; k < WalkableEn.Length; k++)
            {
                if (WalkableEn[k] == Clone.name)
                Clone.GetComponent<CustomAgent>().isWalkable = true;
            }
        }        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Pressed 1");
            Reset();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Pressed 2");
            Hide(1);
        }
    }

    public void Hide(int integer)
    {
        //EnParent.gameObject.SetActive(false);
        //Envi.SetActive(false);
    }

    public void Reset()
    {
        //EnParent.gameObject.SetActive(true);
        //GameObject[] EnObjectArray = GameObject.FindGameObjectsWithTag("Burgler");

        //for (int i = 0; i < EnObjectArray.Length; i++)
        //{
        //    EnObjectArray[i].transform.position = EneSpawnPos[i].transform.position;
        //}
        //Envi.SetActive(true);
    }
}