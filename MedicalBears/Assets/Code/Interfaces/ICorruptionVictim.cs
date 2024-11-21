using System;
using Code.Enums;

namespace Code.Interfaces
{
    /// <summary>
    /// Интерфейс "жертвы" заражения (любой объект, который может быть заражен)
    /// </summary>
    public interface ICorruptionVictim
    {
        public Corruption Corruption { get; }

        public void IncreaseCorruptionLevel(int value);

        public void DecreaseCorruptionLevel(int value);

        public void RefreshCorruptionView();
    }
}