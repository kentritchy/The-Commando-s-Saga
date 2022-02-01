using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public Animator playerAnim;

    public float x = 0f;
    private float xMin = 7.3f;
    private float xMax = 57f;
    public float horizontalSpeed;
    public float jumpPower = 10f;

    public bool facingRight = true;
    public bool canDoubleJump = false;

    public Transform groundCheck;
    public Transform ceilingCheck;
    public LayerMask groundLayer;
    public LayerMask ceilingLayer;
    public bool isOnGround = false;
    private bool notTallEnough = false;
    public Transform enemyB;

    public Joystick joystick;
    public ShootButton joyButtonA;
    public ButtonController joyButtonB;
    private float nextTimeToInst;
    private float fireRate = 50f;
    private float lastTimeClick;
    private const float doubleClickTime = 0.5f;

    public AudioSource gunShot;

    public GateController gateController;
    public ButtonController buttonController;
    public GameObject barricade;

    

    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    public void Start()
    {
        facingRight = true;
        joystick = FindObjectOfType<Joystick>();
        joyButtonA = FindObjectOfType<ShootButton>();
        joyButtonB = FindObjectOfType<ButtonController>();
        barricade.GetComponent<BoxCollider2D>().enabled = false;
    }  

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        notTallEnough = Physics2D.Linecast(transform.position, ceilingCheck.position, ceilingLayer);


       transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xMin, xMax), transform.position.y, transform.position.z);


        x = joystick.Horizontal * Time.deltaTime * horizontalSpeed;
        playerAnim.SetFloat("speed", Mathf.Abs(x));
        transform.Translate(x, 0, 0);


        if (Input.GetKeyUp(KeyCode.S) && Time.time > nextTimeToInst || joyButtonA.Pressed && Time.time > nextTimeToInst  )
        {
            nextTimeToInst = Time.time + fireRate / 200;
            playerAnim.SetBool("isShooting", true);
            gunShot.Play();
        } 
        else playerAnim.SetBool("isShooting", false);

        Crouch();

        DontStand();

        x = joystick.Horizontal;

       // Jump();

    }


    void FixedUpdate()
    {
        Flip(x);
    }

   public void Jump()
    {

       // if (Input.GetKeyDown(KeyCode.Space))
        
            if (isOnGround)
            {

                playerRb.velocity = Vector2.up * jumpPower;
                playerAnim.SetBool("isJumping", true);
                isOnGround = false;
                Debug.Log("Singlejump");

                canDoubleJump = true;
            }

            else if (canDoubleJump)
            {

                {
                    if (facingRight)
                    {
                        jumpPower = jumpPower * 1;
                        horizontalSpeed = joystick.Horizontal * 90f * Time.deltaTime;
                        playerRb.velocity = Vector2.up * jumpPower;
                        playerAnim.SetBool("isJumping", true);
                        isOnGround = false;
                        
                        Debug.Log("doublejump");
                        canDoubleJump = false;
                        jumpPower = jumpPower / 1;
                }

                    if (!facingRight)
                    {
                        jumpPower = jumpPower * 1;
                        horizontalSpeed = joystick.Horizontal * -90f * Time.deltaTime;
                        playerRb.velocity = Vector2.up * jumpPower;
                        playerAnim.SetBool("isJumping", true);
                        isOnGround = false;

                        Debug.Log("doublejump");
                        canDoubleJump = false;
                        jumpPower = jumpPower / 1;
                }
                }
            }
        

        if (!joyButtonB.Pressed)
        {
            playerAnim.SetBool("isJumping", false);
        }
    }


    public void Flip(float x)
    {
        {
            if (x > 0 && !facingRight || x < 0 && facingRight)
            {
                facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.DownArrow) && facingRight || joystick.Vertical < -.75f && facingRight)
        {
            horizontalSpeed = joystick.Horizontal * 35f * Time.deltaTime;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            playerAnim.SetBool("isSquatting", true);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !facingRight || joystick.Vertical < -.75f && !facingRight)

        {
            horizontalSpeed = joystick.Horizontal * -35f * Time.deltaTime;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            playerAnim.SetBool("isSquatting", true);
        }

        else 
        {
            horizontalSpeed = 1.5f;
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            playerAnim.SetBool("isSquatting", false);
        }
    }

    void DontStand()
    {
        if (notTallEnough == true)
        {
            playerAnim.SetBool("isSquatting", true);
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" && facingRight)
        {
            transform.Translate(Vector2.left * 0.2f);
        }

        if (col.gameObject.tag == "Enemy" && !facingRight)
        {
            transform.Translate(Vector2.right * 0.2f);
        }

        if (col.gameObject.tag == "Blade" && facingRight)
        {

            transform.Translate(Vector2.left * 0.4f);
        }

        if (col.gameObject.tag == "Blade" && !facingRight)
        {
            transform.Translate(Vector2.right * 0.4f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("gateclosed");
        gateController.LockGate();
        barricade.GetComponent<BoxCollider2D>().enabled = true;
    }
}
