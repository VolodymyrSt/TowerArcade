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

        [SerializeField] private SwipeHandler _swipeHandler;
        [SerializeField] private MenuSettingHandlerUI _menuSettingHandlerUI;
        [SerializeField] private CoinBalanceUI _coinBalanceUI;
        [SerializeField] private LocationHandler _locationHandler;

        [SerializeField] private ShopHandlerUI _shopMenuHandlerUI;
        [SerializeField] private InventoryHandlerUI _inventoryHandlerUI;
        [SerializeField] private MainInventoryContainer _mainInventoryContainer;

        private List<IUpdatable> _updatable = new List<IUpdatable>();

        private void Awake()
        {
            _rootContainer = FindFirstObjectByType<GameEntryPoint>().GetRootContainer();

            _rootContainer.RegisterInstance<InventoryHandlerUI>(_inventoryHandlerUI);
            _rootContainer.RegisterInstance<CoinBalanceUI>(_coinBalanceUI);
            _rootContainer.RegisterInstance<LocationHandler>(_locationHandler);

            _menuContainer = new DIContainer(_rootContainer);

            _menuContainer.RegisterInstance<MenuSettingHandlerUI>(_menuSettingHandlerUI);
            _menuContainer.RegisterInstance<MainInventoryContainer>(_mainInventoryContainer);

            MenuRegistrator.Register(_menuContainer);

            _coinBalanceUI.Init(_menuContainer.Resolve<EventBus>(), _menuContainer.Resolve<SaveSystem>(), _menuContainer.Resolve<SaveData>());

            _inventoryHandlerUI.Init(_menuContainer.Resolve<EventBus>(), _menuContainer.Resolve<MenuSoundHandler>());

            _shopMenuHandlerUI.Init(_menuContainer.Resolve<CoinBalanceUI>(), _menuContainer.Resolve<EventBus>(), _menuContainer.Resolve<MenuSoundHandler>()
                , _menuContainer.Resolve<SaveSystem>(), _menuContainer.Resolve<SaveData>(), _menuContainer.Resolve<MainInventoryContainer>());

            AddUpdatables();
        }

        private void Update()
        {
            foreach (var update in _updatable)
            {
                update.Tick();
            }
        }

        private void AddUpdatables()
        {
            _updatable.Add(_menuContainer.Resolve<MenuSettingHandler>());
        }
    }
}
