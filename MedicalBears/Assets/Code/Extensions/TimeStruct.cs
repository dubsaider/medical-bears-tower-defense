
using System;
using UnityEngine;

namespace Extensions
{
    //TODO пересмотреть логику
    /// <summary>
    /// Структура времени
    /// </summary>
    [Serializable]
    public struct TimeStruct
    {
        public int Minutes => _minutes;
        public int Seconds => _seconds;
        public int TotalSeconds { get; private set; }
        public bool IsNull => IsNullImpl();

        [SerializeField] private int _minutes; 
        [SerializeField] private int _seconds; 
        
        public TimeStruct(int minutes, int seconds) : this(MinutesToSeconds(minutes) + seconds) {}
        
        public TimeStruct(int seconds) : this()
        {
            TotalSeconds = seconds;
            UpdateValues();
        }

        public void AddMinutes(int minutes) => AddSeconds(MinutesToSeconds(minutes));
        
        public void AddSeconds(int seconds)
        {
            TotalSeconds += seconds;
            UpdateValues();
        }
        
        public void SubstractMinutes(int minutes) => SubstractSeconds(MinutesToSeconds(minutes));
        
        public void SubstractSeconds(int seconds)
        {
            if (IsNull)
                return;
            
            TotalSeconds -= seconds;
            UpdateValues();
        }
        
        public override string ToString()
        {
            var mins = _minutes < 10 ? $"0{_minutes}" : _minutes.ToString();
            var secs = _seconds < 10 ? $"0{_seconds}" : _seconds.ToString();
            return $"{mins}:{secs}";
        }

        private void UpdateValues()
        {
            var values = SecondsToMinutes(TotalSeconds);
            _minutes = values.minutes;
            _seconds = values.seconds;
        }

        private bool IsNullImpl()
        {
            if(!IsSynchronized())
                Synchronize();
            
            return TotalSeconds == 0;
        }

        private bool IsSynchronized()
        {
            return GetTotalSecondsFromValues() == TotalSeconds;
        }

        private void Synchronize()
        {
            if (TotalSeconds == 0)
            {
                TotalSeconds = MinutesToSeconds(_minutes) + _seconds;
                return;
            }

            UpdateValues();
        }
        
        private int GetTotalSecondsFromValues()
        {
            return MinutesToSeconds(_minutes) + _seconds;
        }
        
        private static int MinutesToSeconds(int minutes)
        {
            return minutes * 60;
        }

        private static (int minutes, int seconds) SecondsToMinutes(int seconds)
        {
            var mins = seconds / 60;
            var secs = seconds % 60;
            
            return new(mins, secs);
        }
    }
}