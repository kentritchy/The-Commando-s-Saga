using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
   [SerializeField] Slider healthBar;
    public GameObject Player,  DeathUI, WinUI, Joystick, JoystickA, JoystickB, PauseButton;
 

    public PlayerController playerCtrl;
    public Animator playerAnim;

    public float maxHealth = 100f;
    public float curHealth;
    

    void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = maxHealth;
        curHealth = healthBar.value;
    }

    // Update is called once per frame
    public void Update()
    {
        if (curHealth <= 0)
        {
            Die();
            DeathUI.SetActive(true);
            Joystick.SetActive(false);
            JoystickA.SetActive(false);
            JoystickB.SetActive(false);
            PauseButton.SetActive(false);
        }
    }

    void Die()
    {
        playerCtrl.enabled = false;
        playerAnim.SetBool("isDead", true);
        Player.SetActive(false);    
    }

    void TakeHit(float hit)
    {
        healthBar.value -= hit;
        curHealth = healthBar.value;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Hit")
        {
                TakeHit(26);
        }

        if (col.gameObject.tag == "VHit")
        {
            TakeHit(35);
        }

        if (col.gameObject.tag == "Blade")
        {
            TakeHit(10);
        }

        if (col.gameObject.tag == "NextLevel")
        {
            Destroy(col.gameObject);
            WinUI.SetActive(true);
            Joystick.SetActive(false);
            JoystickA.SetActive(false);
            JoystickB.SetActive(false);
            PauseButton.SetActive(false);
        }

        if (col.gameObject.tag == "FinalLevel")
        {
            Destroy(col.gameObject);
            WinUI.SetActive(true);
            Joystick.SetActive(false);
            JoystickA.SetActive(false);
            JoystickB.SetActive(false);
            PauseButton.SetActive(false);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Water")
        {
            TakeHit(100);
        }
    }
}
