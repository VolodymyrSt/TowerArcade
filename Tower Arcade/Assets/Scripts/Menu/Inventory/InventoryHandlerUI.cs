using Sound;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class InventoryHandlerUI : MonoBehaviour
    {
        [Header("Root")]
        [SerializeField] private Transform _inventoryMenuRoot;

        [Header("Buttons")]
        [SerializeField] private Button _openInventoryButton;
        [SerializeField] private Button _goHomeButton;

        private MainInventoryContainer _mainInventoryContainer;
        private ToolBarItemContainer _toolBarItemContainer;

        private List<TowerSO> _towersGeneral = new List<TowerSO>();

        public void Init(EventBus eventBus, MenuSoundHandler soundHandler)
        {
            _inventoryMenuRoot.gameObject.SetActive(true);

            _mainInventoryContainer = _inventoryMenuRoot.GetComponentInChildren<MainInventoryContainer>();
            _toolBarItemContainer = _inventoryMenuRoot.GetComponentInChildren<ToolBarItemContainer>();

            eventBus.SubscribeEvent<OnItemBoughtSignal>(AddItem);

            _openInventoryButton.onClick.AddListener(() => OpenInventoryMenu(soundHandler));

            _goHomeButton.onClick.AddListener(() => { 
                CloseInventoryMenu(soundHandler);
                UpdateTowerGeneralList();
            });

            UpdateTowerGeneralList();

            _inventoryMenuRoot.gameObject.SetActive(false);
        }

        public void AddItem(OnItemBoughtSignal signal) => _mainInventoryContainer.AddItemToSlot(signal.Item);

        private void UpdateTowerGeneralList() => _towersGeneral = _toolBarItemContainer.GetTowersGeneral();

        public List<TowerSO> GetTowerGeneralList() => _towersGeneral;

        private void OpenInventoryMenu(MenuSoundHandler soundHandler)
        {
            soundHandler.PlaySound(ClipName.Click);
            _inventoryMenuRoot.gameObject.SetActive(true);
        }

        private void CloseInventoryMenu(MenuSoundHandler soundHandler)
        {
            soundHandler.PlaySound(ClipName.Click);
            _inventoryMenuRoot.gameObject.SetActive(false);
        }
    }
}
