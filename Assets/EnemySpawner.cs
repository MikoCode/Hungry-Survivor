using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float minDistance = 5f;
    public float maxDistance = 10f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector2 GetRandomSpawnPosition()
    {
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(0, 2 * Mathf.PI);
        Vector2 spawnDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Vector2 spawnPosition = (Vector2)player.position + spawnDirection * distance;
        return spawnPosition;
    }
}


