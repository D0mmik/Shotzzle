using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turret : MonoBehaviour
{
    private GameObject player;
    [SerializeField] GameObject bullet;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(TurretShooting());
    }

    private void Update()
    {
        transform.LookAt(player.transform);
    }

    private IEnumerator TurretShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * 1500);
            Destroy(clone, 5);
        }
    }
    
    
}
