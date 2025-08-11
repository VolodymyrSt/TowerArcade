using Sound;
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

        //Dependencies
        private MainInventoryContainer _mainInventoryContainer;
        private ToolBarItemContainer _toolBarItemContainer;

        private SaveSystem _saveSystem;
        private SaveData _saveData;

        public void Init(EventBus eventBus, SoundHandler soundHandler, SaveData saveData, SaveSystem saveSystem)
        {
            ShowInventoryMenu();

            _saveSystem = saveSystem;
            _saveData = saveData;

            _mainInventoryContainer = _inventoryMenuRoot.GetComponentInChildren<MainInventoryContainer>();
            _toolBarItemContainer = _inventoryMenuRoot.GetComponentInChildren<ToolBarItemContainer>();

            eventBus.SubscribeEvent<OnItemBoughtSignal>(AddItem);

            _openInventoryButton.onClick.AddListener(() => OpenInventoryMenu(soundHandler));

            _goHomeButton.onClick.AddListener(() => { 
                GoToMenu(soundHandler);
            });

            _saveData.TowerGenerals = _toolBarItemContainer.GetTowersGeneral();

            HideInventoryMenu();
        }

        public void AddItem(OnItemBoughtSignal signal) => _mainInventoryContainer.AddItemToSlot(signal.Item);

        public void UpdateAndSaveTowerGeneralList()
        {
            _saveData.TowerGenerals = _toolBarItemContainer.GetTowersGeneral();
            _saveSystem.Save(_saveData);    
        }

        private void OpenInventoryMenu(SoundHandler soundHandler)
        {
            soundHandler.PlaySound(ClipName.Click);
            _inventoryMenuRoot.gameObject.SetActive(true);
        }

        private void GoToMenu(SoundHandler soundHandler)
        {
            soundHandler.PlaySound(ClipName.Click);
            UpdateAndSaveTowerGeneralList();
            HideInventoryMenu();
        }

        private void ShowInventoryMenu() => _inventoryMenuRoot.gameObject.SetActive(true);
        private void HideInventoryMenu() => _inventoryMenuRoot.gameObject.SetActive(false);
    }
}
