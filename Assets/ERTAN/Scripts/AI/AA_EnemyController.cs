using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AA_EnemyController : MonoBehaviour
{
    [SerializeField] BasicEnemyTrigger trigger;
    CameraMovement camMove;
    Transform target;
    ObjectPooler objPooler;

    string tagOfObjectToSpawn = "AARocket";
    private float coolDownPeriot = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        camMove = CameraMovement.Instance;
        objPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (trigger.triggered)
        {
            case true:
                //ATTACK

                coolDownPeriot -= Time.deltaTime;

                if (coolDownPeriot < 0) { AttackToANKA(); coolDownPeriot = 7.0f; }


                break;
            case false:
                //IDLE
                break;
        }
    }

    private void AttackToANKA()
    {
        objPooler.SpawnFromPool(tagOfObjectToSpawn,transform.position, Quaternion.identity, 1);
    }
}
