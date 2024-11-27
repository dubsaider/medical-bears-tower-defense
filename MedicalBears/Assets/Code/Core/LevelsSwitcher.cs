using Code.Entities;
using UnityEngine;

namespace Code.Core
{
    public class LevelsSwitcher : MonoBehaviour
    {
        public Level CurrentLevel { get; private set; }

        [SerializeField] private Level[] levels;
        private int _currentLevelIndex;

        public void Init(int levelIndex)
        {
            _currentLevelIndex = levelIndex;
            CurrentLevel = levels[_currentLevelIndex];
        }
        
        public void Switch()
        {
            if (CurrentLevel)
                _currentLevelIndex++;
            
            if (_currentLevelIndex == levels.Length)
                return;

            CurrentLevel = levels[_currentLevelIndex];
        }

        private void OnLevelPassed()
        {
            SaveLoadHandler.Save(_currentLevelIndex);
        }

        private void Awake()
        {
            CoreEventsProvider.LevelPassed += OnLevelPassed;
        }
    }
}