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

        public bool IsEmpty = false;
        private bool IsBorder => _cell.Type is MapCellType.Border;
        
        private SpriteRenderer _spriteRenderer;

        public void Init(MapCell cell)
        {
            _cell = cell;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            var enteredObj = eventData.pointerDrag;
            if (enteredObj is null || !enteredObj.TryGetComponent(out TowerBuildHandler towerBuildHandler)) 
                return;

            var color = Colors.lightRed;

            if (!IsBorder)
            {
                if (IsEmpty)
                    color = Colors.lightGreen;
                else if (IsCanUpgrade(towerBuildHandler.Tower))
                    color = Colors.lightBlue;
                _spriteRenderer.color = color;
            }

            color = Colors.ColorWithModifiedAlpha(color, 0.7f);
            enteredObj.GetComponent<SpriteRenderer>().color = color;
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            _spriteRenderer.color = Colors.ColorWithModifiedAlpha(Colors.white, 1f);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (IsBorder)
                return;
            
            if(TryBuildOrMergeTower(eventData))
                return;
            
            //Здесь можно продолжить обрабатывать дропы других объектов
        }

        private bool TryBuildOrMergeTower(PointerEventData eventData)
        {
            var droppedTower = eventData.pointerDrag;
            if (droppedTower is null || !droppedTower.TryGetComponent(out TowerBuildHandler towerBuildHandler))
                return false;

            var tower = towerBuildHandler.Tower;
            
            if (IsEmpty)
            {
                _tower = tower;
                towerBuildHandler.Build();
                towerBuildHandler.transform.position = transform.position;
            }
            else if (IsCanUpgrade(tower))
            {
                _tower.UpgradeLevel();
                Destroy(droppedTower);
            }

            _spriteRenderer.color = Colors.ColorWithModifiedAlpha(Colors.white, 1f);
            return true;
        }

        public MapCellType GetMapCellType()
        {
            return _cell.Type;
        }

        /// <summary>
        /// Проверяем, можно ли апгрейднуть уже стоящую башню
        /// </summary>
        /// <param name="tower">Башня, которую планируем построить</param>
        private bool IsCanUpgrade(Tower tower)
        {
            return !IsEmpty && _tower.TowerType == tower.TowerType;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}