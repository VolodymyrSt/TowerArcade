using DI;
using Sound;

namespace Game
{
    public static class LevelDI
    {
        private static DIContainer _container;

        public static void Register(DIContainer container)
        {
            _container = container;

            //enemy
            _container.RegisterFactory(c => new SkeletonMinionFactory()).AsSingle();
            _container.RegisterFactory(c => new SkeletonRogueFactory()).AsSingle();
            _container.RegisterFactory(c => new SkeletonWarriorFactory()).AsSingle();
            _container.RegisterFactory(c => new ArmoredWarriorFactory()).AsSingle();
            _container.RegisterFactory(c => new ArmoredMinionFactory()).AsSingle();
            _container.RegisterFactory(c => new RobinHoodFactory()).AsSingle();
            _container.RegisterFactory(c => new NecromancerFactory()).AsSingle();
            _container.RegisterFactory(c => new MutatedBatFactory()).AsSingle();
            _container.RegisterFactory(c => new DragonFactory()).AsSingle();
            _container.RegisterFactory(c => new MutatedNecromancerFactory()).AsSingle();

            _container.RegisterFactory(c => new EnemyFactoryHandler()).AsSingle();

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

            _container.RegisterFactory(c => new TowerFactoryHandler()).AsSingle();

            //towerWeapon
            _container.RegisterFactory(c => new ArrowWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new ProjectileWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new BlowProjectileWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new CatapultProjectileWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new IceCrystalWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new BigIceCrystalWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new FireBallWeaponFactory()).AsSingle();
            _container.RegisterFactory(c => new MegaFireBallWeaponFactory()).AsSingle();


            //card
            _container.RegisterFactory(c => new EnemyDescriptionCardHandler(c.Resolve<EnemyDescriptionCardUI>())).AsSingle();
            _container.RegisterFactory(c => new TowerDescriptionCardHandler(c.Resolve<TowerDescriptionCardUI>(), c.Resolve<LevelCurencyHandler>(), c.Resolve<EffectPerformer>(), c.Resolve<MassegeHandlerUI>(), c.Resolve<SoundHandler>())).AsSingle();

            //other
            _container.RegisterFactory(c => new LevelCurencyHandler(c.Resolve<LevelConfigurationSO>(), c.Resolve<EventBus>())).AsSingle();

            _container.RegisterFactory(c => new LevelSettingHandler(c.Resolve<LevelSettingHandlerUI>(), c.Resolve<CameraMoveController>(), c.Resolve<SoundHandler>(), c.Resolve<SaveSystem>(), c.Resolve<SaveData>())).AsSingle();

            _container.RegisterFactory(c => new TimeHandler(c.Resolve<EventBus>())).AsSingle();

            _container.RegisterFactory(c => new SoundHandler()).AsSingle();

            _container.RegisterFactory(c => new PlacementBlockColorHandler(c.Resolve<GameInventoryHandler>())).AsSingle();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}