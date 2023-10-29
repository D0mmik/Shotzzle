using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public bool isButtonActivated;

    [SerializeField] Material blueMaterial;
    public void Activate()
    {
        if (isButtonActivated) return;
        GetComponent<Renderer>().material = blueMaterial;
        isButtonActivated = true;
    }
}
