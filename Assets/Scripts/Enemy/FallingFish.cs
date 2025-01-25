using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fish : MonoBehaviour
{
    [SerializeField] private float fishSpeed;
    [SerializeField] private PlayerController player;
    private Rigidbody2D m_rb2d;

    private void Awake()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_rb2d.AddForce(Vector2.down * fishSpeed, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        player.LooseHealth(5);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
