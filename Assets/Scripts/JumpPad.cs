using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] Material jumpPadMaterial;  
    [SerializeField] Material defaultMaterial;
    bool isActivated = false;
    Material material;
    Renderer renderer;
    Collider collider;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
        defaultMaterial = renderer.material;
    }

    public void ActivateJumpPad()
    {
        isActivated = !isActivated;
        renderer.material = isActivated ? jumpPadMaterial : defaultMaterial;
        transform.tag = !isActivated ? "Untagged" : "JumpPad";
        collider.isTrigger = !isActivated;
    }
}
