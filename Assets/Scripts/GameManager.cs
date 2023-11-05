using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text fpsCountText;
    InputSystem playerInput;
    void Awake()
    {
        Application.targetFrameRate = 160;
        StartCoroutine(nameof(FpsCounter));
        playerInput = new InputSystem();
        playerInput.Player.Enable();
        playerInput.Player.QuitGame.performed += QuitGame;
    }

    private void QuitGame(InputAction.CallbackContext obj)
    {
        Application.Quit();
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
