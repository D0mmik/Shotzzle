using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

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
        if (buttonsGO.Any(button => !button.GetComponent<DoorButton>().isButtonActivated)) return;
        animator.SetBool(Door, true);
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool(Door, false);
        Debug.Log("clossing door");
    }
}
