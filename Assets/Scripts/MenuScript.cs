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
        settingsContainer.SetActive(false);
    }

    public void Settings()
    {
        buttonsContainer.SetActive(false);
        settingsContainer.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void OpenLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

}
