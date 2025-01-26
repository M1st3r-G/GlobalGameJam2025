using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BackgroundMover : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D m_rb;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_rb.velocity = Vector2.up * speed; 
    }
}
