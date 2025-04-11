using UnityEngine;
using System.Collections.Generic;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int numberOfCollectibles = 10;
    public float minX, maxX, minY, maxY;
    public float minDistance = 1f;

    private List<Vector2> spawnedPositions = new List<Vector2>();

    void Start()
    {
        for (int i = 0; i < numberOfCollectibles; i++)
        {
            Vector2 spawnPos = GetValidSpawnPosition();
            Instantiate(collectiblePrefab, spawnPos, Quaternion.identity);
            spawnedPositions.Add(spawnPos);
        }
    }

    Vector2 GetValidSpawnPosition()
    {
        int maxAttempts = 100;
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector2 potentialPos = new Vector2(randomX, randomY);
            bool valid = true;
            foreach (Vector2 pos in spawnedPositions)
            {
                if (Vector2.Distance(pos, potentialPos) < minDistance)
                {
                    valid = false;
                    break;
                }
            }
            if (valid)
                return potentialPos;
        }
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}