using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Sound sound;
    public Button soundToggleButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    public AudioSource buttonClick;

    public GameObject analogControl;

    void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        sound = GameObject.FindObjectOfType<Sound>();
        UpdateSoundIconAndVolume();
    }
    public void PauseSound()
    {
        sound.ToggleSound();
        UpdateSoundIconAndVolume();
    }


    void UpdateSoundIconAndVolume()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 1;
            soundToggleButton.GetComponent<Image>().sprite = soundOnSprite;
        }
        else
        {
            AudioListener.volume = 0;
            soundToggleButton.GetComponent<Image>().sprite = soundOffSprite;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleA");
        buttonClick.Play();
    }

    public void Settings()
    {
        buttonClick.Play();
        Debug.Log("set");
    }

    public void Shop()
    {
        buttonClick.Play();
    }


    /*public void Story()
    {
        loadStory;
    }*/


    public void Quit()
    {
        buttonClick.Play();
        Application.Quit();
        Debug.Log("quit");
    }

   public void InputSelector(int val)
    {
        if (val == 0)
        {
            if (PlayerPrefs.GetInt("input", 0) == 0)
            {
                analogControl.SetActive(true);
                Debug.Log("analog");
            }   
        }

        if (val == 1)
        {
            if (PlayerPrefs.GetInt("input", 1) == 1)
            {
                analogControl.SetActive(false);
                Debug.Log("ang");
            }
        }
    }
}
