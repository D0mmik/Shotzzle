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
    Renderer rendererComponent;
    Collider colliderComponent;

    void Start()
    {
        rendererComponent = GetComponent<Renderer>();
        colliderComponent = GetComponent<Collider>();
        defaultMaterial = rendererComponent.material;
    }

    public void ActivateJumpPad()
    {
        if (isActivated) return;
        isActivated = true;
        rendererComponent.material = jumpPadMaterial;
        transform.tag = "JumpPad";
        colliderComponent.isTrigger = false;
        StartCoroutine(nameof(DeActivateJumpPad));
    }

    IEnumerator DeActivateJumpPad()
    {
        yield return new WaitForSeconds(3);
        rendererComponent.material = defaultMaterial;
        transform.tag = "Untagged";
        colliderComponent.isTrigger = true;
        isActivated = false;
    }

}
