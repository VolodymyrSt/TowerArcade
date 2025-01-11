using UnityEngine;

namespace Game
{
    public class SkeletonWarriorFactory : Factory
    {
        public override Enemy Create()
        {
            var enemyPrefab = Resources.Load<SkeletonWarriorController>("Enemy/SkeletonWarrior");
            Enemy skeletonWarrior = Object.Instantiate(enemyPrefab);
            return skeletonWarrior;
        }
    }
}
