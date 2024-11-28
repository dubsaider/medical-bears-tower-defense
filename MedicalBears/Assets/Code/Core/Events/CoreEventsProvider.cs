using System;

namespace Code.Core
{
    public static class CoreEventsProvider
    {
        /// <summary>
        /// Уровень запущен (инициализирован)
        /// </summary>
        public static Action LevelStarted;
        
        /// <summary>
        /// Уровень пройден (по сути - победа)
        /// </summary>
        public static Action LevelPassed;

        /// <summary>
        /// Волна переключилась
        /// </summary>
        public static Action<int> WaveSwitched;
        
        /// <summary>
        /// Таймер начала волны обновлен
        /// </summary>
        public static Action<string> WaveTimerUpdated;
        
        /// <summary>
        /// Таймер перед началом волны истек (волна началась)
        /// </summary>
        public static Action WaveStarted;
        
        /// <summary>
        /// Обновилось значение общего заражения карты
        /// </summary>
        public static Action<float> TotalCorruptionValueUpdated;

        /// <summary>
        /// Все враги волны были заспавлены
        /// </summary>
        public static Action AllWaveEnemiesSpawned;

        /// <summary>
        /// Все враги волны поражены
        /// </summary>
        public static Action AllWaveEnemiesDied;
        
        /// <summary>
        /// Изменился баланс
        /// </summary>
        public static Action<int> BalanceChanged; //TODO к нему подписать UI магазина

        /// <summary>
        /// Достигнуто критическое заражение (по сути - поражение)
        /// </summary>
        public static Action CriticalCorruptionReached;

        /// <summary>
        /// смерть пилюли
        /// </summary>
        public static Action<int> HealerUnitHasDie;

        public static void UnsubscribeAll()
        {

        }


    }
}