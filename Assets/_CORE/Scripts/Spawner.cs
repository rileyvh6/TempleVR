using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner spawner;
    [SerializeField] private float RandomRangeX, RandomRangeZ;
    [SerializeField] int amountOfPrefab, amountOfGoals;
    public delegate void FuntionMod<T>(T param1);
    public GameObject[] globalGoal { get; set;}
    int LastRecordedIndex;
    [SerializeField] GameObject prefab;
    public Transform EscapePoint;
    public Transform player;

    private void Start()
    {
        spawner = this;
        globalGoal = new GameObject[amountOfGoals];
        for(int i = 0; i < globalGoal.Length; i++)
        {
            globalGoal[i] = new GameObject("globalGoal_" + i);
        }
        StartCoroutine("ChangePosEveryX", Random.Range(3f, 4f));
        for (int i = 0; i < amountOfPrefab; i++)
        {
            MovePosSetup((x) => {
                if (x.collider.gameObject.tag == "Ground")
                {
                   GameObject obj = Instantiate(prefab, x.point, this.transform.rotation);
                }           
             });
        }
    }

    public void MovePosSetup(FuntionMod<RaycastHit> function)
    {
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(Random.Range(RandomRangeX * -1, RandomRangeX), 100f, Random.Range(RandomRangeZ * -1, RandomRangeZ)), Vector3.down);
        if(Physics.Raycast(ray, out hit))
            function(hit);
    }

    IEnumerator ChangePosEveryX(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            MovePosSetup((x) =>
            {
               int randomIndex;
               do { randomIndex = Random.Range(0, globalGoal.Length);}
               while (globalGoal.Length > 1 && LastRecordedIndex == randomIndex);
                globalGoal[randomIndex].transform.position = x.point;

                LastRecordedIndex = randomIndex;
            });
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(RandomRangeX * 2f, 100f, RandomRangeZ * 2f));
    }

}
