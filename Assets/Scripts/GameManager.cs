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
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settings;
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
        StartCoroutine(nameof(FpsCounter));
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
        Pause();
    }

    public void Pause()
    {
        IsPaused = !IsPaused;
        pauseMenu.SetActive(IsPaused);
        settings.SetActive(false);
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
}
