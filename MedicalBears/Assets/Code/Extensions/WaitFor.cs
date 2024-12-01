using UnityEngine;

namespace Extensions
{
    public static class WaitFor
    {
        /// <summary>
        /// 0.1 сек
        /// </summary>
        public static WaitForSeconds Seconds01 = new WaitForSeconds(.1f);
        
        public static WaitForSeconds Seconds1 = new WaitForSeconds(1f);
        public static WaitForSeconds Seconds2 = new WaitForSeconds(2f);
        public static WaitForSeconds Seconds3 = new WaitForSeconds(2f);
    }
}