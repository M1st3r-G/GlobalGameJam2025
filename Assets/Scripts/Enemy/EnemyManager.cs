using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private float thunderLifespanAlive;
    [SerializeField] private float thunderLifespanDead;
    
    private GameObject[] m_thunders;
    private bool m_thunderIsOn;

    private void Awake()
    {
        instance = this;
        m_thunders = GameObject.FindGameObjectsWithTag("Thunder");
        for (int i = 0; i < m_thunders.Length; i++)
        {
            StartCoroutine(Attack(m_thunders[i]));
        }
    }
    
    IEnumerator Attack(GameObject thunder)
    {
        while (true)
        {
            yield return new WaitForSeconds(thunderLifespanDead);
            //ChangeThunderAttack(thunder);
            thunder.SetActive(true);
            print("Show");
            yield return new WaitForSeconds(thunderLifespanAlive);
            //ChangeThunderAttack(thunder);
            thunder.SetActive(false);
            print("Hide");
            yield return null;
        }
    }
}
