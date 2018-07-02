using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveToPlayer : MonoBehaviour {
    public Transform playerPos;

    private Vector3 offset;
    private Vector3 dir;
    private float speed = 5f;
    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        offset = playerPos.position - this.transform.position;
        dir = Vector3.Normalize(offset);
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = dir * speed;
	}
}
