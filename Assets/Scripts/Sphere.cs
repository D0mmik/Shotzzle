using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sphere : MonoBehaviour
{
    private int sphereHealth = 2;
    private Renderer rendererComponent;
    [SerializeField] Material secondMaterial;
    [SerializeField] Material mainMaterial;
    public bool moveToPlayer;
    private GameObject player;
    private GameObject spawnPos;

    private void OnEnable()
    {
        rendererComponent = GetComponent<Renderer>();
        player = GameObject.FindWithTag("Player");
        spawnPos = GameObject.FindWithTag("GuidedSpawner");
    }

    public void DestroySphere()
    {
        sphereHealth--;
        if (sphereHealth == 0)
        {
            if (moveToPlayer)
            {
                float rndX = Random.Range(-1f, 25f);
                Vector3 position = spawnPos.transform.position;
                transform.position = new Vector3(position.x + rndX, position.y, position.z);
                sphereHealth = 2;
                rendererComponent.material = mainMaterial;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            rendererComponent.material = secondMaterial;
        }
    }

    private void FixedUpdate()
    {
        if (!moveToPlayer) return;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.23f);
    }
}
