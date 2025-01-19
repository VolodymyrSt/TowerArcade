using DI;

namespace Game
{
    public static class LevelRegistrator
    {
        public static void Register(DIContainer container)
        {
            container.RegisterFactory(c => new SkeletonMinionFactory()).AsSingle();
            container.RegisterFactory(c => new SkeletonRogueFactory()).AsSingle();
            container.RegisterFactory(c => new SkeletonWarriorFactory()).AsSingle();

            container.RegisterFactory(c => new EnemyFactoryHandler()).AsSingle();
            container.RegisterFactory(c => new EnemyDescriptionCardHandler(c.Resolve<EnemyDescriptionCardUI>())).AsSingle();

            container.RegisterFactory(c => new EventBus()).AsSingle();
        }
    }
}