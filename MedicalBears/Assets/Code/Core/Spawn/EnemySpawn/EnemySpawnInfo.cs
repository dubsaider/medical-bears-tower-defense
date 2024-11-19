using System;
using UnityEngine;

namespace Code.Spawn.EnemySpawn
{
    [Serializable]
    public class EnemySpawnInfo
    {
        public GameObject EnemyPrefab;
        public int Count;
    }
}