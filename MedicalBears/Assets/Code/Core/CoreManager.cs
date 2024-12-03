using System;
using Code.Controllers;
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
            InitCurrentLevel();
        }
        
        public void ContinueGame()
        {
            var index = SaveLoadHandler.GetLastPassedLevelIndex();
            if (index < 0)
                index++;
            
            _levelsSwitcher.Init(index);
            InitCurrentLevel();
        }

        public void StartSelectedLevel(int selectedLevelIndex)
        {
            _levelsSwitcher.Init(selectedLevelIndex);
            InitCurrentLevel();
        }
        
        public void NextLevel()
        {
            _levelsSwitcher.Switch();
            InitCurrentLevel();
        }
        
        public void RestartLevel()
        {
            InitCurrentLevel(true);
        }

        public void FinishLevelManually()
        {
            CoreEventsProvider.LevelNotPassed.Invoke();
        }

        private void InitCurrentLevel(bool skipNovel = false)
        {
            Map = _mapGenerator.Generate(CurrentLevel.mapIndex);
            _sceneRenderer.Render(Map);

            BalanceMediator = new(CurrentLevel.startBalance);
            
            if (!skipNovel && CurrentLevel.dialogueSession)
            {
                UIController.Instance.StartNovel(CurrentLevel.dialogueSession);
                return;
            }
            
            StartCurrentLevel();
        }

        private void StartCurrentLevel()
        {
            CoreEventsProvider.LevelStarted.Invoke();
            GameModeManager.SetDefaultMode();
        }
        
        private void Awake()
        {
            Instance = this;
            _mapGenerator = new();

            _levelsSwitcher = GetComponent<LevelsSwitcher>();
            _wavesSwitcher = GetComponent<WavesSwitcher>();

            CoreEventsProvider.NovelFinished += StartCurrentLevel;
        }
    }
}