using Sound;
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
        [SerializeField] private TextMeshProUGUI _itemName;

        private int _costAmount;
        private bool _isBought = false;

        private CoinBalanceUI _coinBalanceUI;
        private EventBus _eventBus;
        private SoundHandler _soundHandler;
        private MassegeHandlerUI _masegeHandlerUI;

        private SaveSystem _saveSystem;
        private SaveData _saveData;

        public void Init(CoinBalanceUI coinBalanceUI, EventBus eventBus, SoundHandler soundHandler, SaveSystem saveSystem
            , SaveData saveData, MainInventoryContainer mainInventoryContainer, MassegeHandlerUI massegeHandler)
        {
            _coinBalanceUI = coinBalanceUI;
            _eventBus = eventBus;
            _soundHandler = soundHandler;

            _saveSystem = saveSystem;
            _saveData = saveData;

            _masegeHandlerUI = massegeHandler;

            _itemImage.sprite = _itemConfig.Sprite;
            _costAmount = _itemConfig.Cost;
            _coinCostAmountText.text = _itemConfig.Cost.ToString();
            _itemName.text = _itemConfig.Name;

            if (_saveData.ShopItems.TryGetValue(_itemConfig.Name, out bool isBought))
            {
                _isBought = isBought;
            }
            else
            {
                _saveData.ShopItems[_itemConfig.Name] = _isBought;
            }

            if (_isBought)
                mainInventoryContainer.AddItemToSlot(_itemConfig.InventoryItem);

            UpdateGood();
        }

        private void TryToBuy()
        {
            if (_costAmount <= _coinBalanceUI.GetCoinBalance())
            {
                _soundHandler.PlaySound(ClipName.Click);
                Buy();
            }
            else
            {
                _masegeHandlerUI.ShowMassege("You don`t have enough coins");
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
                _buyButton.onClick.AddListener(() => TryToBuy());
            }
        }

        public ShopItemSO GetItemConfig() => _itemConfig;
    }
}
