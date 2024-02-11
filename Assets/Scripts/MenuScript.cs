using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject buttonsContainer;
    [SerializeField] GameObject levelsContainer;
    [SerializeField] GameObject settingsContainer;
    public void Play()
    {
        buttonsContainer.SetActive(false);
        levelsContainer.SetActive(true);
    }

    public void Back()
    {
        levelsContainer.SetActive(false);
        buttonsContainer.SetActive(true);
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
