using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBalls : MonoBehaviour
{
    public float spawnRate = 5;
    public GameObject[] SpawnObjects;
    public Transform[] spawnPoints;
    public Transform[] wayPoints;
    float timer = 0;
    public float timeSpawnPointChangeLocation = 2f;
    private int wayPointIndex = 0;
    private int spawnBalls = 0;
    private int maxNumberBalls = 50;
    private int forceMultiplier = 2;



    public void Start()
    {
        StartCoroutine(Spawn());
       
        //  InvokeRepeating("Spawn", 2, spawnRate);

    }

   public IEnumerator Spawn()
    {
        while ( spawnBalls < maxNumberBalls)
        {
            Vector3 spawnpos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            GameObject obj = Instantiate(SpawnObjects[Random.Range(0, SpawnObjects.Length)], spawnpos, Quaternion.identity);
            Vector3 force = new Vector3(Random.value, Random.value, Random.value) * forceMultiplier;
            obj.GetComponent<Rigidbody>().AddForce(force);
            yield return new WaitForSeconds(spawnRate);


        }
        
    }
    /* public void Spawn()
    {
        Vector3 spawnpos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        Instantiate(SpawnObjects[Random.Range(0, SpawnObjects.Length)], spawnpos, Quaternion.identity);

        if ()
    }*/

    public void Update()
    {


        if (Time.time >= timer)
        {
            //GetNextWaypoint();
            spawnPoints[0].transform.position = GetNextWaypoint();
            timer = Time.time + Random.Range(timeSpawnPointChangeLocation, timeSpawnPointChangeLocation + 2f);
        }
    }

    public Vector3 GetNextWaypoint()
    {

        if (wayPointIndex < wayPoints.Length - 1)
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
