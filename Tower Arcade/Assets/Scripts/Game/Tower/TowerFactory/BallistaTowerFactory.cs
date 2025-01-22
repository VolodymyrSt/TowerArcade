using UnityEngine;

namespace Game
{
    public class BallistaTowerFactory : TowerFactory
    {
        public override ITower CreateTower()
        {
            var towerPrefab = Resources.Load<BallistaTowerController>("Towers/BallistaTower");
            ITower ballistaTower = Object.Instantiate(towerPrefab);
            return ballistaTower;
        }
    }
}
