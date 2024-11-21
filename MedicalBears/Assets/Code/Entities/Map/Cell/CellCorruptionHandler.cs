using Code.Enums;
using Code.Interfaces;
using UnityEngine;

namespace Code.Entities.Map
{
    public class CellCorruptionHandler : MonoBehaviour, ICorruptionVictim
    {
        public Corruption Corruption { get; private set; }
        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Corruption = new();
        }

        public void IncreaseCorruptionLevel(int value)
        {
            Corruption?.IncreaseCorruptionLevel(value);
            RefreshCorruptionView();
        }

        public void DecreaseCorruptionLevel(int value)
        {
            Corruption?.DecreaseCorruptionLevel(value);
            RefreshCorruptionView();
        }

        public void RefreshCorruptionView()
        {
            _spriteRenderer.color = Corruption.CorruptionState switch
            {
                CorruptionState.NotCorrupted => new Color(1, 1, 1),
                CorruptionState.Level1 => new Color(0.9f, 0, 0.9f),
                CorruptionState.Level2 => new Color(0.8f, 0, 0.8f),
                CorruptionState.Level3 => new Color(0.7f, 0, 0.7f),
                CorruptionState.Level4 => new Color(0.6f, 0, 0.6f),
                CorruptionState.Level5 => new Color(0.5f, 0, 0.5f),
                _ => _spriteRenderer.color
            };
        }
    }
}