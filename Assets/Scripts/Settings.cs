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
    [SerializeField] TMP_Dropdown resolutionDP;
    Resolution[] resolutions;
    List<Resolution> filteredResolutions;
    float currentRefreshRate;
    int currentResIndex = 0;
    
    private void Awake()
    {
        sensSlider.value = sensitivity = PlayerPrefs.HasKey(sensKey) ? PlayerPrefs.GetFloat(sensKey) : 1;
        volumeSlider.value = volume = PlayerPrefs.HasKey(volumeKey) ? PlayerPrefs.GetFloat(volumeKey) : 1;

        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        
        resolutionDP.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " +
                                      filteredResolutions[i].refreshRate + " Hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
                currentResIndex = i;
        }
        
        resolutionDP.AddOptions(options);
        resolutionDP.value = currentResIndex;
        resolutionDP.RefreshShownValue();
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = filteredResolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.MaximizedWindow, resolution.refreshRate);
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
