using System.Collections;
using Code.Entities;
using Extensions;
using UnityEngine;

namespace Code.Core
{
    public class WavesSwitcher : MonoBehaviour
    {
        public Wave CurrentWave
        {
            get => _currentWave;
            private set
            {
                _currentWave = value;
                CoreEventsProvider.WaveSwitched.Invoke(CurrentWaveNumber, Waves.Length);
            }
        }

        public int CurrentWaveNumber => _currentWaveIndex + 1;
        private CoreManager CoreManager => CoreManager.Instance;
        private Wave[] Waves => CoreManager.CurrentLevel.waves;

        private Wave _currentWave;
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
                CoreEventsProvider.LevelPassed.Invoke();
                return;
            }

            CurrentWave = Waves[_currentWaveIndex];
            StartTimer(CurrentWave.beforeStartTime);
        }

        private void StartTimer(TimeStruct timeToWait)
        {
            StartCoroutine(Timer(timeToWait));
        }

        private void StopTimer()
        {
            StopAllCoroutines();
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

            CoreEventsProvider.LevelPassed += StopTimer;
            CoreEventsProvider.LevelNotPassed += StopTimer;
            
            CoreEventsProvider.AllWaveEnemiesDied += Switch;
        }
    }
}