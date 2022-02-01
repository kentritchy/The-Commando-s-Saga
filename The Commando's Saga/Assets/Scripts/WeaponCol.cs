using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCol : MonoBehaviour
{

    public Transform colliderCheck;
    public LayerMask groundLayer;
    public bool weaponColliderCheck = false;


    // Update is called once per frame
    void Update()
    {
        weaponColliderCheck = Physics2D.Linecast(transform.position, colliderCheck.position, groundLayer);
    }
}
