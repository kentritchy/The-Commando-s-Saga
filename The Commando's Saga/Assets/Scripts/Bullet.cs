using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Animator explosion;

    public float damage = 25f;
    public float range = 4.8f;

    public GameObject player;


    void Start()
    {
        explosion = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        GunRange();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Bullet" || col.gameObject.tag == "Blade" || col.gameObject.tag == "NextLevel" || col.gameObject.tag == "FinalLevel")
        {
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyB")
        {
            explosion.SetBool("hasHit", true);
            Destroy(gameObject, 0.3f);
        }

    }

    void GunRange()
    {
       if (Mathf.Abs(this.transform.position.x - player.transform.position.x) >= range)
        {
            Destroy(gameObject);
        }
    }
}
