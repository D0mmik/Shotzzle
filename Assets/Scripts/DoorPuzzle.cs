using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    [SerializeField] GameObject[] buttonsGO;
    Animator animator;
    private static readonly int Door = Animator.StringToHash("OpenDoor");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        foreach (GameObject button in buttonsGO)
        {
            if (!button.GetComponent<DoorButton>().isButtonActivated) return;
            animator.SetBool(Door, true);
            StartCoroutine(nameof(CloseDoor));
            break;
        }
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool(Door, false);
        Debug.Log("clossing door");
    }
}
