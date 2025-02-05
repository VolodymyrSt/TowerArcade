using DI;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class CoinBalanceUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinText;

        private int _currentCoinBalance;

        public void Init(EventBus eventBus)
        {
            eventBus.SubscribeEvent<OnCoinBalanceChangedSignal>(ChangeCoinBalance);

            _currentCoinBalance = 120;
            UpdateCoinDisplay();
        }

        public int GetCoinBalance() => _currentCoinBalance;

        public void ChangeCoinBalance(OnCoinBalanceChangedSignal signal)
        {
            if (signal.Value < 0)
            {
                Debug.LogWarning($"Attempted to add a negative value: {signal.Value}");
                return;
            }
            else if (_currentCoinBalance - signal.Value < 0)
            {
                _currentCoinBalance = 0;
            }
            else
            {
                _currentCoinBalance -= signal.Value;
            }

            UpdateCoinDisplay();
        }

        public void AddCoinBalace(int value)
        {
            if (value < 0)
            {
                Debug.LogWarning($"Attempted to add a negative value: {value}");
                return;
            }

            _currentCoinBalance += value;
            UpdateCoinDisplay();
        }

        private void UpdateCoinDisplay() => _coinText.text = _currentCoinBalance.ToString();
    }
}
