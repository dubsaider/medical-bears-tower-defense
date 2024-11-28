using Code.Entities.Map;
using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Core.BuildMode
{
    public class TowerBuildHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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

        public void Upgrade()
        {
            _tower.UpgradeLevel();
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
        
        private void Awake()
        {
            _tower = GetComponent<Tower>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }
    }
}