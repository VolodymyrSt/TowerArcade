using Sound;
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

        public void Init(CoinBalanceUI coinBalanceUI, EventBus eventBus, MenuSoundHandler soundHandler, SaveSystem saveSystem, SaveData saveData, MainInventoryContainer mainInventoryContainer)
        {
            _coinBalanceUI = coinBalanceUI;

            eventBus.SubscribeEvent<OnCoinBalanceChangedSignal>(UpdateCoinBalanceText);

            _openShopButton.onClick.AddListener(() => OpenShopMenu(soundHandler));
            _goHomeButton.onClick.AddListener(() => CloseShopMenu(soundHandler));

            _coinBalanceText.text = coinBalanceUI.GetCoinBalance().ToString();

            foreach (var item in _shopItems)
            {
                item.Init(coinBalanceUI, eventBus, soundHandler, saveSystem, saveData, mainInventoryContainer);
            }

            _shopMenuRoot.SetActive(false);
        }

        private void OpenShopMenu(MenuSoundHandler soundHandler)
        {
            soundHandler.PlaySound(ClipName.Click);
            _shopMenuRoot.SetActive(true);
        }

        private void CloseShopMenu(MenuSoundHandler soundHandler)
        {
            soundHandler.PlaySound(ClipName.Click);
            _shopMenuRoot.SetActive(false);
        }

        private void UpdateCoinBalanceText(OnCoinBalanceChangedSignal signal) => _coinBalanceText.text = _coinBalanceUI.GetCoinBalance().ToString();
    }
}
