using System;
using Code.Core.RewardsAndBalance;
using Code.Entities;
using UnityEngine;

namespace Code.Core
{
    public class CoreManager : MonoBehaviour
    {
        public static CoreManager Instance;

        public Level CurrentLevel => _levelsSwitcher.CurrentLevel;
        public Wave CurrentWave => _wavesSwitcher.CurrentWave;
        public int CurrentWaveNumber => _wavesSwitcher.CurrentWaveNumber;

        public Map Map { get; private set; }
        public BalanceMediator BalanceMediator { get; private set; }

        [SerializeField] private SceneRenderer _sceneRenderer;

        private LevelsSwitcher _levelsSwitcher;
        private WavesSwitcher _wavesSwitcher;
        private MapGenerator _mapGenerator;

        public void StartNewGame()
        {
            SaveLoadHandler.ClearSaves();
            _levelsSwitcher.Init(0);
            StartCurrentLevel();
        }
        
        public void ContinueGame()
        {
            var index = SaveLoadHandler.GetLastPassedLevelIndex();
            if (index < 0)
                index++;
            
            _levelsSwitcher.Init(index);
            StartCurrentLevel();
        }

        public void StartSelectedLevel(int selectedLevelIndex)
        {
            _levelsSwitcher.Init(selectedLevelIndex);
            StartCurrentLevel();
        }
        
        public void NextLevel()
        {
            _levelsSwitcher.Switch();
            StartCurrentLevel();
        }
        
        public void RestartLevel()
        {
            StartCurrentLevel();
        }

        public void FinishLevelManually()
        {
            CoreEventsProvider.LevelNotPassed.Invoke();
        }

        private void StartCurrentLevel()
        {
            Map = _mapGenerator.Generate(CurrentLevel.mapIndex);
            _sceneRenderer.Render(Map);

            BalanceMediator = new(CurrentLevel.startBalance);
            CoreEventsProvider.LevelStarted.Invoke();
            
            GameModeManager.SetDefaultMode();
        }
        
        private void Awake()
        {
            Instance = this;
            _mapGenerator = new();

            _levelsSwitcher = GetComponent<LevelsSwitcher>();
            _wavesSwitcher = GetComponent<WavesSwitcher>();
        }
    }
}