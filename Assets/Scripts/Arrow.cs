using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour {
    [SerializeField]
    private int damage;
    [SerializeField]
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Debug.Log("Hit: " + collision.gameObject.tag);
            Destroy(gameObject);
        }
            
    }
}
