using UnityEngine;
using Random = UnityEngine.Random;

public class GuidedEnemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPos;
    [SerializeField] private GameObject enemy;
    private int enemyCount = 3;

    public void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            float rndX = Random.Range(-1f, 25f);
            Vector3 position = spawnPos.position;
            GameObject enemyClone = Instantiate(enemy, new Vector3( position.x + rndX,position.y, position.z), Quaternion.identity);
            Sphere sphere = enemyClone.GetComponent<Sphere>();
            sphere.moveToPlayer = true;
        }
        
    }
}
