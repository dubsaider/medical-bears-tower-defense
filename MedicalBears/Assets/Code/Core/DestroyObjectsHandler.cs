using System.Collections.Generic;
using UnityEngine;

namespace Code.Core
{
    public static class DestroyObjectsHandler
    {
        private static readonly List<GameObject> Objects = new(128);
        
        public static void Add(GameObject obj) => Objects.Add(obj);

        private static void Destroy()
        {
            foreach (var obj in Objects)
                Object.Destroy(obj);
            
            Objects.Clear();
        }
        
        static DestroyObjectsHandler()
        {
            CoreEventsProvider.LevelPassed += Destroy;
            CoreEventsProvider.LevelNotPassed += Destroy;
        }
    }
}