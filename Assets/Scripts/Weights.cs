using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weights : MonoBehaviour
{
    [SerializeField] TMP_Text weightText;
    [SerializeField] int finalWeight;
    [SerializeField] int currentWeight;
    [SerializeField] GameObject wall;
    [SerializeField] Animator animatorComponent;

    void Start()
    {
        finalWeight = Random.Range(15, 45);
        weightText.text = "> " + finalWeight;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("RigidbodyExplosive")) return;
        animatorComponent.SetTrigger("weightAnimation");
        currentWeight += int.Parse(other.gameObject.name);
        Destroy(other.gameObject, 3);
        
        if (currentWeight >= finalWeight)
            wall.SetActive(false);
    }
}
