using Game;

namespace DI
{
    public static class MenuRegistrator
    {
        private static DIContainer _container;

        public static void Register(DIContainer container)
        {
            _container = container;

            //_container.RegisterFactory(c => new SceneLoader()).AsSingle();

            _container.RegisterFactory(c => new EventBus()).AsSingle();

            _container.RegisterFactory(c => new MenuSettingHandler(c.Resolve<MenuSettingHandlerUI>())).AsSingle();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
