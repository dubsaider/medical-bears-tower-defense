using Unity.VisualScripting;

namespace Code.Core.RewardsAndBalance
{
    public class BalanceMediator
    {
        public int CurrentBalance => _balance.Value;
        private int WaveNumber => CoreManager.Instance.CurrentWaveNumber;

        private readonly Balance _balance;

        public BalanceMediator(int startBalance)
        {
            _balance = new(startBalance);
        }

        public void AddKillReward(int killReward)
        {
            _balance.Add(RewardCalculationUtility.CalculateKillReward(killReward, WaveNumber));
        }
        
        public void AddWaveReward(int waveReward = 100)
        {
            _balance.Add(RewardCalculationUtility.CalculateWaveReward(waveReward, 1, WaveNumber)); //todo
        }

        public void Add(int value) => _balance.Add(value);
        public void Substract(int value) => _balance.Subtract(value);
    }
}