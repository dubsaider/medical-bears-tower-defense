using System;
using Code.Entities.Map;
using Code.Enums;
using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Core.BuildMode
{
    public class TowerSellHandler : MonoBehaviour, IPointerClickHandler
    {
        private int _towerCost;
        private SpriteRenderer _spriteRenderer;
        private CellHandler _cellHandler;

        public void Init(CellHandler cellHandler, int returnValue, SpriteRenderer spriteRenderer)
        {
            _towerCost = returnValue;
            _spriteRenderer = spriteRenderer;
            _cellHandler = cellHandler;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(GameModeManager.CurrentGameMode is not GameMode.SellMode)
                return;
            
            _cellHandler.OnTowerSell();
            
            CoreManager.Instance.BalanceMediator.Add(_towerCost / 2);
            GameModeManager.SetDefaultMode();
            Destroy(gameObject);
        }

        private void OnGameModeChanged(GameMode gameMode)
        {
            _spriteRenderer.color = gameMode switch
            {
                GameMode.SellMode => Colors.lightGreen,
                _ => Colors.white
            };
        }
        
        private void Awake()
        {
            GameModeManager.GameModeChanged += OnGameModeChanged;
        }

        private void OnDestroy()
        {
            GameModeManager.GameModeChanged -= OnGameModeChanged;
        }
    }
}