using System;
using TMPro;
using UnityEngine;

namespace Game
{
    public class CoinBalanceUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinText;

        private int _currentCoinAmount;

        private void Start()
        {
            _currentCoinAmount = 120;
            UpdateCoinDisplay();
        }

        public int GetCoinAmount() => _currentCoinAmount;

        public void ChangeCoinAmount(int value)
        {
            if (value < 0)
            {
                Debug.LogWarning($"Attempted to add a negative value: {value}");
                return;
            }
            else if (_currentCoinAmount - value < 0)
            {
                _currentCoinAmount = 0;
            }
            else
            {
                _currentCoinAmount -= value;
            }

            UpdateCoinDisplay();
        }

        public void AddCoinAmount(int value)
        {
            if (value < 0)
            {
                Debug.LogWarning($"Attempted to add a negative value: {value}");
                return;
            }

            _currentCoinAmount += value;
            UpdateCoinDisplay();
        }

        private void UpdateCoinDisplay()
        {
            _coinText.text = _currentCoinAmount.ToString();
        }
    }
}
