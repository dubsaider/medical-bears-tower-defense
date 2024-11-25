using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Entities;
using Extensions;
using UnityEngine;

namespace Code.Core
{
    public class WavesSwitcher : MonoBehaviour
    {
        public Wave CurrentWave { get; private set; }
        public int CurrentWaveNumber => _currentWaveIndex + 1;
        private CoreManager CoreManager => CoreManager.Instance;
        private Wave[] Waves => CoreManager.CurrentLevel.waves;

        private int _currentWaveIndex;
        
        /// <summary>
        /// Старт первой волны
        /// </summary>
        /// <remarks>Вызывать при инициализации уровня</remarks>
        private void Init()
        {
            _currentWaveIndex = 0;
            CurrentWave = Waves[_currentWaveIndex];
            
            StartTimer(CurrentWave.beforeStartTime);
        }
        
        private void Switch()
        {
            _currentWaveIndex++;

            if (_currentWaveIndex == Waves.Length)
            {
                CoreManager.Victory();
                return;
            }

            CurrentWave = Waves[_currentWaveIndex];
            StartTimer(CurrentWave.beforeStartTime);
        }

        private void StartTimer(TimeStruct timeToWait)
        {
            StartCoroutine(Timer(timeToWait));
        }
        
        private IEnumerator Timer(TimeStruct timeToWait)
        {
            while (!timeToWait.IsNull)
            {
                CoreEventsProvider.WaveTimerUpdated.Invoke(timeToWait.ToString());
                
                yield return new WaitForSeconds(1);
                timeToWait.SubstractSeconds(1);
            }
            
            CoreEventsProvider.WaveStarted?.Invoke();
        }

        private void Awake()
        {
            CoreEventsProvider.LevelStarted += Init;
            
            CoreEventsProvider.AllWaveEnemiesDied += Switch;
        }
    }
}