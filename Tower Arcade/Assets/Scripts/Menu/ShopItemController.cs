using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ShopItemController : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private TowerSO _tower;
        [SerializeField] private ShopItemSO _itemConfig;

        [Header("UI")]
        [SerializeField] private Button _buyButton;
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _coinCostAmountText;
        [SerializeField] private TextMeshProUGUI _isBoughtText;

        private int _costAmount;
        private bool _isBought = false;

        private CoinBalanceUI _coinBalanceUI;

        public void Init(CoinBalanceUI coinBalanceUI)
        {
            _coinBalanceUI = coinBalanceUI;

            _itemImage.sprite = _itemConfig.Sprite;
            _costAmount = _itemConfig.Cost;
            _coinCostAmountText.text = _itemConfig.Cost.ToString();

            UpdateGood();
        }

        private void TryToBuy()
        {
            if (_costAmount >= _coinBalanceUI.GetCoinAmount())
            {
                Buy();
            }
        }

        private void Buy()
        {
            _isBought = true;
            UpdateGood();
            _coinBalanceUI.ChangeCoinAmount(_costAmount);
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
