﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed;
    public float maxSpeed;
    public float jumpPower;
    public float dashSpeed;
    public float trust;

    public LayerMask groundLayer;

    private Rigidbody2D rb2d;
    private bool isOnGround;
    private bool faceRight;
    private bool jumpRequest;
    private float horizontal;
    private float dashTime;
    private float startDashTime;

    [SerializeField]
    private float groundedSkin = 0.05f;

    private SpriteRenderer spriteRenderer;
    private PlayerAttack weapon;
    private Vector2 playerSize;
    private Vector2 boxSize;

    void Awake()
    {
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
        Debug.Log(boxSize);
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start () {
        isOnGround = false;
        faceRight = true;

        dashTime = 1f;
        startDashTime = dashTime;

        spriteRenderer = GetComponent<SpriteRenderer>();
        
        foreach (Transform wp in gameObject.transform)
        {
            if (wp.gameObject.CompareTag("Weapon"))
            {
                weapon = wp.gameObject.GetComponent<PlayerAttack>();
            }
        }
	}
	
	void FixedUpdate () {
        horizontal = Input.GetAxis("Horizontal");
        MoveHorizontal(horizontal);
        Flip(horizontal);

        if (jumpRequest)
        {
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            jumpRequest = false;
            isOnGround = false;
        }
        else
        {
            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            Debug.Log(boxCenter);
            isOnGround = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer) != null);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
       
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isOnGround)
        {
            jumpRequest = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Hit();
        }
    }

    void MoveHorizontal (float dirHorizontal)
    {
        Vector2 moveVel = rb2d.velocity;
        moveVel.x = dirHorizontal * speed;
        rb2d.velocity = moveVel;
    }

    void Hit ()
    {
        weapon.Attack();
    }

    void Flip (float horizontal)
    {
        if ((horizontal > 0 && !faceRight) || (horizontal < 0 && faceRight))
        {
            faceRight = !faceRight;

            Vector2 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
    }

    void Dash()
    {
        Vector2 dashVel;

        if (faceRight)
        {
            dashVel = Vector2.right * dashSpeed;
        }
        else
        {
            dashVel = Vector2.left * dashSpeed;
        }

        if (dashTime <= 0)
        {
            dashTime = startDashTime;
            rb2d.velocity = Vector2.zero;
        }

        else
        {
            rb2d.velocity = dashVel;
        }
    }

    public IEnumerator Knockback (float duration, float power, Vector3 dir)
    {
        float timer = 0;
        while (duration > timer)
        {
            timer += Time.deltaTime;

            if (faceRight)
            {
                rb2d.AddForce(new Vector3(dir.x * -power, power, transform.position.z));
            }
            
            else
            {
                rb2d.AddForce(new Vector3(dir.x * power, power, transform.position.z));
            }
        }

        Color tmp = spriteRenderer.color;
        tmp.a -= 0.3f;
        //need to change color to black and white
        if (tmp.a < 0.0f)
        {
            tmp.a = 0.1f;
        }

        spriteRenderer.color = tmp;

        yield return 0;
    }
}
