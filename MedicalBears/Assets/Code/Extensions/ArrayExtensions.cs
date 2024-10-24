using UnityEngine;

namespace Extensions
{
    public static class ArrayExtensions
    {
        public static T GetRandomItem<T>(this T[] array)
        {
            var index = Random.Range(0, array.Length - 1);
            return array[index];
        }
    }
}