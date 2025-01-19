using UnityEngine;

namespace Game
{
    public class SkeletonWarriorFactory : EnemyFactory
    {
        public override IEnemy Create()
        {
            var enemyPrefab = Resources.Load<SkeletonWarriorController>("Enemy/SkeletonWarrior");
            IEnemy skeletonWarrior = Object.Instantiate(enemyPrefab);
            return skeletonWarrior;
        }
    }
}
