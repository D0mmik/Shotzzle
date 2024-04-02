using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    
    public static Timer Instance { get; private set; }

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
    }
    public float time;
    public TMP_Text timerText;
    
    Func<Double, string> displayTime;

    void Start()
    {
        displayTime = Leaderboard.Instance.DisplayTime;
    }
    void Update()
    {
        time += Time.deltaTime;
        displayTime(time);
        timerText.text = displayTime(time);
    }
}
