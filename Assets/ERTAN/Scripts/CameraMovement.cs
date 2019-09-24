using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private List<Transform> waypoints = new List<Transform>();
    [SerializeField]
    private float moveSpeed = 2.0f;
    private int waypointIndex = 0;
    private bool Once = false;
    [SerializeField] private float rotationSpeed = 2.0f;
    Vector3 currentVelocity = Vector3.zero;
    RaycastManager rayManager;
    public Camera mainCam;

    [HideInInspector] public Vector3 lookAtPosition;
    public static CameraMovement Instance;
    bool WasEnemy
    {
        get { return rayManager.lastHitTransform.CompareTag(rayManager.enemyTag); }
    }


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].position;
        rayManager = RaycastManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetMouseButton(0))
        {
            lookAtPosition = rayManager.CalculateLookAtPosition(mainCam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)));
        }
        else if (WasEnemy)
        {
            lookAtPosition = rayManager.lastHitTransform.position;

        }

        Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, .2f * Time.deltaTime * rotationSpeed);
    }

    void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {

            float distance = (transform.position - waypoints[waypointIndex].position).sqrMagnitude; // vector3.distance is very slow so i used sqrMagnitude instead  - Ertan
            transform.position = Vector3.SmoothDamp(transform.position, waypoints[waypointIndex].position, ref currentVelocity, moveSpeed * Time.deltaTime * distance);

            if (distance < 10 & Once)
            {
                waypointIndex++;
                Once = false;
            }
            else Once = true;

        }
        else waypointIndex = 0;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < waypoints.Count; i++)
        {
            Vector3 currentWaypoint = waypoints[i].position;
            Vector3 previousWaypoint = Vector3.zero;

            if (i > 0)
            { previousWaypoint = waypoints[i - 1].position; }
            else if (i == 0 && waypoints.Count > 1)
            { previousWaypoint = waypoints[waypoints.Count - 1].position; }

            Gizmos.DrawLine(previousWaypoint, currentWaypoint); // you can observe/adjust the lines in the scene view - Ertan
        }
    }
}
