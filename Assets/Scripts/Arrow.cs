﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour {
    [SerializeField]
    private int damage;

    private float speed;
    private Rigidbody2D rb;
    private Vector2 dir;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void FixedUpdate()
    {
        rb.velocity = dir * speed;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetDirection (Vector2 dir)
    {
        this.dir = dir;
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
