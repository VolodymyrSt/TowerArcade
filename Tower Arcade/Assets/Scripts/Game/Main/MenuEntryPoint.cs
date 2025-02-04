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

        private List<IUpdatable> _updatable = new List<IUpdatable>();

        private void Awake()
        {
            //var gameEntryPoint = FindFirstObjectByType<GameEntryPoint>();

            //_menuContainer = new DIContainer(gameEntryPoint.GetRootContainer());
            _menuContainer.RegisterInstance<MenuSettingHandlerUI>(_menuSettingHandlerUI);
            _menuContainer.RegisterInstance<CoinBalanceUI>(_coinBalanceUI);

            _menuContainer.RegisterFactory(c => new SceneLoader()).AsSingle();

            MenuRegistrator.Register(_menuContainer);

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
