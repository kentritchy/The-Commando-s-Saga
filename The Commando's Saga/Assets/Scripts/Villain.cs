using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain : MonoBehaviour
{
    public Rigidbody2D vilRb;

    public float upForce;
    public float downForce;

    public float health = 40f;
    public float curHealth;


    public bool wallCollision = true;
    public bool facingLeft = true;

    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        vilRb = GetComponent<Rigidbody2D>();
        curHealth = health;
    }

    void Update()
    {
        if(wallCollision == true)
        {
            vilRb.AddForce(Vector2.up * upForce * Time.deltaTime);
        }

        else vilRb.AddForce(Vector2.down * downForce * Time.deltaTime);

        if (curHealth <= 0f)
        {
            //enemyAnim.SetBool("isShot", true);
            spriteRenderer.enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }

    }

   void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            wallCollision = true;   
        }

        if (col.gameObject.tag == "Ceiling")
        {
            wallCollision = false;
        }


        if (col.gameObject.tag == "Bullet")
        {

            TakeDamage(25);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        curHealth = health;
    }
}
