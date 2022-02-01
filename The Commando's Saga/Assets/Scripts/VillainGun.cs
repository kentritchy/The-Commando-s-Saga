using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainGun : MonoBehaviour
{
    public Rigidbody2D rocket;

    public float xyz;
    public float shootForce = 20f;

    private float nextTimeToInst;
    private float fireRate = 50f;

    private Villain villainCtrl;

    public AudioSource villainLaserSound;

    void Awake()
    {
        villainCtrl = transform.root.GetComponent<Villain>();
    }

    void Update()
    {
        Inst();
    }

    void Inst()
    {
        {
            if (Time.time > nextTimeToInst)
            {
                if (villainCtrl.facingLeft)
                {
                    nextTimeToInst = Time.time + fireRate / xyz;
                    Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                    bulletInstance.velocity = new Vector2(-shootForce, 0);
                    villainLaserSound.Play();
                }
            }
        }
    }
}
