using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    /*
    [System.Serializable]
    public class PlayerAttribute
    {
        public float amountOfArrow;
    }
    */
    
    //public PlayerAttribute playerAttr;
    public GameObject shootObject;

    public LayerMask groundLayer;

    private Rigidbody2D rb2d;
    private bool isOnGround;
    private bool faceRight;
    private bool jumpRequest;
    private float horizontal;
    private float dashTime;
    private float startDashTime;
    private float xMin = -21.5f;
    private float xMax = 21.5f;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float shootSpeed;
    [SerializeField]
    private float firingRate;
    [SerializeField]
    private int amountOfArrow;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float trust;
    [SerializeField]
    private float groundedSkin = 0.05f;
    [SerializeField]
    private string horizontalAxis;
    [SerializeField]
    private string jumpButton;
    [SerializeField]
    private string meleeAtkButton;
    [SerializeField]
    private string rangeAtkButton;
    [SerializeField]
    private Vector3 offsetArrow;

    private SpriteRenderer spriteRenderer;
    private PlayerAttack weapon;
    private Vector2 playerSize;
    private Vector2 boxSize;

    void Awake()
    {
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
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
        horizontal = Input.GetAxis(horizontalAxis);
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
            isOnGround = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer) != null);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
       
	}

    void Update()
    {
        if (Input.GetButtonDown(jumpButton) && isOnGround)
        {
            jumpRequest = true;
        }

        if (Input.GetButtonDown(meleeAtkButton))
        {
            Hit();
        }

        if (Input.GetButtonDown(rangeAtkButton) && amountOfArrow > 0)
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }

        if (Input.GetButtonUp(rangeAtkButton))
        {
            CancelInvoke("Fire");
        }
    }

    void MoveHorizontal (float dirHorizontal)
    {
        Vector2 moveVel = rb2d.velocity;
        moveVel.x = dirHorizontal * speed;
        rb2d.velocity = moveVel;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y, transform.position.z);
    }

    void Hit ()
    {
        weapon.Attack();
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.W))
        {
            GameObject arrow = Instantiate(shootObject, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 90f))) as GameObject;
            arrow.GetComponent<Arrow>().SetDirection(Vector2.up);
            arrow.GetComponent<Arrow>().Speed = shootSpeed;
        }

        else if (faceRight)
        {
            GameObject arrow = Instantiate(shootObject, transform.position + offsetArrow, Quaternion.Euler(new Vector3(0f, 0f, 0f))) as GameObject;
            arrow.GetComponent<Arrow>().SetDirection(Vector2.right);
            arrow.GetComponent<Arrow>().Speed = shootSpeed;
        }

        else if (!faceRight)
        {
            GameObject arrow = Instantiate(shootObject, transform.position - offsetArrow, Quaternion.Euler(new Vector3(0f, 0f, 180f))) as GameObject;
            arrow.GetComponent<Arrow>().SetDirection(Vector2.left);
            arrow.GetComponent<Arrow>().Speed = shootSpeed;
        }

        amountOfArrow -= 1;
        Debug.Log("Arrow: " + amountOfArrow);
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

    public int AmountOfArrow
    {
        get {
            return amountOfArrow;
        }

        set {
            amountOfArrow += 1;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed *= value;
        }
    }

    public float JumpPower
    {
        get
        {
            return jumpPower;
        }

        set
        {
            jumpPower *= value;
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
