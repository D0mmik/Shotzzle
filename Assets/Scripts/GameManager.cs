using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text fpsCountText;
    InputSystem playerInput;
    public bool IsPaused;
    public bool deathOrFinished;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject finishScreen;
    
    public static GameManager Instance { get; private set; }
    private void Awake() 
    { 
        if (Instance != null && Instance != this)
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
        
        Application.targetFrameRate = 160;
        playerInput = new InputSystem();
        playerInput.Player.Enable();
        playerInput.Player.Escape.performed += PauseMenu;
        pauseMenu.SetActive(false);
    }
    
    private void OnDisable()
    {
        playerInput.Player.Escape.performed -= PauseMenu;
    }

    private void PauseMenu(InputAction.CallbackContext obj)
    {
        if (deathOrFinished) return;
        Pause();
    }

    public void Pause()
    {
        IsPaused = !IsPaused;
        pauseMenu.SetActive(IsPaused);
        settings.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = IsPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }
    
    public void Settings()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void Back()
    {
        settings.SetActive(false);
        pauseMenu.SetActive(true);
    }
    
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator FpsCounter()
    {
        while(true)
        {
            fpsCountText.text = Mathf.Ceil(1f / Time.deltaTime).ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    public void ShowFinishScreen()
    {
        EndGame();
        finishScreen.SetActive(true);
        if (!PlayerPrefs.HasKey("time")) PlayerPrefs.SetFloat("time", Timer.Instance.time);
        
        Debug.Log(Timer.Instance.time <= PlayerPrefs.GetFloat("time"));
        if (Timer.Instance.time <= PlayerPrefs.GetFloat("time"))
            Leaderboard.Instance.SetTime();
    }

    public void ShowDeathScreen()
    {
        EndGame();
        deathScreen.SetActive(true);
    }
    
    public void ResetScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        deathOrFinished = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void EndGame()
    {
        deathOrFinished = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
