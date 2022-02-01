using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject textUI;
    public GameObject villain;

    public SpriteRenderer spriteRenderer;
    public Sprite openGate;
    public Sprite closeGate;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemy.Length == 0)
        {
            spriteRenderer.sprite = openGate;
            GetComponent<BoxCollider2D>().enabled = false;
            villain.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            textUI.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        textUI.SetActive(false);
    }
    
    public void LockGate()
    {
        spriteRenderer.sprite = closeGate;
    } 
}
