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
    [SerializeField] TMP_Text ammoText;
    [SerializeField] int ammoCount = 30;
    [SerializeField] GameObject bulletImpact;
    Transform shootPoint;
    InputSystem playerInput;
    RaycastHit hit;
    void Awake()
    {
        playerInput = new InputSystem();
        playerInput.Player.Enable();
        playerInput.Player.Shoot.performed += Shoot;
        //ammoText.text = ammoCount.ToString();
        shootPoint = Camera.main?.transform;
    }

    void Shoot(InputAction.CallbackContext obj)
    {
        ammoCount--;
        //ammoText.text = ammoCount.ToString();
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 100f))
        {
            hit.transform.GetComponent<JumpPad>()?.ActivateJumpPad();
            GameObject clone = Instantiate(bulletImpact, hit.point, Quaternion.identity);
            Destroy(clone, 3f);
            if (hit.rigidbody)
            {
                //hit.rigidbody.maxLinearVelocity = 20;
                //hit.rigidbody.AddForce(shootPoint.forward * 500);
            }
        }
    }
}
