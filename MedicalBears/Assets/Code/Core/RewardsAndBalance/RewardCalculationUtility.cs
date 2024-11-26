namespace Code.Core.RewardsAndBalance
{
    public static class RewardCalculationUtility
    {
        public static int CalculateKillReward(int baseKillReward, int waveNumber)
        {
            return baseKillReward + waveNumber; //todo
        }
        
        public static int CalculateWaveReward(int baseWaveReward, int healTowersCount, int waveNumber)
        {
            return baseWaveReward 
                   + baseWaveReward * healTowersCount 
                   + 50 * waveNumber;
        }
    }
}