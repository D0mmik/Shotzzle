using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    [SerializeField] GameObject[] buttonsGO;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        foreach (GameObject button in buttonsGO)
        {
            if (!button.GetComponent<DoorButton>().isButtonActivated) return;
            animator.SetTrigger("OpenDoor");
            break;
        }
    }
}
