using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    
    [Header("ThunderCloud")] [SerializeField] GameObject thunderAttack;

    private void Awake()
    {
        instance = this;
    }
}
