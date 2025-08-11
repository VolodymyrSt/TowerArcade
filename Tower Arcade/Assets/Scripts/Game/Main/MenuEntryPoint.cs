using DI;
using NUnit.Framework;
using Sound;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MenuEntryPoint : MonoBehaviour
    {
        private DIContainer _menuContainer = new();
        private DIContainer _rootContainer;

        [Header("Setting")]
        [SerializeField] private MenuSettingHandlerUI _menuSettingHandlerUI;

        [Header("Currency")]
        [SerializeField] private CoinBalanceUI _coinBalanceUI;

        [Header("Location")]
        [SerializeField] private SwipeHandler _swipeHandler;
        [SerializeField] private LocationHandler _locationHandler;

        [Header("Shop")]
        [SerializeField] private ShopHandlerUI _shopMenuHandlerUI;

        [Header("Inventory")]
        [SerializeField] private InventoryHandlerUI _inventoryHandlerUI;
        [SerializeField] private MainInventoryContainer _mainInventoryContainer;

        [Header("Massege")]
        [SerializeField] private MassegeHandlerUI _massegeHandler;

        private List<IUpdatable> _updatable = new List<IUpdatable>();

        private void Awake()
        {
            _rootContainer = FindFirstObjectByType<GameEntryPoint>().GetRootContainer();

            _rootContainer.RegisterInstance<CoinBalanceUI>(_coinBalanceUI);
            _rootContainer.RegisterInstance<LocationHandler>(_locationHandler);

            _menuContainer = new DIContainer(_rootContainer);

            _menuContainer.RegisterInstance<MenuSettingHandlerUI>(_menuSettingHandlerUI);
            _menuContainer.RegisterInstance<MainInventoryContainer>(_mainInventoryContainer);
            _menuContainer.RegisterInstance<MassegeHandlerUI>(_massegeHandler);

            MenuDI.Register(_menuContainer);

            InitializeMain();

            AddUpdatables();
        }

        private void Update()
        {
            foreach (var update in _updatable)
                update.Tick();
        }

        private void InitializeMain()
        {
            _coinBalanceUI.Init(_menuContainer.Resolve<EventBus>(), _menuContainer.Resolve<SaveSystem>(), _menuContainer.Resolve<SaveData>());

            _inventoryHandlerUI.Init(_menuContainer.Resolve<EventBus>(), _menuContainer.Resolve<SoundHandler>()
                , _menuContainer.Resolve<SaveData>(), _menuContainer.Resolve<SaveSystem>());

            _shopMenuHandlerUI.Init(_menuContainer.Resolve<CoinBalanceUI>(), _menuContainer.Resolve<EventBus>(),
                _menuContainer.Resolve<SoundHandler>(), _menuContainer.Resolve<SaveSystem>(), _menuContainer.Resolve<SaveData>()
                , _menuContainer.Resolve<MainInventoryContainer>(), _menuContainer.Resolve<MassegeHandlerUI>());
        }

        private void AddUpdatables() => 
            _updatable.Add(_menuContainer.Resolve<MenuSettingHandler>());
    }
}
