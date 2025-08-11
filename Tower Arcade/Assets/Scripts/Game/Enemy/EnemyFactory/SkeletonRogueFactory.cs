using Game;
using UnityEngine;

namespace Game
{
    public class SkeletonRogueFactory : EnemyFactory
    {
        public override IEnemy Create(Transform parent)
        {
            var enemyPrefab = Resources.Load<SkeletonRogueController>("Enemy/SkeletonRogue");
            IEnemy skeletonRogue = Object.Instantiate(enemyPrefab, parent);
            return skeletonRogue;
        }
    }
}
