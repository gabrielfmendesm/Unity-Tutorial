using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies = 20;
    public Vector2 spawnAreaMin = new Vector2((float)-6.5, (float)-3.5);
    public Vector2 spawnAreaMax = new Vector2((float)6.5, (float)3.5);
    public bool spawnContinuously = false;
    public float spawnInterval = 0f;
    public float exclusionRadius = 1.5f;
    public float checkRadius = 0.5f;

    void Start()
    {
        if (spawnContinuously)
        {
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        }
        else
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = Vector2.zero;
        int maxAttempts = 10;
        int attempt = 0;
        bool validPosition = false;

        while (!validPosition && attempt < maxAttempts)
        {
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            spawnPosition = new Vector2(randomX, randomY);

            if (spawnPosition.magnitude < exclusionRadius)
            {
                attempt++;
                continue;
            }

            Collider2D hit = Physics2D.OverlapCircle(spawnPosition, checkRadius);
            if (hit != null && hit.CompareTag("Enemy"))
            {
                attempt++;
                continue;
            }

            validPosition = true;
        }

        if (validPosition)
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Não foi possível encontrar uma posição válida para spawnar o inimigo após " + maxAttempts + " tentativas.");
        }
    }
}