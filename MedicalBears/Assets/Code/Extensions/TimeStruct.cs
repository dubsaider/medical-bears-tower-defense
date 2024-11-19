
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Extensions
{
    /// <summary>
    /// Структура времени
    /// </summary>
    [Serializable]
    public class TimeStruct
    {
        public int Minutes => _minutes;
        public int Seconds => _seconds;
        public int TotalSeconds { get; private set; }
        public bool IsNull => TotalSeconds == 0;

        [SerializeField] private int _minutes; 
        [SerializeField] private int _seconds; 
        
        public TimeStruct(int minutes, int seconds) : this(MinutesToSeconds(minutes) + seconds) {}
        
        public TimeStruct(int seconds)
        {
            TotalSeconds = seconds;
            UpdateStruct();
        }

        public void AddMinutes(int minutes) => AddSeconds(MinutesToSeconds(minutes));
        
        public void AddSeconds(int seconds)
        {
            TotalSeconds += seconds;
            UpdateStruct();
        }
        
        public void SubstractMinutes(int minutes) => SubstractSeconds(MinutesToSeconds(minutes));
        
        public void SubstractSeconds(int seconds)
        {
            if(IsNull)
                return;
            
            TotalSeconds -= seconds;
            UpdateStruct();
        }
        
        public override string ToString()
        {
            var mins = _minutes < 10 ? $"0{_minutes}" : _minutes.ToString();
            var secs = _seconds < 10 ? $"0{_seconds}" : _seconds.ToString();
            return $"{mins}:{secs}";
        }

        private void UpdateStruct()
        {
            var timeStruct = SplitTotalSeconds();
            _minutes = timeStruct.minutes;
            _seconds = timeStruct.seconds;
        }
        
        private static int MinutesToSeconds(int minutes)
        {
            return minutes * 60;
        }
        
        private (int minutes, int seconds) SplitTotalSeconds()
        {
            var mins = TotalSeconds / 60;
            var secs = TotalSeconds % 60;
            
            return new(mins, secs);
        }
    }
}