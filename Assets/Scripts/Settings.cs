using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider sensSlider;
    [SerializeField] Slider volumeSlider;
    private const string sensKey = "SENSITIVITY";
    private const string volumeKey = "VOLUME";
    public static float sensitivity;
    public static float volume;
    [SerializeField] TMP_Text sensText;
    [SerializeField] TMP_Text volumeText;
    
    private void Awake()
    {
        sensSlider.value = sensitivity = PlayerPrefs.HasKey(sensKey) ? PlayerPrefs.GetFloat(sensKey) : 1;
        volumeSlider.value = volume = PlayerPrefs.HasKey(volumeKey) ? PlayerPrefs.GetFloat(volumeKey) : 1;
    }

    public void ChangeSensitivity()
    {
        sensitivity = sensSlider.value;
        sensText.text = math.round(sensitivity * 100).ToString();
        PlayerPrefs.SetFloat(sensKey, sensitivity);
    }
    
    public void ChangeVolume()
    {
        volume = volumeSlider.value;
        AudioListener.volume = volume;
        volumeText.text = math.round(volume * 100).ToString();
        PlayerPrefs.SetFloat(volumeKey, volume);
    }
}
