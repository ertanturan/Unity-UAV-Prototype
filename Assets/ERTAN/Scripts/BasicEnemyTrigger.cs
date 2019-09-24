using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyTrigger : MonoBehaviour
{
    [HideInInspector] public bool triggered;
    CameraMovement camMove;
    float distance;
    private void Start()
    {
        camMove = CameraMovement.Instance;
    }

    private void LateUpdate()
    {
        distance = (transform.position - camMove.transform.position).sqrMagnitude;
        if (distance < 25)
        {
            triggered = true;
        }
        else
        {
            triggered = false;
        }
    }
}
