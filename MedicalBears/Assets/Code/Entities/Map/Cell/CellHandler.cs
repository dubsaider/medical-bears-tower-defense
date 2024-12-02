using Code.Core;
using Code.Core.BuildMode;
using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Entities.Map
{
    public class CellHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private MapCell _cell;
        private Tower _tower;
        
        public bool IsEmpty => !_tower && !IsUnitStayOnCell;
        public bool IsUnitStayOnCell = false;
        private bool IsBorder => _cell.Type is MapCellType.Border;
        
        private SpriteRenderer _spriteRenderer;

        public void Init(MapCell cell)
        {
            _cell = cell;
        }

        public void OnTowerSell()
        {
            _tower = null;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsEmpty)
                return;
            
            var enteredObj = eventData.pointerDrag;
            if (enteredObj is null || !enteredObj.TryGetComponent(out TowerBuildHandler towerBuildHandler)) 
                return;
            
            if(towerBuildHandler.Tower.IsBuilded)
                return;

            var color = IsBorder 
                ? Colors.lightRed
                : Colors.lightGreen;

            _spriteRenderer.color = color;
            color = Colors.ColorWithModifiedAlpha(color, 0.7f);
            enteredObj.GetComponent<SpriteRenderer>().color = color;
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            _spriteRenderer.color = Colors.ColorWithModifiedAlpha(Colors.white, 1f);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (IsBorder || !IsEmpty)
                return;
            
            if(TryBuildTower(eventData))
                return;
            
            //Здесь можно продолжить обрабатывать дропы других объектов
        }

        private bool TryBuildTower(PointerEventData eventData)
        {
            var droppedTower = eventData.pointerDrag;
            if (droppedTower is null || !droppedTower.TryGetComponent(out TowerBuildHandler towerBuildHandler))
                return false;
            
            var tower = towerBuildHandler.Tower;
            if(tower.IsBuilded)
                return false;
            
            if (IsEmpty)
            {
                _tower = tower;
                towerBuildHandler.Build(this);
                towerBuildHandler.transform.position = transform.position;
            }

            _spriteRenderer.color = Colors.ColorWithModifiedAlpha(Colors.white, 1f);
            GameModeManager.SetDefaultMode();
            return true;
        }

        public MapCellType GetMapCellType()
        {
            return _cell.Type;
        }

        

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}