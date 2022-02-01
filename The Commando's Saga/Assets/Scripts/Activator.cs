using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{

    public GameObject step;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            step.SetActive(true);
        }
    }
}
