using Code.Core;
using Code.Entities;
using Code.Enums;
using Code.Render;
using UnityEngine;

namespace Code.Controllers
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        
        [SerializeField] private DialogueManager _dialogueManager;

        private UIRenderer _uiRenderer;

        public void OpenMainMenu()
        {
            if (GameModeManager.CurrentGameMode != GameMode.NotInGame) 
                CoreManager.Instance.FinishLevelManually();
            
            _uiRenderer.ShowMainMenuUI();
            _uiRenderer.HideGameUI();
            Time.timeScale = 1;
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

        public void StartSelectedLevel(int index)
        {
            _uiRenderer.HideMainMenuUI();
            _uiRenderer.ShowGameUI();
            CoreManager.Instance.StartSelectedLevel(index);
        }

        public void RestartLevel()
        {
            _uiRenderer.HideDefeatMenu();
            CoreManager.Instance.RestartLevel();
        }

        public void StartNewGame()
        {
            _uiRenderer.HideMainMenuUI();
            _uiRenderer.ShowGameUI();
            CoreManager.Instance.StartNewGame();
        }
        
        public void ContinueGame()
        {
            _uiRenderer.HideMainMenuUI();
            _uiRenderer.ShowGameUI();
            CoreManager.Instance.ContinueGame();
        }
        
        public void StartNovel(DialogueSession dialogueSession)
        {
            _uiRenderer.ShowNovelUI();
            _dialogueManager.Init(dialogueSession);
        }

        private void EndNovel()
        {
            _uiRenderer.HideNovelUI();
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
            Instance = this;
            _uiRenderer = GetComponent<UIRenderer>();

            CoreEventsProvider.LevelPassed += OnVictory;
            CoreEventsProvider.LevelNotPassed += OnDefeat;

            CoreEventsProvider.NovelFinished += EndNovel;
        }
    }
}