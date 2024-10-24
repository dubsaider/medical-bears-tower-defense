using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Core.BuildMode.BuildMode
{
    public class TowerBuildHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Tower _tower;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider;
        private void Awake()
        {
            _tower = GetComponent<Tower>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        public void Build()
        {
            _tower.SetBuildStatus(true);
            _spriteRenderer.color = Colors.ColorWithModifiedAlpha(Colors.white, 1f);
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
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            if(_tower.IsBuilded) return;

            _boxCollider.enabled = true;
        }
    }
}