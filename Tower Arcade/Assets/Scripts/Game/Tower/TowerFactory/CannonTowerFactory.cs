using UnityEngine;

namespace Game
{
    public class CannonTowerFactory : TowerFactory
    {
        public override ITower CreateTower()
        {
            var cannonPrefab = Resources.Load<CannonTowerController>("Towers/CannonTower");
            ITower cannonTower = Object.Instantiate(cannonPrefab);
            return cannonTower;
        }
    }
}
