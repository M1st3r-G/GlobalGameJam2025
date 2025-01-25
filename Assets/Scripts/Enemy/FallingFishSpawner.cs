using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallingFishSpawner : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject fallingFish;
    [SerializeField] private int shootDelay;
    [SerializeField] private float spawnRangeRight;
    [SerializeField] private float spawnRangeLeft;
    [SerializeField] private float spawnHeight;
    
    private float m_rndPos;

    private void Awake()
    {
        StartCoroutine(SpawnFallingFish());
    }

    private void Update()
    {
        m_rndPos = Random.Range(spawnRangeLeft, spawnRangeRight);
    }

    IEnumerator SpawnFallingFish()
    {
        while (true)
        {
            gameObject.transform.position = new Vector3(camera.transform.position.x + m_rndPos, camera.transform.position.y + spawnHeight, camera.transform.position.z + 10);
            yield return new WaitForSeconds(shootDelay);
            Instantiate(fallingFish, gameObject.transform.position, gameObject.transform.rotation);
            yield return null;
        }
    }
}
