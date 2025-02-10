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

                case EnemyFactoryType.ArmoredWarrior:
                    return container.Resolve<ArmoredWarriorFactory>();
                
                case EnemyFactoryType.ArmoredMinion:
                    return container.Resolve<ArmoredMinionFactory>();

                case EnemyFactoryType.RobinHood:
                    return container.Resolve<RobinHoodFactory>();

                case EnemyFactoryType.Necromancer:
                    return container.Resolve<NecromancerFactory>();

                case EnemyFactoryType.MutatedBat:
                    return container.Resolve<MutatedBatFactory>();

                case EnemyFactoryType.Dragon:
                    return container.Resolve<DragonFactory>();

                case EnemyFactoryType.MutatedNecromancer:
                    return container.Resolve<MutatedNecromancerFactory>();

                default:
                    throw new Exception("The factory doesn`t exist");
            }
        }
    }

    public enum EnemyFactoryType
    {
        SkeletonMinion,
        SkeletonRogue,
        SkeletonWarrior,
        ArmoredWarrior,
        ArmoredMinion,
        RobinHood,
        Necromancer,
        MutatedBat,
        Dragon,
        MutatedNecromancer
    }
}
