using DI;

namespace Game
{
    public static class LevelRegistrator
    {
        public static void Register(DIContainer container)
        {
            //enemy
            container.RegisterFactory(c => new SkeletonMinionFactory()).AsSingle();
            container.RegisterFactory(c => new SkeletonRogueFactory()).AsSingle();
            container.RegisterFactory(c => new SkeletonWarriorFactory()).AsSingle();

            //tower
            container.RegisterFactory(c => new BallistaTowerFactory()).AsSingle();
            container.RegisterFactory(c => new CannonTowerFactory()).AsSingle();

            //other
            container.RegisterFactory(c => new EnemyFactoryHandler()).AsSingle();
            container.RegisterFactory(c => new EnemyDescriptionCardHandler(c.Resolve<EnemyDescriptionCardUI>())).AsSingle();

            container.RegisterFactory(c => new TowerFactoryHandler()).AsSingle();

            container.RegisterFactory(c => new EventBus()).AsSingle();
        }
    }
}