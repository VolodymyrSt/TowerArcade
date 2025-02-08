using DI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ShopItemUI : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private ShopItemSO _itemConfig;

        [Header("UI")]
        [SerializeField] private Button _buyButton;
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _coinCostAmountText;
        [SerializeField] private TextMeshProUGUI _isBoughtText;

        private int _costAmount;
        private bool _isBought = false;

        private CoinBalanceUI _coinBalanceUI;
        private EventBus _eventBus;

        private SaveSystem _saveSystem;
        private SaveData _saveData;

        public void Init(CoinBalanceUI coinBalanceUI, EventBus eventBus, SaveSystem saveSystem, SaveData saveData, MainInventoryContainer mainInventoryContainer)
        {
            _coinBalanceUI = coinBalanceUI;
            _eventBus = eventBus;

            _saveSystem = saveSystem;
            _saveData = saveData;

            _itemImage.sprite = _itemConfig.Sprite;
            _costAmount = _itemConfig.Cost;
            _coinCostAmountText.text = _itemConfig.Cost.ToString();

            if (_saveData.ShopItems.TryGetValue(_itemConfig.Name, out bool isBought))
            {
                _isBought = isBought;

                mainInventoryContainer.AddItemToSlot(_itemConfig.InventoryItem);
            }
            else
            {
                _saveData.ShopItems[_itemConfig.Name] = _isBought;
            }

            UpdateGood();
        }

        private void TryToBuy()
        {
            if (_costAmount <= _coinBalanceUI.GetCoinBalance())
            {
                Buy();
            }
        }

        private void Buy()
        {
            _isBought = true;
            UpdateGood();

            _eventBus.Invoke<OnCoinBalanceChangedSignal>(new OnCoinBalanceChangedSignal(_costAmount));
            _eventBus.Invoke<OnItemBoughtSignal>(new OnItemBoughtSignal(_itemConfig.InventoryItem));

            _saveData.ShopItems[_itemConfig.Name] = true;
            _saveSystem.Save(_saveData);
        }

        private void UpdateGood()
        {
            if (_isBought)
            {
                _buyButton.gameObject.SetActive(false);
                _isBoughtText.gameObject.SetActive(true);
                _isBoughtText.text = "Bought";
            }
            else
            {
                _buyButton.gameObject.SetActive(true);
                _isBoughtText.gameObject.SetActive(false);
                _buyButton.onClick.AddListener(() => Buy());
            }
        }
    }
}
