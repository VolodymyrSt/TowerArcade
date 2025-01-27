using UnityEngine;

namespace Game
{
    public class BallistaTowerFactory : TowerFactory
    {
        public override ITower CreateTower()
        {
            var towerPrefab = Resources.Load<BallistaTowerController>("Tower/Ballista/BallistaTower");
            ITower ballistaTower = Object.Instantiate(towerPrefab);
            return ballistaTower;
        }
    }
}
