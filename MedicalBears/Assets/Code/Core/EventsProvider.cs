using System;

namespace Code.Core
{
    public static class EventsProvider
    {
        /// <summary>
        /// Уровень запущен (инициализирован)
        /// </summary>
        public static Action LevelStarted;
        
        /// <summary>
        /// Таймер перед началом волны истек (волна началась)
        /// </summary>
        public static Action WaveStarted;

        /// <summary>
        /// Все враги волны были заспавлены
        /// </summary>
        public static Action AllWaveEnemiesSpawned;

        /// <summary>
        /// Все враги волны поражены
        /// </summary>
        public static Action AllWaveEnemiesDied;


        public static void UnsubscribeAll()
        {

        }


    }
}