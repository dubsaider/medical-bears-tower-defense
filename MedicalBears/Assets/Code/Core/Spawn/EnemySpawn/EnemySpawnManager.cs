using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Core;
using Code.Entities;
using UnityEngine;

namespace Code.Spawn.EnemySpawn
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        private Wave CurrentWave => CoreManager.Instance.CurrentWave;
        private List<Enemy> SpawnedEnemies => _enemySpawner.spawnedEnemies;
        
        private void BeginSpawn()
        {
            var enemiesCount = CalculateEnemiesToSpawnCount();
            var wavePartitionsCount = CalculateWavePartitionsCount(enemiesCount);
            
            _enemySpawner.StartSpawn(CurrentWave.enemySpawnInfos, wavePartitionsCount);
        }
        
        private int CalculateEnemiesToSpawnCount()
        {
            var enemiesToSpawnCount = 0;
            foreach (var spawnInfo in CurrentWave.enemySpawnInfos)
                enemiesToSpawnCount += spawnInfo.Count;
            return enemiesToSpawnCount;
        }
        
        private int CalculateWavePartitionsCount(int enemiesCount)
        {
            var count = 2;
            while (true)
            {
                if (enemiesCount / count < 10 || count == 5) //TODO жесткий хардкод, надо потом подумать еще
                    return count;

                count++;
            }
        }

        private IEnumerator TrackingRemainingEnemies()
        {
            while (SpawnedEnemies.Any(e=>e.IsAlive()))
                yield return new WaitForSeconds(3);
            
            CoreEventsProvider.AllWaveEnemiesDied?.Invoke();
        }
        
        private void Awake()
        {
            CoreEventsProvider.WaveStarted += BeginSpawn;
            
            CoreEventsProvider.AllWaveEnemiesSpawned += () =>
                StartCoroutine(TrackingRemainingEnemies());
        }
    }
}