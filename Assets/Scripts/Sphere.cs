using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private int sphereHealth = 2;
    private Renderer renderer;
    [SerializeField] Material secondMaterial;

    private void OnEnable()
    {
        renderer = GetComponent<Renderer>();
    }

    public void DestroySphere()
    {
        sphereHealth--;
        if (sphereHealth == 0) Destroy(this.gameObject);
        renderer.material = secondMaterial;
    }

}
