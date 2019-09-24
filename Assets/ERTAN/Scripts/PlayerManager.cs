using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singletion

    public static PlayerManager player;

    private void Awake()
    {
        player = this;
    }


    #endregion
}
