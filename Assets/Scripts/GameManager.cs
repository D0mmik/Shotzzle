using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int fpsCount = 60;
    [SerializeField] TMP_Text fpsCountText;
    InputSystem playerInput;
    void Awake()
    {
        Application.targetFrameRate = 160;
        StartCoroutine(nameof(FpsCounter));
        playerInput = new InputSystem();
        playerInput.Player.Enable();
        playerInput.Player.Reload.performed += ReloadScene;
    }

    private void ReloadScene(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator FpsCounter()
    {
        while (true)
        {
            fpsCountText.text = Mathf.Ceil(1f / Time.deltaTime).ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
