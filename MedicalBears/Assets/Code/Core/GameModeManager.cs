using System;
using Code.Enums;

namespace Code.Core
{
    public static class GameModeManager
    {
        public static event Action<GameMode> GameModeChanged;
        public static GameMode CurrentGameMode { get; private set; }

        public static void SetNullMode() => SetMode(GameMode.NotInGame);
        public static void SetDefaultMode() => SetMode(GameMode.Default);
        public static void SetBuildMode() => SetMode(GameMode.BuildMode);
        public static void SetSellMode() => SetMode(GameMode.SellMode);
        private static void SetMode(GameMode newGameMode)
        {
            CurrentGameMode = newGameMode;
            GameModeChanged?.Invoke(CurrentGameMode);
        }

        static GameModeManager()
        {
            CoreEventsProvider.LevelStarted += SetDefaultMode;

            CoreEventsProvider.LevelPassed += SetNullMode;
            CoreEventsProvider.LevelNotPassed += SetNullMode;
        }
    }
}