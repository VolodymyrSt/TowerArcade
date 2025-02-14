using TMPro;
using UnityEngine;

namespace Game
{
    public class CoinBalanceUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinText;

        private int _currentCoinBalance = 0;

        private SaveSystem _saveSystem;
        private SaveData _saveData;

        public void Init(EventBus eventBus, SaveSystem saveSystem, SaveData saveData)
        {
            eventBus.SubscribeEvent<OnCoinBalanceChangedSignal>(ChangeCoinBalance);

            _saveSystem = saveSystem;
            _saveData = saveData;

            _currentCoinBalance = _saveSystem.Load().CoinCurrency;

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

            _saveData.CoinCurrency = _currentCoinBalance;
            _saveSystem.Save(_saveData);

            UpdateCoinDisplay();
        }

        public void IncreaseCoinBalace(int value)
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
