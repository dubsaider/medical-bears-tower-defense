using Code.Core.BuildMode;
using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Entities.Map
{
    public class CellHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private MapCell _cell;

        private int _maxCorupLevel = 1;
        private Corruption _corup;

        private bool _isEmpty = true;
        private SpriteRenderer _spriteRenderer;
        private bool _isBorder => _cell.Type is MapCellType.Border;
        private bool _isAvailable => _isEmpty && !_isBorder;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetCell(MapCell cell)
        {
            _cell = cell;
            _corup = new Corruption()
            {
                corruptionLevel = 0
            };
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

        public float GetX()
        {
            return _cell.X;
        }
        public float GetY()
        {
            return _cell.Y;
        }

        public int GetCoruptionLevel()
        {
            return _corup.corruptionLevel;
        }
        public void CorruptionLevelUp()
        {
            if (_corup != null)
            {
                if (_corup.corruptionLevel < _maxCorupLevel)
                {
                    _corup.corruptionLevel++;
                    ApplyCorruptionChanges();
                }
            }
        }

        public void CorruptionLevelDown()
        {
            if (_corup != null)
            {
                if (_corup.corruptionLevel >0)
                {
                    _corup.corruptionLevel--;
                    ApplyCorruptionChanges();
                }
            }
        }

        private void ApplyCorruptionChanges()
        {
            if (_corup.corruptionLevel > 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.3f, 0f, 0.3f);
            }
        }
    }
}