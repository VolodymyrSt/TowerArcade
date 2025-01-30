using UnityEngine;

namespace Game
{
    public class FireTowerFactory : TowerFactory
    {
        public override ITower CreateTower()
        {
            var fireTowerPrefab = Resources.Load<FireTowerController>("Tower/Fire/FireTower");
            ITower fireTower = Object.Instantiate(fireTowerPrefab);
            return fireTower;
        }
    }
}
