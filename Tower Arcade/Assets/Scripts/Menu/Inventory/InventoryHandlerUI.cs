using System.Collections.Generic;
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

        public void Init(EventBus eventBus)
        {
            OpenInventoryMenu();

            _mainInventoryContainer = _inventoryMenuRoot.GetComponentInChildren<MainInventoryContainer>();
            _toolBarItemContainer = _inventoryMenuRoot.GetComponentInChildren<ToolBarItemContainer>();

            eventBus.SubscribeEvent<OnItemBoughtSignal>(AddItem);

            _openInventoryButton.onClick.AddListener(() => OpenInventoryMenu());

            _goHomeButton.onClick.AddListener(() => { 
                CloseInventoryMenu();
                UpdateTowerGeneralList();
            });

            UpdateTowerGeneralList();

            CloseInventoryMenu();
        }

        public void AddItem(OnItemBoughtSignal signal) => _mainInventoryContainer.AddItemToSlot(signal.Item);

        private void UpdateTowerGeneralList() => _towersGeneral = _toolBarItemContainer.GetTowersGeneral();

        public List<TowerSO> GetTowerGeneralList() => _towersGeneral;

        private void OpenInventoryMenu() => _inventoryMenuRoot.gameObject.SetActive(true);
        private void CloseInventoryMenu() => _inventoryMenuRoot.gameObject.SetActive(false);
    }
}
