using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThunderCloudEnemy : MonoBehaviour
{
    private void Update()
    {
        if (gameObject.transform.position.y < -10)
        {
            EnemyManager.instance.DestroyThunderCloud(gameObject);
        }

        if ()
        {
            
        }
    }
}
