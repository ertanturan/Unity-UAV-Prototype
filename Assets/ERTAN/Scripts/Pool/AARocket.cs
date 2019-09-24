using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AARocket : MonoBehaviour, IPooledObject
{

    [HideInInspector] public Transform target;
    public float rotateSpeed = 200f;
    public float speed = 5f;
    Rigidbody rb;
    ObjectPooler objPooler;
    CameraMovement camMove;
    
    public void OnObjectSpawn()
    {
        objPooler = ObjectPooler.Instance;
        rb = GetComponent<Rigidbody>();
        camMove = CameraMovement.Instance;
        target = camMove.transform;

        transform.position += new Vector3(0, 5, 0);

    }

    private void FixedUpdate()
    {
        Vector3 direction = (Vector3)target.position - rb.position;
        direction.Normalize();

        Vector3 rotateAmount = Vector3.Cross(transform.forward, direction);
        rb.angularVelocity = rotateAmount * rotateSpeed;
        rb.velocity = transform.forward * speed;

        float dist = (transform.position - target.position).sqrMagnitude;

        if (dist < 100 && this.gameObject.activeSelf)
        {
            objPooler.SpawnFromPool("Explosion", transform.position, Random.rotation, -2);
            this.gameObject.SetActive(false);
        }

    }


}
