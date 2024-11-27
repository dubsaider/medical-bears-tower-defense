
using System;

namespace Code.Core
{
    /// <summary>
    /// Поставщик событий клетки
    /// </summary>
    public static class CellEventsProvider
    {
        /// <summary>
        /// Событие убрать из счётчику заражения карты
        /// </summary>
        public static Action<int> AddValueToCorruptionLevel;
        /// <summary>
        /// Событие заражения клетки
        /// </summary>
        public static Action<MapCell> CellWasCorrupted;
        
        /// <summary>
        /// Событие полного излечения клетки
        /// </summary>
        public static Action<MapCell> CellWasHealed;
    }
}