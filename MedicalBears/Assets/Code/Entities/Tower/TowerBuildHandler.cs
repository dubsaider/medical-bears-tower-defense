using Code.Entities.Map;
using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Core.BuildMode
{
    public class TowerBuildHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        public Tower Tower => _tower;
        public int TowerCost;

        private Tower _tower;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider;

        public void Build(CellHandler cellHandler)
        {
            _tower.SetBuildStatus(true);
            _spriteRenderer.color = Colors.ColorWithModifiedAlpha(Colors.white, 1f);

            var sellComponent = gameObject.AddComponent<TowerSellHandler>();
            sellComponent.Init(cellHandler, TowerCost * _tower.Level, _spriteRenderer);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(_tower.IsBuilded) return;
            
            //TODO сделать проверку на border (не давать туда передвинуться)
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition); 
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(_tower.IsBuilded) return;

            _boxCollider.enabled = false;
            _spriteRenderer.sortingOrder = 100;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            _spriteRenderer.sortingOrder = 30;
            _boxCollider.enabled = true;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            var enteredObj = eventData.pointerDrag;
            if (enteredObj is null || !enteredObj.TryGetComponent(out TowerBuildHandler towerBuildHandler)) 
                return;

            var color = IsCanUpgrade(towerBuildHandler.Tower) 
                ? Colors.lightBlue 
                : Colors.lightRed;
            
            _spriteRenderer.color = color;
            color = Colors.ColorWithModifiedAlpha(color, 0.7f);
            enteredObj.GetComponent<SpriteRenderer>().color = color;
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_tower.IsBuilded)
                _spriteRenderer.color = Colors.ColorWithModifiedAlpha(Colors.white, 1f);
        }

        public void OnDrop(PointerEventData eventData)
        {
            TryMergeTower(eventData);
        }

        private void TryMergeTower(PointerEventData eventData)
        {
            var droppedTower = eventData.pointerDrag;
            if (droppedTower is null || !droppedTower.TryGetComponent(out TowerBuildHandler towerBuildHandler))
                return;

            if (!IsCanUpgrade(towerBuildHandler.Tower))
                return;
            
            _tower.UpgradeLevel();
            Destroy(droppedTower);
            
            _spriteRenderer.color = Colors.ColorWithModifiedAlpha(Colors.white, 1f);
            GameModeManager.SetDefaultMode();
        }

        /// <summary>
        /// Проверяем, можно ли апгрейднуть башню
        /// </summary>
        /// <param name="tower">Башня, которую "наводим"</param>
        private bool IsCanUpgrade(Tower tower)
        {
            return _tower.TowerType == tower.TowerType &&
                   !_tower.IsMaxLevel;
        }
        
        private void Awake()
        {
            _tower = GetComponent<Tower>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        
    }
}