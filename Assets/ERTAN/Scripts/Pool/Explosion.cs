using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour , IPooledObject
{

    public void OnObjectSpawn()
    {
        GetComponent<ParticleSystem>().Play();

        Vector3 pos = transform.position + Random.insideUnitSphere * 4;
        pos = new Vector3(pos.x, Mathf.Abs(pos.y), pos.z);
        transform.position = pos;
    }

}
