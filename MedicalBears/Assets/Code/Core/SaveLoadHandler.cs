using UnityEngine;

namespace Code.Core
{
    public static class SaveLoadHandler
    {
        private const string LevelIndexKey = "LevelIndexKey";

        static SaveLoadHandler()
        {
            if (!PlayerPrefs.HasKey(LevelIndexKey))
                Save(0);
        }

        public static int GetLastPassedLevelIndex()
        {
            return PlayerPrefs.GetInt(LevelIndexKey);
        }
        
        public static void Save(int lastPassedLevelIndex)
        {
            PlayerPrefs.SetInt(LevelIndexKey, lastPassedLevelIndex);
            Save();
        }

        public static void ClearSaves()
        {
            PlayerPrefs.SetInt(LevelIndexKey, 0);
            Save();
        }
        
        private static void Save() => PlayerPrefs.Save();
    }
}