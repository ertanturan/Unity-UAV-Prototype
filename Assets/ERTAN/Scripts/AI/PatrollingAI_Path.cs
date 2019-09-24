using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingAI_Path : MonoBehaviour
{
    public static PatrollingAI_Path Instance;
    public List<Transform> Waypoints;
    private void Awake()
    {
        Instance = this;
    }


}
