using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float time;
    public TMP_Text timerText;
    void Update()
    {
        time += Time.deltaTime;
        DisplayTime(time);
    }
    
    void DisplayTime(float timeToDisplay)
    {
        var t0 = (int) timeToDisplay;
        var m = t0/60;
        var s = t0 - m*60;
        var ms = (int)( (timeToDisplay - t0)*100);

        timerText.text = $"{m:00}:{s:00}:{ms:00}";
    }
}
