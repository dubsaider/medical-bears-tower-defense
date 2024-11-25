using Code.Core.BuildMode;
using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Entities.Map
{
    public class CellHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private MapCell _cell;
        private bool _isEmpty = true;
        private SpriteRenderer _spriteRenderer;
        private bool _isBorder => _cell.Type is MapCellType.Border;
        private bool _isAvailable => _isEmpty && !_isBorder;
        
        public void Init(MapCell cell)
        {
            _cell = cell;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            var enteredObj = eventData.pointerDrag;
            if (enteredObj is null) 
                return;

            if (enteredObj.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                if (_isBorder)
                {
                    spriteRenderer.color = Colors.ColorWithModifiedAlpha(Colors.white, 0.7f);
                    return;
                }
                
                var color = Colors.ColorWithModifiedAlpha(_isAvailable
                    ? Colors.lightGreen
                    : Colors.lightRed, 0.7f);
                
                spriteRenderer.color = color;
                _spriteRenderer.color = color;
            }
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            _spriteRenderer.color = Colors.ColorWithModifiedAlpha(Colors.white, 1f);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (!_isAvailable)
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

            towerBuildHandler.Build();
            towerBuildHandler.transform.position = transform.position;

            _spriteRenderer.color = Colors.ColorWithModifiedAlpha(Colors.white, 1f);
            
            _isEmpty = false;
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