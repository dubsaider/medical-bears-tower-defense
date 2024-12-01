using Code.Core;
using Code.Interfaces;
using UnityEngine;

namespace Code.Entities.Map
{
    // обработчик заражений. в него передаются значения из других классов, таких как CorrupAttack.
    public class CellCorruptionHandler : MonoBehaviour, ICorruptionVictim
    {
        public Corruption Corruption { get; private set; }
        public bool IsHealthy => Corruption.IsHealthy;
        public bool IsMaxCorrupted => Corruption.IsMaxCorrupted;
        
        public SpriteRenderer corruptionSpriteRenderer;

        private MapCell _cell;

        public void Init(MapCell cell)
        {
            _cell = cell;
        }

        // передаёт классу заражения значение, на которое надо увеличить или уменьшить
        // потом идёт обновление ячейки
        public void IncreaseCorruptionLevel(int value)
        {
            Corruption?.IncreaseCorruptionLevel(value);
            CellEventsProvider.CellWasCorrupted(_cell);
        }

        public void DecreaseCorruptionLevel(int value)
        {
            Corruption?.DecreaseCorruptionLevel(value);

            CellEventsProvider.CellWasHealed(_cell);
           
        }

        public int GetCorruptionLevel()
        {
            return (int)Corruption.CorruptionState;
        }
        
        private void Awake()
        {
            Corruption = new();
        }
    }
}