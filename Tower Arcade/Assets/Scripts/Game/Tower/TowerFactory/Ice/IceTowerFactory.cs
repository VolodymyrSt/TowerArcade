using UnityEngine;

namespace Game
{
    public class IceTowerFactory : TowerFactory
    {
        public override ITower CreateTower()
        {
            var iceTowerPrefab = Resources.Load<IceTowerController>("Tower/Ice/IceTower");
            ITower iceTower = Object.Instantiate(iceTowerPrefab);
            return iceTower;
        }
    }
}
