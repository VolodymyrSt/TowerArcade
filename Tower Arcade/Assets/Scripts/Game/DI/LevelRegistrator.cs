using DI;

namespace Game
{
    public static class LevelRegistrator
    {
        private static DIContainer _container;

        public static void Register(DIContainer container)
        {
            _container = container;

            _container.RegisterFactory(c => new EventBus()).AsSingle();

            //enemy
            _container.RegisterFactory(c => new SkeletonMinionFactory()).AsSingle();
            _container.RegisterFactory(c => new SkeletonRogueFactory()).AsSingle();
            _container.RegisterFactory(c => new SkeletonWarriorFactory()).AsSingle();

            //tower
            _container.RegisterFactory(c => new BallistaTowerFactory()).AsSingle();
            _container.RegisterFactory(c => new BallistaStateFactory()).AsSingle();

            _container.RegisterFactory(c => new CannonTowerFactory()).AsSingle();
            _container.RegisterFactory(c => new CannonStateFactory()).AsSingle();

            _container.RegisterFactory(c => new CatapultTowerFactory()).AsSingle();
            _container.RegisterFactory(c => new CatapultStateFactory()).AsSingle();

            _container.RegisterFactory(c => new IceTowerFactory()).AsSingle();
            _container.RegisterFactory(c => new IceStateFactory()).AsSingle();

            _container.RegisterFactory(c => new FireTowerFactory()).AsSingle();
            _container.RegisterFactory(c => new FireStateFactory()).AsSingle();

            //towerWeapon
            _container.RegisterFactory(c => new ArrowWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new ProjectileWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new BlowProjectileWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new CatapultProjectileWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new IceCrystalWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new BigIceCrystalWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new FireBallWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new MegaFireBallWeaponFactory()).AsSingle();


            //other
            _container.RegisterFactory(c => new EnemyFactoryHandler()).AsSingle();
            _container.RegisterFactory(c => new TowerFactoryHandler()).AsSingle();

            _container.RegisterFactory(c => new EnemyDescriptionCardHandler(c.Resolve<EnemyDescriptionCardUI>())).AsSingle();
            _container.RegisterFactory(c => new TowerDescriptionCardHandler(c.Resolve<TowerDescriptionCardUI>(), c.Resolve<LevelCurencyHandler>(), c.Resolve<EffectPerformer>(), c.Resolve<MassegeHandlerUI>())).AsSingle();

            _container.RegisterFactory(c => new LevelCurencyHandler(c.Resolve<LevelConfigurationSO>(), c.Resolve<EventBus>())).AsSingle();

            _container.RegisterFactory(c => new LevelSettingHandler(c.Resolve<LevelSettingHandlerUI>(), c.Resolve<CameraMoveController>())).AsSingle();

            _container.RegisterFactory(c => new TimeHandler()).AsSingle();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}