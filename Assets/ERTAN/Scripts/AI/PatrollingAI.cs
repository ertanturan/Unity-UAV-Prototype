using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PatrollingAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private float radius = 2.0f;
    public float waitForSeconds = 2.0F;
    bool IsTargetChoosen;

    

    PatrollingAI_Path path;

    // Start is called before the first frame update
    void Start()
    {
        path = PatrollingAI_Path.Instance;
        agent = GetComponent<NavMeshAgent>();
        radius += agent.radius;
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - target.position).sqrMagnitude;
      
;        if (distance < radius && IsTargetChoosen)
        {
            //wait for a few seconds
            //choose next target destination
            StartCoroutine(WaitForSeconds());
            IsTargetChoosen = false;
        }

    }

    private Transform RandomDestination()
    {
        if (target == null)
        {
            return path.Waypoints[ReturnRandomInt(path.Waypoints.Count)];
        }
        else
        {
            List<Transform> tempList = new List<Transform>();
            tempList.Add(target);
            List<Transform> ExceptedList = path.Waypoints.Except(tempList).ToList();
            return ExceptedList[ReturnRandomInt(ExceptedList.Count)];
        }

    }

    private int ReturnRandomInt(int listSize)
    {
        System.Random rand = new System.Random();
        return rand.Next(0, listSize - 1);
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(waitForSeconds);
        SetNewDestination();
    }

    private void SetNewDestination()
    {
        target = RandomDestination();
        agent.SetDestination(target.position);
        IsTargetChoosen = true;
    }

}
