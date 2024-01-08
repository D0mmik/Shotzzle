using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeCalculator : MonoBehaviour
{
    public string number;
    public TMP_Text numberText;
    public string randomNumber;
    public TMP_Text randomNumberText;
    public GameObject bouncePad;

    private void Start()
    {
        randomNumberText.text = randomNumber = Random.Range(1000, 10000).ToString();
        bouncePad.SetActive(false);
    }
}
