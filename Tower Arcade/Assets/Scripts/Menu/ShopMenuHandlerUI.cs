using DI;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ShopMenuHandlerUI : MonoBehaviour
    {
        [Header("Root")]
        [SerializeField] private GameObject _shopMenuRoot;

        [Header("Buttons")]
        [SerializeField] private Button _openShopButton;
        [SerializeField] private Button _goHomeButton;

        [SerializeField] private TextMeshProUGUI _coinsAmountText;

        [SerializeField] private List<ShopItemController> _shopItems = new();

        private void Start()
        {
            var coinBalanceUI = MenuRegistrator.Resolve<CoinBalanceUI>();

            _openShopButton.onClick.AddListener(() => OpenShopMenu());
            _goHomeButton.onClick.AddListener(() => CloseShopMenu());

            _coinsAmountText.text = coinBalanceUI.GetCoinAmount().ToString();

            foreach (var item in _shopItems)
            {
                item.Init(coinBalanceUI);
            }

            CloseShopMenu();
        }

        private void OpenShopMenu() =>_shopMenuRoot.SetActive(true);
        private void CloseShopMenu() =>_shopMenuRoot.SetActive(false);
    }
}
