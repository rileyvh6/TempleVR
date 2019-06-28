using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float spawnRate = 5;
    public GameObject[] SpawnObjects;
    public Transform[] spawnPoints;
    public Transform[] wayPoints;
    float timer = 0;
    public float timeSpawnPointChangeLocation = 2f;
    private int wayPointIndex = 0;



    public void Awake()
    {
        InvokeRepeating("Spawn", 2, spawnRate);

    }

    public void Spawn()
    {
        Vector3 spawnpos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        Instantiate(SpawnObjects[Random.Range(0, SpawnObjects.Length)], spawnpos, Quaternion.identity);
    }

    public void Update()
    {
        

      if (Time.time>=timer)
        {
            //GetNextWaypoint();
            spawnPoints[0].transform.position = GetNextWaypoint();
            timer = Time.time + Random.Range(timeSpawnPointChangeLocation, timeSpawnPointChangeLocation + 2f);
        }
    }

    public Vector3 GetNextWaypoint()
    {
      
        if (wayPointIndex < wayPoints.Length -1)
        {
            wayPointIndex++;
        }
        else
        {
            wayPointIndex = 0;
        }

        return wayPoints[wayPointIndex].position;
    }
}
