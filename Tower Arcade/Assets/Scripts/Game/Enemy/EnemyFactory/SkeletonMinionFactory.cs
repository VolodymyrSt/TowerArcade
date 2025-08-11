using UnityEngine;

namespace Game
{
    public class SkeletonMinionFactory : EnemyFactory
    {
        public override IEnemy Create(Transform parent)
        {
            var skeletonPrefab = Resources.Load<SkeletonMinionController>("Enemy/SkeletonMinion");
            IEnemy skeleton = Object.Instantiate(skeletonPrefab, parent);
            return skeleton;
        }
    }
}
