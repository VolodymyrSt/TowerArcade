using UnityEngine;

namespace Game
{
    public class RobinHoodFactory : EnemyFactory
    {
        public override IEnemy Create(Transform parent)
        {
            var robinHoodPrefab = Resources.Load<RobinHoodController>("Enemy/RobinHood");
            IEnemy robinHood = Object.Instantiate(robinHoodPrefab, parent);
            return robinHood;
        }
    }
}
