using DI;
using System;

namespace Game
{
    public class EnemyFactoryHandler
    {
        public EnemyFactory GetFactoryByType(DIContainer container, FactoryType type)
        {
            switch (type)
            {
                case FactoryType.SkeletonMinion:
                    return container.Resolve<SkeletonMinionFactory>();

                case FactoryType.SkeletonRogue:
                    return container.Resolve<SkeletonRogueFactory>();

                case FactoryType.SkeletonWarrior:
                    return container.Resolve<SkeletonWarriorFactory>();

                default:
                    throw new Exception("The factory doesn`t exist");
            }
        }
    }

    public enum FactoryType
    {
        SkeletonMinion,
        SkeletonRogue,
        SkeletonWarrior
    }
}
