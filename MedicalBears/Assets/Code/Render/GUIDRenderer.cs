using System;
using Code.Core;
using TMPro;
using UnityEngine;

namespace Code.Render
{
    public class GUIDRenderer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private TextMeshProUGUI _timerText;

        private void UpdateBalanceText(int balance)
        {
            _balanceText.text = balance.ToString();
        }
        
        private void UpdateTimerText(string text)
        {
            _timerText.text = text;
        }
        
        private void ClearTimerText()
        {
            _timerText.text = "";
        }
        
        private void Awake()
        {
            CoreEventsProvider.WaveTimerUpdated += UpdateTimerText;
            CoreEventsProvider.WaveStarted += ClearTimerText;

            CoreEventsProvider.BalanceChanged += UpdateBalanceText;
        }
    }
}