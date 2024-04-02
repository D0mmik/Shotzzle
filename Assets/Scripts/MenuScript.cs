using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject buttonsContainer;
    [SerializeField] GameObject levelsContainer;
    [SerializeField] GameObject settingsContainer;
    [SerializeField] GameObject leaderboardContainer;

    public void Play()
    {
        buttonsContainer.SetActive(false);
        leaderboardContainer.SetActive(false);
        levelsContainer.SetActive(true);
    }

    public void Back()
    {
        levelsContainer.SetActive(false);
        buttonsContainer.SetActive(true);
        settingsContainer.SetActive(false);
        leaderboardContainer.SetActive(false);
    }

    public void Settings()
    {
        buttonsContainer.SetActive(false);
        settingsContainer.SetActive(true);
        leaderboardContainer.SetActive(false);
    }

    public void OpenLeaderboard()
    {
        Leaderboard.Instance.GetLeaderboard();
        leaderboardContainer.SetActive(true);
        buttonsContainer.SetActive(false);
        settingsContainer.SetActive(false);
        Leaderboard.Instance.RenderTimes(0);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void OpenLevel(string name)
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(name);
    }

}
