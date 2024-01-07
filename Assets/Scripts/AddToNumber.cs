using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToNumber : MonoBehaviour
{
    private CubeCalculator cubeCalculator;
    private Spawner spawner;
    private void Start()
    {
        cubeCalculator = FindObjectOfType<CubeCalculator>();
        cubeCalculator.numberText.text = "";

        spawner = FindObjectOfType<Spawner>();
    }

    public void AddNumber(int num)
    {
        if (cubeCalculator.number == null) return;
        
        cubeCalculator.number = num == 100
            ? cubeCalculator.number.Length != 0 ? cubeCalculator.number.Remove(cubeCalculator.number.Length - 1) : cubeCalculator.number
            : cubeCalculator.number + num;

        cubeCalculator.numberText.text = cubeCalculator.number;

        if (cubeCalculator.number != cubeCalculator.randomNumber) return;
        
        spawner.canSpawn = false;
        Debug.Log("WIN");
    }
}
