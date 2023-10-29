using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float multiplier;
    InputSystem playerInput;

    private void Awake()
    {
        playerInput = new InputSystem();
        playerInput.Player.Enable();
    }

    private void Update()
    {
        // get mouse input
        Vector2 mouseAxis = playerInput.Player.Look.ReadValue<Vector2>() * (Time.deltaTime * multiplier);

        // calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseAxis.y, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseAxis.x, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        // rotate 
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
