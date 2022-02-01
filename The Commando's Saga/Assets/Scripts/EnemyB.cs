using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
    public Rigidbody2D enemyRb;
    public Animator enemyAnim;

    public bool facingRight = true;
    public float moveSpeed = 0.6f;
    public float health = 40f;
    public float curHealth;

    public PlayerController playerCtrl;
    public PlayerHealth playerHealth;
    public EnemyBGun enemyBGun;

    public SpriteRenderer spriteRenderer;
    public GameObject tracker;

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        playerCtrl.Start();
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
            enemyBGun.enabled = false;
            tracker.SetActive(false);
        }

        AttackPlayer();
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

    void AttackPlayer()
    {
      
            
        if (facingRight)
        {
            if (this.transform.position.x - playerCtrl.transform.position.x <= 4 && this.transform.position.x > playerCtrl.transform.position.x && Mathf.Abs(playerCtrl.transform.position.y - this.transform.position.y) <= 0.15f)
            {
                Flip();
            }
        }

        else
        
             if (playerCtrl.transform.position.x - this.transform.position.x <= 4 && this.transform.position.x < playerCtrl.transform.position.x && Mathf.Abs(playerCtrl.transform.position.y - this.transform.position.y) <= 0.15f)
             {
                Flip();
             }
    }
}
