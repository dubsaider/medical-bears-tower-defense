using System;
using Code.Entities;
using UnityEngine;

namespace Code.Core
{
    public class LevelsSwitcher : MonoBehaviour
    {
        public Level CurrentLevel { get; private set; }

        [SerializeField] private Level[] levels;
        private int _currentLevelIndex;

        public void Switch()
        {
            if (CurrentLevel)
                _currentLevelIndex++;
            
            if (_currentLevelIndex == levels.Length)
                return;

            CurrentLevel = levels[_currentLevelIndex];
        }
    }
}