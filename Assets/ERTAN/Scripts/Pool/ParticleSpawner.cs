using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    ObjectPooler objPooler;
    CameraMovement camMove;
    public string tagOfObjectToSpawn;
    private void Start()
    {
        objPooler = ObjectPooler.Instance;
        camMove = CameraMovement.Instance;
    }

    public void Spawn()
    {
        objPooler.SpawnFromPool(tagOfObjectToSpawn, camMove.mainCam.transform.position + camMove.mainCam.transform.forward, Quaternion.identity,5);
    }
}
