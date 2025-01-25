using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Thunder : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private void OnCollisionEnter2D(Collision2D other)
    {
        player.LooseHealth(5);
    }
}
