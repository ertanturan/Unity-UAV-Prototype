using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LockAndStrike : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair;

    [SerializeField] private Button btnFire;
    [SerializeField] private GameObject lockSign;

    private bool isEnemy = false;

    RaycastManager rayManager;

    public static LockAndStrike Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rayManager = RaycastManager.Instance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(0) || crosshair.anchoredPosition != rayManager.CalculateScreenPosition(rayManager.lastHitPosition))
        {
            switch (rayManager.CheckIsEnemy())
            {
                case true:
                    crosshair.anchoredPosition = rayManager.CalculateScreenPosition(rayManager.lastHitTransform.position);
                    break;
                case false:
                    crosshair.anchoredPosition = rayManager.CalculateScreenPosition(rayManager.lastHitPosition);
                    break;
            }
        }
    }

    
}
