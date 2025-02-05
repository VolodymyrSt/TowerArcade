using DI;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MenuEntryPoint : MonoBehaviour
    {
        private DIContainer _menuContainer = new();

        [SerializeField] private SwipeHandler _swipeHandler;
        [SerializeField] private MenuSettingHandlerUI _menuSettingHandlerUI;
        [SerializeField] private CoinBalanceUI _coinBalanceUI;

        [SerializeField] private ShopHandlerUI _shopMenuHandlerUI;
        [SerializeField] private InventoryHandlerUI _inventoryHandlerUI;

        private List<IUpdatable> _updatable = new List<IUpdatable>();

        private void Awake()
        {
            //var gameEntryPoint = FindFirstObjectByType<GameEntryPoint>();

            //gameEntryPoint.GetRootContainer().RegisterInstance<InventoryHandlerUI>(_inventoryHandlerUI);

            //_menuContainer = new DIContainer(gameEntryPoint.GetRootContainer());

            _menuContainer.RegisterInstance<MenuSettingHandlerUI>(_menuSettingHandlerUI);
            _menuContainer.RegisterInstance<CoinBalanceUI>(_coinBalanceUI);

            MenuRegistrator.Register(_menuContainer);

            _coinBalanceUI.Init(_menuContainer.Resolve<EventBus>());

            _shopMenuHandlerUI.Init(_menuContainer.Resolve<CoinBalanceUI>(), _menuContainer.Resolve<EventBus>());

            _inventoryHandlerUI.Init(_menuContainer.Resolve<EventBus>());

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
