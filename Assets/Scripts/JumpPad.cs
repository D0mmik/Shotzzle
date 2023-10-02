using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] Material jumpPadMaterial;
    [SerializeField] Material defaultMaterial;
    [SerializeField] bool isActivated;
    Material material;
    Renderer renderer;
    Collider collider;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
        defaultMaterial = renderer.material;
    }

    public void ActivateJumpPad()
    {
        if (isActivated) return;
        isActivated = true;
        renderer.material = jumpPadMaterial;
        transform.tag = "JumpPad";
        collider.isTrigger = false;
        StartCoroutine(nameof(DeActivateJumpPad));
    }

    IEnumerator DeActivateJumpPad()
    {
        yield return new WaitForSeconds(3);
        renderer.material = defaultMaterial;
        transform.tag = "Untagged";
        collider.isTrigger = true;
        isActivated = false;
    }

}
