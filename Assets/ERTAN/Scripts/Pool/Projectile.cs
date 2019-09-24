using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{

    [HideInInspector] public Vector3 target;
    public float rotateSpeed = 200f;
    public float speed = 5f;

    ObjectPooler objPooler;
    RaycastManager rayManager;
    CameraMovement camMove;
    private Rigidbody rb;

    private bool isEnemy;

    public void OnObjectSpawn()
    {
        objPooler = ObjectPooler.Instance;
        rayManager = RaycastManager.Instance;
        camMove = CameraMovement.Instance;
        rb = GetComponent<Rigidbody>();
        isEnemy = rayManager.CheckIsEnemy();

        if (!isEnemy) target = rayManager.lastHitPosition;

    }

    private void FixedUpdate()
    {

        if (isEnemy) target = rayManager.lastHitTransform.position;

        Vector3 direction = (Vector3)target - rb.position;
        direction.Normalize();

        Vector3 rotateAmount = Vector3.Cross(transform.forward, direction);
        rb.angularVelocity = rotateAmount * rotateSpeed;
        rb.velocity = transform.forward * speed;

        float dist = (transform.position - target).sqrMagnitude;

        if (dist < 1 && this.gameObject.activeSelf)
        {
            objPooler.SpawnFromPool("Explosion", transform.position, Random.rotation, -2);
            this.gameObject.SetActive(false);
        }

    }



}
