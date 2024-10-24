using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    [SerializeField] private GameObject[] enemyPrefabs; 
    [SerializeField] private int numberOfEnemiesPerWave = 5;
    [SerializeField] private float enemySpawnInterval = 1f; 
    [SerializeField] private float waveInterval = 5f; 
    [SerializeField] private float spawnRange = 5f; 

    void Start()
    {
        StartCoroutine(SpawnWaveCoroutine());
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        while (true)
        {
            Vector3 spawnPosition1 = transform.position + Random.insideUnitSphere * spawnRange;
            spawnPosition1.z = 0f; 
            GameObject enemy1 = Instantiate(enemyPrefabs[1], spawnPosition1, Quaternion.identity);
            yield return new WaitForSeconds(enemySpawnInterval);

            for (int i = 0; i < 4; i++)
            {
                Vector3 spawnPosition2 = transform.position + Random.insideUnitSphere * spawnRange;
                spawnPosition2.z = 0f; 
                GameObject enemy2 = Instantiate(enemyPrefabs[0], spawnPosition2, Quaternion.identity);
                yield return new WaitForSeconds(enemySpawnInterval);
            }

            yield return new WaitForSeconds(waveInterval);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}