using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int fpsCount = 100;
    [SerializeField] TMP_Text fpsCountText;
    void Awake()
    {
        Application.targetFrameRate = fpsCount;
        StartCoroutine(nameof(FpsCounter));
    }

    void Update()
    {
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
