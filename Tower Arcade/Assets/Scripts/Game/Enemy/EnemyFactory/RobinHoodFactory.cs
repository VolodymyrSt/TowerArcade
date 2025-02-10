using UnityEngine;

namespace Game
{
    public class RobinHoodFactory : EnemyFactory
    {
        public override IEnemy Create()
        {
            var robinHoodPrefab = Resources.Load<RobinHoodController>("Enemy/RobinHood");
            IEnemy robinHood = Object.Instantiate(robinHoodPrefab);
            return robinHood;
        }
    }
}
