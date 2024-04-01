using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomColor : MonoBehaviour
{
    public GameObject[] objectsToColor;

    void Start()
    {
        foreach (GameObject obj in objectsToColor)
        {
            Color randomColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
            obj.GetComponent<Renderer>().material.color = randomColor;
        }
    }
}

