using UnityEngine;

namespace Game
{
    public class CatapultTowerFactory : TowerFactory
    {
        public override ITower CreateTower()
        {
            var cannonPrefab = Resources.Load<CatapultTowerController>("Tower/Catapult/CatapultTower");
            ITower cannonTower = Object.Instantiate(cannonPrefab);
            return cannonTower;
        }
    }
}
