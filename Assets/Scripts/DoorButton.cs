using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public bool isButtonActivated;
    public void Activate()
    {
        Debug.Log("activated");
        isButtonActivated = true;
    }
}
