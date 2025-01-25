using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    
    [Header("ThunderCloud")][SerializeField] GameObject thunderCloud;
    [SerializeField] GameObject thunderAttack;
    
    private List<GameObject> thunderClouds = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
}
