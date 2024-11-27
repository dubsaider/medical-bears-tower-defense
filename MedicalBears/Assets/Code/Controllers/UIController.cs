using System;
using Code.Core;
using Code.Render;
using UnityEngine;

namespace Code.Controllers
{
    public class UIController : MonoBehaviour
    {
        private UIRenderer _uiRenderer;

        public void OpenMainMenu()
        {
            _uiRenderer.ShowMainMenuUI();
            _uiRenderer.HideGameUI();
        }
        
        public void OpenPauseMenu()
        {
            Time.timeScale = 0;
            _uiRenderer.ShowPauseMenu();
        }
        
        public void ClosePauseMenu()
        {
            Time.timeScale = 1;
            _uiRenderer.HidePauseMenu();
        }

        public void NextLevel()
        {
            _uiRenderer.HideVictoryMenu();
            CoreManager.Instance.NextLevel();
        }

        public void RestartLevel()
        {
            _uiRenderer.HideDefeatMenu();
            CoreManager.Instance.RestartLevel();
        }

        public void StartNewGame()
        {
            _uiRenderer.HidePauseMenu();
            CoreManager.Instance.StartNewGame();
        }
        
        public void ContinueGame()
        {
            _uiRenderer.HidePauseMenu();
            CoreManager.Instance.ContinueGame();
        }

        private void OnVictory()
        {
            _uiRenderer.ShowVictoryMenu();
        }
        
        private void OnDefeat()
        {
            _uiRenderer.ShowDefeatMenu();
        }
        
        private void Awake()
        {
            _uiRenderer = GetComponent<UIRenderer>();

            CoreEventsProvider.LevelPassed += OnVictory;
            CoreEventsProvider.CriticalCorruptionReached += OnDefeat;
        }
    }
}