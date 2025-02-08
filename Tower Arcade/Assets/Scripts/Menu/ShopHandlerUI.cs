using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ShopHandlerUI : MonoBehaviour
    {
        [Header("Root")]
        [SerializeField] private GameObject _shopMenuRoot;

        [Header("Buttons")]
        [SerializeField] private Button _openShopButton;
        [SerializeField] private Button _goHomeButton;

        [SerializeField] private TextMeshProUGUI _coinBalanceText;

        [SerializeField] private List<ShopItemUI> _shopItems = new();

        private CoinBalanceUI _coinBalanceUI;

        public void Init(CoinBalanceUI coinBalanceUI, EventBus eventBus, SaveSystem saveSystem, SaveData saveData, MainInventoryContainer mainInventoryContainer)
        {
            _coinBalanceUI = coinBalanceUI;

            eventBus.SubscribeEvent<OnCoinBalanceChangedSignal>(UpdateCoinBalanceText);

            _openShopButton.onClick.AddListener(() => OpenShopMenu());
            _goHomeButton.onClick.AddListener(() => CloseShopMenu());

            _coinBalanceText.text = coinBalanceUI.GetCoinBalance().ToString();

            foreach (var item in _shopItems)
            {
                item.Init(coinBalanceUI, eventBus, saveSystem, saveData, mainInventoryContainer);
            }

            CloseShopMenu();
        }

        private void OpenShopMenu() => _shopMenuRoot.SetActive(true);
        private void CloseShopMenu() => _shopMenuRoot.SetActive(false);

        private void UpdateCoinBalanceText(OnCoinBalanceChangedSignal signal) => _coinBalanceText.text = _coinBalanceUI.GetCoinBalance().ToString();
    }
}
