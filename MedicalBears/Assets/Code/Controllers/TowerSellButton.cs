using System;
using Code.Core;
using Code.Enums;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Controllers
{
    public class TowerSellButton : MonoBehaviour
    {
        private GameMode CurrentGameMode => GameModeManager.CurrentGameMode;
        private Button _button; 
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClick);

            GameModeManager.GameModeChanged += OnChangeGameMode;
        }
        
        private void OnButtonClick()
        {
            switch (CurrentGameMode)
            {
                case GameMode.Default:
                    GameModeManager.SetSellMode();
                    break;
                case GameMode.SellMode:
                    GameModeManager.SetDefaultMode();
                    break;
            }
        }
        
        private void OnChangeGameMode(GameMode gameMode)
        {
            _button.interactable = true;

            switch (CurrentGameMode)
            {
                case GameMode.Default:
                    _button.image.color = Colors.white;
                    break;
                case GameMode.SellMode:
                    _button.image.color = Colors.lightRed;
                    break;
                default:
                    _button.interactable = false;
                    break;
            }
        }
    }
}