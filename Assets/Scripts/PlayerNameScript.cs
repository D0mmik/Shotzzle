using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameIF;
    private void Start()
    {
        if (PlayerPrefs.HasKey("PlayerName")) gameObject.SetActive(false);
    }

    public void SaveName()
    {
        if (string.IsNullOrEmpty(nameIF.text)) return;
        
        PlayerPrefs.SetString("PlayerName", nameIF.text);
        gameObject.SetActive(false);
    }
}
