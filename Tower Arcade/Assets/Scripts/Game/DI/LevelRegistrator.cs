using DI;

namespace Game
{
    public static class LevelRegistrator
    {
        private static DIContainer _container;

        public static void Register(DIContainer container)
        {
            _container = container;

            //enemy
            _container.RegisterFactory(c => new SkeletonMinionFactory()).AsSingle();
            _container.RegisterFactory(c => new SkeletonRogueFactory()).AsSingle();
            _container.RegisterFactory(c => new SkeletonWarriorFactory()).AsSingle();

            //tower
            _container.RegisterFactory(c => new BallistaTowerFactory()).AsSingle();
            _container.RegisterFactory(c => new CannonTowerFactory()).AsSingle();

            //other
            _container.RegisterFactory(c => new EnemyFactoryHandler()).AsSingle();

            _container.RegisterFactory(c => new EnemyDescriptionCardHandler(c.Resolve<EnemyDescriptionCardUI>())).AsSingle();
            _container.RegisterFactory(c => new TowerDescriptionCardHandler(c.Resolve<TowerDescriptionCardUI>())).AsSingle();

            _container.RegisterFactory(c => new EventBus()).AsSingle();

            _container.RegisterFactory(c => new TowerFactoryHandler()).AsSingle();

            _container.RegisterFactory(c => new LevelCurencyHandler(c.Resolve<LevelConfigurationSO>(), c.Resolve<EventBus>())).AsSingle();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}