using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnerMovement : MonoBehaviour
{
    [SerializeField] private Camera followCamera;
    [SerializeField] private float spawnHeight;
    private void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, followCamera.transform.position.y + spawnHeight, gameObject.transform.position.z);
    }
}
