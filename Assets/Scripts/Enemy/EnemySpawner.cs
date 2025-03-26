using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnRate = 5f;
    private float timer;

    private const int MinimumSpawnRadius = 10;
    private const int MaximumSpawnRadius = 50;
    [Range(MinimumSpawnRadius, MaximumSpawnRadius)]
    public int spawnRadius = 10;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));
        Instantiate(enemyPrefabs[index], spawnPosition, Quaternion.identity);
    }
}