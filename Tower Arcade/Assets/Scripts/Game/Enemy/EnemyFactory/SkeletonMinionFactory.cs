using UnityEngine;

namespace Game
{
    public class SkeletonMinionFactory : EnemyFactory
    {
        public override IEnemy Create()
        {
            var skeletonPrefab = Resources.Load<SkeletonMinionController>("Enemy/SkeletonMinion");
            IEnemy skeleton = Object.Instantiate(skeletonPrefab);
            return skeleton;
        }
    }
}
