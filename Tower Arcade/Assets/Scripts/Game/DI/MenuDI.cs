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
            _container.RegisterFactory(c => new SoundHandler()).AsSingle();
            _container.RegisterFactory(c => new MenuSettingHandler(c.Resolve<MenuSettingHandlerUI>(), c.Resolve<SoundHandler>(), c.Resolve<SaveData>())).AsSingle();
        }

        public static T Resolve<T>() => 
            _container.Resolve<T>();
    }
}
