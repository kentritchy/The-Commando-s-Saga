using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBGun : MonoBehaviour
{
    public GameObject player;

    public Rigidbody2D rocket;

    public float shootForce = 20f;
    private float range = 4f;

    private float nextTimeToInst;
    private float fireRate = 50f;
    private EnemyB enemyBCtrl;

    public AudioSource enemyGunShot;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyBCtrl = transform.root.GetComponent<EnemyB>();
    }


    void Start()
    {
        nextTimeToInst = 1.5f;
    }


    /*void Update()
    {
        Shoot();
    }*/
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBCtrl.transform.position.x - player.transform.position.x) <= range && Mathf.Abs(enemyBCtrl.transform.position.y - player.transform.position.y) <= 2.5f)
        {
            if (Time.time > nextTimeToInst)

            {
                if (enemyBCtrl.facingRight)
                {
                    nextTimeToInst = Time.time + fireRate / 15;
                    Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                    bulletInstance.velocity = new Vector2(shootForce, 0);
                    enemyGunShot.Play();
                }

                else
                {
                    nextTimeToInst = Time.time + fireRate / 25;
                    Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, -180, 0))) as Rigidbody2D;
                    bulletInstance.velocity = new Vector2(-shootForce, 0);
                    enemyGunShot.Play();
                }
            }
        }
    }
}

