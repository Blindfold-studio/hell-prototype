﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forpitching : MonoBehaviour {

    public float speed;

    [SerializeField]
    private int health;

    private Transform targetplayer;
    private Animator animator;
    private Player player;
    private Rigidbody2D rg;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start () {
        targetplayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Vector2.Distance(transform.position, targetplayer.position) > 0.7)
        {
            animator.SetInteger("SlimeState", 0);
            transform.position = Vector2.MoveTowards(transform.position, targetplayer.position, speed * Time.deltaTime);
        }
        else
        {
            animator.SetInteger("SlimeState", 1);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            
            if (player != null)
            {
                Debug.Log(collision.gameObject.tag);
                StartCoroutine(player.Knockback(0.5f, 5f, player.transform.position));
            }  
        }

        if (collision.CompareTag("Weapon"))
        {
            Sword weapon = collision.GetComponent<Sword>();

            health -= weapon.GetDamage();
            Debug.Log("Slime health: " + health);

            if (weapon.transform.position.x < this.transform.position.x)
            {
                Debug.Log("Player left");
                StartCoroutine(this.Knockback(0.5f, 5f));
            }
                
            else
            {
                Debug.Log("Player right");
                StartCoroutine(this.Knockback(0.5f, -5f));
            }
                

            if (health <= 0)
            {
                // died animation
                Debug.Log("Slime died");
            }
        }
    }

    public IEnumerator Knockback(float duration, float power)
    {
        float timer = 0;
        while (duration > timer)
        {
            timer += Time.deltaTime;

            rg.AddForce(new Vector3(power, 0f, transform.position.z));
        }

        yield return 0;
    }
}
