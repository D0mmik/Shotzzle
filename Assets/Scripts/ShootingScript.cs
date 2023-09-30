using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] int ammoCount = 30;
    InputSystem playerInput;
    RaycastHit hit;
    private void Awake()
    {
        playerInput = new InputSystem();
        playerInput.Player.Enable();
        playerInput.Player.Shoot.performed += Shoot;
        ammoText.text = ammoCount.ToString();
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        ammoCount--;
        ammoText.text = ammoCount.ToString();
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 100f))
        {
            hit.transform.GetComponent<JumpPad>()?.ActivateJumpPad();
            if (hit.rigidbody)
            {
                //hit.rigidbody.maxLinearVelocity = 20;
                //hit.rigidbody.AddForce(shootPoint.forward * 500);
            }
        }
    }
}
