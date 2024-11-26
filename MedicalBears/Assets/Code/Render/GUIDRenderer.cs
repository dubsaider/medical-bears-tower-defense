using System;
using Code.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Render
{
    public class GUIDRenderer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private TextMeshProUGUI _waveText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Slider _corruptionSlider;

        private void UpdateBalanceText(int balance)
        {
            _balanceText.text = balance.ToString();
        }
        
        private void UpdateTimerText(string text)
        {
            _timerText.text = text;
        }
        
        private void UpdateWaveText(int waveNumber)
        {
            _waveText.text = $"ВОЛНА {waveNumber}";
        }

        private void UpdateCorruptionSlider(float value)
        {
            _corruptionSlider.value = value;
        }
        
        private void ClearTimerText()
        {
            _timerText.text = "";
        }
        
        private void DropCorruptionSlider()
        {
            _corruptionSlider.value = 0;
        }
        
        private void Awake()
        {
            CoreEventsProvider.LevelStarted += DropCorruptionSlider;
            
            CoreEventsProvider.WaveSwitched += UpdateWaveText;
            CoreEventsProvider.WaveTimerUpdated += UpdateTimerText;
            CoreEventsProvider.WaveStarted += ClearTimerText;

            CoreEventsProvider.TotalCorruptionValueUpdated += UpdateCorruptionSlider;

            CoreEventsProvider.BalanceChanged += UpdateBalanceText;
        }
    }
}