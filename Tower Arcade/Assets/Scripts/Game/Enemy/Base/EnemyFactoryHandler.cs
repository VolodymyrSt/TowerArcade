using DI;
using System;

namespace Game
{
    public class EnemyFactoryHandler
    {
        public EnemyFactory GetEnemyFactoryByType(DIContainer container, EnemyFactoryType type)
        {
            switch (type)
            {
                case EnemyFactoryType.SkeletonMinion:
                    return container.Resolve<SkeletonMinionFactory>();

                case EnemyFactoryType.SkeletonRogue:
                    return container.Resolve<SkeletonRogueFactory>();

                case EnemyFactoryType.SkeletonWarrior:
                    return container.Resolve<SkeletonWarriorFactory>();

                default:
                    throw new Exception("The factory doesn`t exist");
            }
        }
    }

    public enum EnemyFactoryType
    {
        SkeletonMinion,
        SkeletonRogue,
        SkeletonWarrior
    }
}
