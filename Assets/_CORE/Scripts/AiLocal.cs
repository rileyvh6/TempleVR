using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocal : MonoBehaviour
{
    enum AIBehaviour { roam, escape }
    private AIBehaviour aiBehaviour = AIBehaviour.roam;
    public float fleeDistance = 10;
    Renderer rend;
    NavMeshAgent nvAgent;
    int allocatedGoalIndex, lastRecorded;
    int AllocatedGoalIndex
    {
        get { return allocatedGoalIndex; }
        set 
        {
            allocatedGoalIndex = value;
        }
    }

    private void Start()
    {
        rend = GetComponent<Renderer>();
        nvAgent = GetComponent<NavMeshAgent>();
        AllocatedGoalIndex = Random.Range(0, Spawner.spawner.globalGoal.Length);
    }

    private void Update()
    {
        var distanceFromPlayer = Vector3.Distance(transform.position, Spawner.spawner.player.transform.position);
        if (distanceFromPlayer < fleeDistance) aiBehaviour = AIBehaviour.escape;

        switch (aiBehaviour)
        {
            case AIBehaviour.roam:
                Roam();
                break;

            case AIBehaviour.escape:
                Escape();
                break;
        }
    }

    private void Roam()
    {
        nvAgent.SetDestination(Spawner.spawner.globalGoal[AllocatedGoalIndex].transform.position);
        if (nvAgent.remainingDistance <= 1f)
            Spawner.spawner.MovePosSetup((x) => { Spawner.spawner.globalGoal[AllocatedGoalIndex].transform.position = x.point; });
        if (Random.Range(0, 10000) < 10)
        {
            lastRecorded = AllocatedGoalIndex;
            do { AllocatedGoalIndex = Random.Range(0, Spawner.spawner.globalGoal.Length); }
            while (AllocatedGoalIndex == lastRecorded);
        }

    }

    private void Escape()
    {
        nvAgent.SetDestination(Spawner.spawner.EscapePoint.position);

        if (nvAgent.remainingDistance < 1) aiBehaviour = AIBehaviour.roam;
    }

}



