using Code.Core;
using Code.Enums;
using Code.Interfaces;
using UnityEngine;

namespace Code.Entities.Map
{
    // обработчик заражений. в него передаются значения из других классов, таких как CorrupAttack.
    public class CellCorruptionHandler : MonoBehaviour, ICorruptionVictim
    {
        public Corruption Corruption { get; private set; }
        
        private MapCell _cell;
        private SpriteRenderer _spriteRenderer;

        public void Init(MapCell cell)
        {
            _cell = cell;
        }

        // передаёт классу заражения значение, на которое надо увеличить или уменьшить
        // потом идёт обновление ячейки
        public void IncreaseCorruptionLevel(int value)
        {
            Corruption?.IncreaseCorruptionLevel(value);
            RefreshCorruptionView();

            CellEventsProvider.CellWasCorrupted(_cell);
        }

        public void DecreaseCorruptionLevel(int value)
        {
            Corruption?.DecreaseCorruptionLevel(value);
            RefreshCorruptionView();

            if (Corruption is { IsHealthy: true })
                CellEventsProvider.CellWasHealed(_cell);
        }

        public int GetCorruptionLevel()
        {
            return (int)Corruption.CorruptionState;
        }

        public void RefreshCorruptionView()
        {
            _spriteRenderer.color = Corruption.CorruptionState switch
            {
                CorruptionState.NotCorrupted => new Color(1, 1, 1),
                CorruptionState.Level1 => new Color(0.9f, 0.8f, 0.9f),
                CorruptionState.Level2 => new Color(0.8f, 0.6f, 0.8f),
                CorruptionState.Level3 => new Color(0.7f, 0.4f, 0.7f),
                CorruptionState.Level4 => new Color(0.6f, 0.2f, 0.6f),
                CorruptionState.Level5 => new Color(0.5f, 0, 0.5f),
                _ => _spriteRenderer.color
            };
        }
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Corruption = new();
        }
    }
}