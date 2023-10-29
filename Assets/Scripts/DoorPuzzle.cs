using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    [SerializeField] GameObject[] buttonsGO;
    public void OpenDoor()
    {
        foreach (GameObject button in buttonsGO)
        {
            if (!button.GetComponent<DoorButton>().isButtonActivated) return;
            Debug.Log("open");
            Destroy(this.gameObject);
            break;
        }
    }
}
