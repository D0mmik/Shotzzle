using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Settings()
    {
        Debug.Log("settings");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

}
