using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int fpsCount = 60;
    [SerializeField] TMP_Text fpsCountText;
    void Awake()
    {
        Application.targetFrameRate = 160;
        StartCoroutine(nameof(FpsCounter));
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
