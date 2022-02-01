using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject PauseMenuUI, GameOverMenuUI, WinUI, Joystick, JoystickA, JoystickB, Minimap;
    

    public AudioSource buttonClick;
           
  
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Joystick.SetActive(true);
        JoystickA.SetActive(true);
        JoystickB.SetActive(true);
        buttonClick.Play();
        Minimap.SetActive(true);
    }
    public void Pauze()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Joystick.SetActive(false);
        JoystickA.SetActive(false);
        JoystickB.SetActive(false);
        buttonClick.Play();
        gameIsPaused = true;
        Minimap.SetActive(false);
    }

    public void MarchOn()
    {
        // disable win panel
        WinUI.SetActive(false);

        // load next scene base on index number
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("Menu");
        buttonClick.Play();
    }

    public void Retry()
    {
        GameOverMenuUI.SetActive(false);
        buttonClick.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("retry");
    }
  
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Resume();
        buttonClick.Play();
        Debug.Log("menuselected");
    }
    public void Quit()
    {
        buttonClick.Play();
        Application.Quit();
        Debug.Log("quit");
    }
}
