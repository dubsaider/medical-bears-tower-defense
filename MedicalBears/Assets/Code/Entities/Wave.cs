using System;
using System.Collections.Generic;
using Code.Spawn.EnemySpawn;
using Extensions;
using UnityEngine;

namespace Code.Entities
{
    [CreateAssetMenu(menuName = "MedicalBears/Wave")]
    [Serializable]
    public class Wave : ScriptableObject
    {
        public TimeStruct beforeStartTime;
        public List<EnemySpawnInfo> enemySpawnInfos;
    }
}