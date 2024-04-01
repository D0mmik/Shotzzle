using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour
{
    [SerializeField] GameObject[] platforms;
    private void Start()
    {
        StartCoroutine("Dissapear");
    }

    IEnumerator Dissapear()
    {
        while (true)
        {
            foreach (GameObject platform in platforms)
            {
                platform.gameObject.SetActive(!platform.gameObject.activeSelf);
            }
            yield return new WaitForSeconds(3);
        }
    }
}
