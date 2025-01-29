using DI;
using System;

namespace Game
{
    public class TowerFactoryHandler
    {
        public TowerFactory GetTowerFactoryByType(DIContainer container, TowerFactoryType towerFactoryType)
        {
            switch (towerFactoryType) { 
                case TowerFactoryType.BallistaTower:
                    return container.Resolve<BallistaTowerFactory>();

                case TowerFactoryType.CannonTower:
                    return container.Resolve<CannonTowerFactory>();

                case TowerFactoryType.CatapultTower:
                    return container.Resolve<CatapultTowerFactory>();
                default:
                    throw new Exception("The factory doesn`t exist");
            }

        }
    }

    public enum TowerFactoryType
    {
        BallistaTower,
        CannonTower,
        CatapultTower
    }
}
