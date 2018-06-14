using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed;
    public float maxSpeed;
    public float jumpPower;

    public LayerMask groundLayer;

    private Rigidbody2D rb2d;
    private bool isOnGround;
    private bool isRayHitSomething;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        isRayHitSomething = false;
        isOnGround = false;
	}
	
	void FixedUpdate () {
        MoveHorizontal(Input.GetAxis("Horizontal"));

        //AdjustSpeed();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isOnGround)
            {
                Jump();
                Debug.Log("After jump: " + rb2d.velocity);
            }            
        }
       
	}

    void Update()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.6f, groundLayer))
        {
            isRayHitSomething = true;
            isOnGround = true;
            //rb2d.velocity = new Vector3(rb2d.velocity.x, 0f);
        }
        else
        {
            isRayHitSomething = false;
            isOnGround = false;
        }

        if (isRayHitSomething)
        {
            Debug.DrawLine(this.transform.position, this.transform.position + Vector3.down * 0.6f, Color.red);
        }
        else
        {
            Debug.DrawLine(this.transform.position, this.transform.position + Vector3.down * 0.6f, Color.green);
        }
    }

    void AdjustSpeed ()
    {
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }

    void MoveHorizontal (float dirHorizontal)
    {
        Vector2 moveVel = rb2d.velocity;
        moveVel.x = dirHorizontal * speed;
        rb2d.velocity = moveVel;
    }

    void Jump ()
    {
        //rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        Debug.Log("Before jump: " + rb2d.velocity);
        //rb2d.velocity += jumpPower * Vector2.up;
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
    }
}
