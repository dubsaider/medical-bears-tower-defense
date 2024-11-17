
namespace Extensions
{
    /// <summary>
    /// Структура времени
    /// </summary>
    public struct TimeOnly
    {
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
        public int TotalSeconds { get; private set; }
        
        public TimeStruct(int minutes, int seconds) : this(MinutesToSeconds(minutes) + seconds) {}
        
        public TimeStruct(int seconds) : this()
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
            TotalSeconds -= seconds;
            UpdateStruct();
        }
        
        public string ToString()
        {
            return $"{Minutes}:{Seconds}";
        }

        private void UpdateStruct()
        {
            var timeStruct = SplitTotalSeconds();
            Minutes = timeStruct.minutes;
            Seconds = timeStruct.seconds;
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