using UnityEngine;

namespace Extensions
{
    public static class RotationRandomizer
    {
        /// <summary>
        /// Получение угла, кратного 90 град.
        /// </summary>
        /// <returns></returns>
        public static Quaternion GetRandom90DegreesRotation()
        {
            var multiplier = Random.Range(0, 4);
            
            return new Quaternion
            {
                eulerAngles = new Vector3(0, 0, multiplier * 90)
            };
        }
    }
}