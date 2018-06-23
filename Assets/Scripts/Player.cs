using System.Collections;
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
    private bool isRayHitSomething;
    private bool faceRight;
    private float horizontal;
    private float dashTime;
    private float startDashTime;
    private SpriteRenderer spriteRenderer;
    private PlayerAttack weapon;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        isRayHitSomething = false;
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

        //AdjustSpeed();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isOnGround)
            {
                Jump();
                Debug.Log("After jump: " + rb2d.velocity);
            }            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
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

        if (Input.GetKeyDown(KeyCode.X))
        {
            Hit();
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

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 forceBack = new Vector2(trust, 0f);
            if (faceRight)
            {
                rb2d.AddForce(-forceBack, ForceMode2D.Impulse);
            }
            else
            {
                rb2d.AddForce(forceBack, ForceMode2D.Impulse);
            }
            Color tmp = spriteRenderer.color;
            tmp.a -= 0.3f;
            //need to change color to black and white
            if (tmp.a < 0.0f)
            {
                tmp.a = 0.1f;
            }

            spriteRenderer.color = tmp;
        }
    }
    */

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
