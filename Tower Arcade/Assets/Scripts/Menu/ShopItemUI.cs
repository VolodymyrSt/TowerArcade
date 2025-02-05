using TMPro;
using Unity.VisualScripting;
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

        public void Init(CoinBalanceUI coinBalanceUI, EventBus eventBus)
        {
            _coinBalanceUI = coinBalanceUI;
            _eventBus = eventBus;

            _itemImage.sprite = _itemConfig.Sprite;
            _costAmount = _itemConfig.Cost;
            _coinCostAmountText.text = _itemConfig.Cost.ToString();

            UpdateGood();
        }

        private void TryToBuy()
        {
            if (_costAmount >= _coinBalanceUI.GetCoinBalance())
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

            //Addtoinventory()
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
