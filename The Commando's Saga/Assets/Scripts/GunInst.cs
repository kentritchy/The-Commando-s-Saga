using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInst : MonoBehaviour
{
    public Rigidbody2D rocket;
   
    public float shootForce = 20f;

    private float nextTimeToInst;
    private float fireRate = 50f;


    private PlayerController playerCtrl;
    public WeaponCol weaponCol;
    public ShootButton joyButtonA;

    // Start is called before the first frame update
    void Awake ()
    {
        playerCtrl = transform.root.GetComponent<PlayerController>();
    }

    void Start()
    {
        joyButtonA = FindObjectOfType<ShootButton>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S) && Time.time > nextTimeToInst || joyButtonA.Pressed && Time.time > nextTimeToInst )

        {
            if (playerCtrl.facingRight)
            {
                nextTimeToInst = Time.time + fireRate / 125;
                Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(shootForce, 0);
            }

            else
            {
                nextTimeToInst = Time.time + fireRate / 125;
                Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, -180, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(-shootForce, 0);
            }
        }

        if (weaponCol.weaponColliderCheck == false)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else GetComponent<BoxCollider2D>().enabled = true;
    }
}
