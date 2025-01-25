using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FallingFishSpawner : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject fallingFish;
    [SerializeField] private float shootDelay;
    [SerializeField] private float spawnRangeRight;
    [SerializeField] private float spawnRangeLeft;
    [SerializeField] private float spawnHeight;
    [SerializeField] private float waitDelay;
    [SerializeField] private bool[] checkmark;

    private ProgressBar currentProgress;

    private int chapters;
    private bool m_canSpawn;
    private float m_rndPos;
    

    private void Awake()
    {
        StartCoroutine(SpawnFallingFish());
        StartCoroutine(SpawnWait());
    }

    private void Update()
    {
        m_rndPos = Random.Range(spawnRangeLeft, spawnRangeRight);
    }

    IEnumerator SpawnFallingFish()
    {
        while (true)
        {
            yield return new WaitUntil(() => m_canSpawn);
            gameObject.transform.position = new Vector3(camera.transform.position.x + m_rndPos, camera.transform.position.y + spawnHeight, camera.transform.position.z + 10);
            yield return new WaitForSeconds(shootDelay);
            Instantiate(fallingFish, gameObject.transform.position, gameObject.transform.rotation);
            yield return null;
        }
    }

    IEnumerator SpawnWait()
    {

        for (int i = 1; i < chapters + 1; i++)
        {
            yield return new WaitUntil(() => currentProgress.progress >= i / (float)chapters);
            m_canSpawn = checkmark[i - 1];
        }
    }
}
