using System;

namespace Code.Core.BuildMode
{
    public static class GameModeManager
    {
        public static GameMode GameMode { get; private set; }
        public static Action GameModeChanged;

        private static CoreManager CoreManager => CoreManager.Instance;

        public static void StartBuilding()
        {
            SetGameMode(GameMode.Building);
        }

        private static void SetGameMode(GameMode gameMode)
        {
            GameMode = gameMode;
            GameModeChanged.Invoke();
        }
    }
}