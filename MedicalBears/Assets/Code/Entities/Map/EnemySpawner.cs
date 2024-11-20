using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Core;
using Code.Spawn.EnemySpawn;
using Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{   
    [HideInInspector] public List<Enemy> spawnedEnemies;

    [SerializeField] private float spawnInterval = 1f; 
    [SerializeField] private float partitionInterval = 5f;
    [SerializeField] private float spawnRange = 5f;

    private List<EnemySpawnInfo> _enemiesToSpawn;
    private int _partitionsCount; 

    public void StartSpawn(List<EnemySpawnInfo> enemySpawnInfos, int partitionsCount)
    {
        InitEnemiesToSpawn(enemySpawnInfos);
        spawnedEnemies = new();
        
        _partitionsCount = partitionsCount;
        
        StartCoroutine(SpawnProcess());
    }

    private void InitEnemiesToSpawn(List<EnemySpawnInfo> enemySpawnInfos)
    {
        _enemiesToSpawn = enemySpawnInfos.Select(e =>
            new EnemySpawnInfo
            {
                EnemyPrefab = e.EnemyPrefab,
                Count = e.Count
            }).ToList();
    }

    private IEnumerator SpawnProcess()
    {
        while (true)
        {
            foreach (var spawnInfo in _enemiesToSpawn)
            {
                if(spawnInfo.Count == 0)
                    continue;
                
                var count = spawnInfo.Count % _partitionsCount;
                if (count == 0)
                    count = spawnInfo.Count / _partitionsCount;

                spawnInfo.Count -= count;

                for (var i = 0; i < count; i++)
                {
                    var spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRange;
                    spawnedEnemies.Add(ObjectsManager.CreateObject(spawnInfo.EnemyPrefab, spawnPosition)
                        .GetComponent<Enemy>());
                    yield return new WaitForSeconds(spawnInterval);
                }
            }

            if (IsAllEnemiesSpawned())
            {
                EventsProvider.AllWaveEnemiesSpawned?.Invoke();
                break;
            }

            yield return new WaitForSeconds(partitionInterval);
        }
    }

    private bool IsAllEnemiesSpawned()
    {
        return !_enemiesToSpawn.Any(i => i.Count > 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}