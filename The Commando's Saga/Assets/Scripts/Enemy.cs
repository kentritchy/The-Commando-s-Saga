using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D enemyRb;
    public Animator enemyAnim;

    public bool facingRight = true;
    public float moveSpeed = 0.6f;
    public float health = 40f;
    public float curHealth;

    public PlayerHealth playerHealth;
    public EnemyGun enemyGun;

    public SpriteRenderer spriteRenderer;
    public GameObject tracker;


    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
    }

   void Start()
    {
        curHealth = health;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (facingRight)
        {
            enemyRb.velocity = new Vector2(1f * moveSpeed, 0f);
        }
        else enemyRb.velocity = new Vector2(1f * -moveSpeed, 0f);

        if (curHealth <= 0f)
        {
            enemyAnim.SetBool("isShot", true);
            this.GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(gameObject, 1f);
        }


        if (playerHealth.curHealth <= 0)
        {
            spriteRenderer.enabled = false;
            enemyGun.enabled = false;
            tracker.SetActive(false);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {

            TakeDamage(25);
        }

        if (col.gameObject.tag == "Triggers")
        {
            Flip();
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        curHealth = health;
    }
}
