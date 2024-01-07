using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] Transform spawnPos;
    public bool canSpawn = true;

    public void StartSpawning()
    {
        StartCoroutine(SpawnBalls());
    }

    public IEnumerator SpawnBalls()
    {
        while (canSpawn)
        {
            float rndX = Random.Range(0f, 25f);
            float rndZ = Random.Range(0f, 8f);
            var position = spawnPos.position;
            GameObject ball = Instantiate(ballPrefab, new Vector3( position.x + rndX,position.y + rndZ, position.z), quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(0,0,-500);
            Destroy(ball, 3);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
