using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastManager : MonoBehaviour
{
    public static RaycastManager Instance;
    [HideInInspector] public string enemyTag = "Enemy";


    //[HideInInspector] public Transform hitTransform;
     public Vector3 lastHitPosition;
     public Transform lastHitTransform;
    CameraMovement camMove;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        camMove = CameraMovement.Instance;
        camMove.lookAtPosition = CalculateLookAtPosition(camMove.mainCam.ScreenPointToRay(new Vector3(Screen.width - Screen.width / 2, Screen.height - Screen.height / 2, 0)));
    }

    public Vector2 CalculateScreenPosition(Vector3 position)
    {
        Vector3 vec = Camera.main.WorldToScreenPoint(position);
        return vec;
    }

    public Vector3 CalculateLookAtPosition(Ray ray)
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                lastHitPosition = hit.point;
                lastHitTransform = hit.transform;
                return hit.point;
            }
        }

        return lastHitPosition;

    }

    public bool CheckIsEnemy()
    {
        return lastHitTransform.CompareTag(enemyTag);
    }
}
