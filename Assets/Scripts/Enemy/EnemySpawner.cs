using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HungrySurvivor.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject[] enemyPrefab;
        public int maxEnemyType;
        public float spawnInterval = 2f;
        public float minDistance = 5f;
        public float maxDistance = 10f;
        private Transform player;
        public bool isSpawnable = true;

        void Start()
        {
            maxEnemyType = 1;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            StartCoroutine(SpawnEnemies());
            InvokeRepeating("NewEnemies", 90, 210);
            InvokeRepeating("IncreaseDifficulty", 30, 60);
        }

        IEnumerator SpawnEnemies()
        {
            while (true)
            {
                if (isSpawnable)
                {
                    Vector2 spawnPosition = GetRandomSpawnPosition();
                    Instantiate(enemyPrefab[Random.Range(0, maxEnemyType)], spawnPosition, Quaternion.identity);
                }

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

        public void IncreaseDifficulty()
        {
            if (isSpawnable && spawnInterval > 0.2f)
            {
                spawnInterval *= 0.97f;
            }
        }

        public void NewEnemies()
        {
            if (isSpawnable && maxEnemyType < 4)
            {
                maxEnemyType += 1;
            }
        }

        public void SetSpawnableState(bool spawnable = false)
        {
            isSpawnable = spawnable;
        }
    }
}