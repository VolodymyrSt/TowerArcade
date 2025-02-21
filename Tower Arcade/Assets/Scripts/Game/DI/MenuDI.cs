using Game;
using Sound;

namespace DI
{
    public static class MenuDI
    {
        private static DIContainer _container;

        public static void Register(DIContainer container)
        {
            _container = container;

            _container.RegisterFactory(c => new EventBus()).AsSingle();

            _container.RegisterFactory(c => new MenuSoundHandler()).AsSingle();

            _container.RegisterFactory(c => new MenuSettingHandler(c.Resolve<MenuSettingHandlerUI>(), c.Resolve<MenuSoundHandler>(), c.Resolve<SaveSystem>(), c.Resolve<SaveData>())).AsSingle();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
