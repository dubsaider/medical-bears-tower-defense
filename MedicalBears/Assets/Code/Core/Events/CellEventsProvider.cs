
using System;

namespace Code.Core
{
    /// <summary>
    /// Поставщик событий клетки
    /// </summary>
    public static class CellEventsProvider
    {
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