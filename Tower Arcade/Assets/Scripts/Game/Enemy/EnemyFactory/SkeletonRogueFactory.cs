using Game;
using UnityEngine;

namespace Game
{
    public class SkeletonRogueFactory : Factory
    {
        public override Enemy Create()
        {
            var enemyPrefab = Resources.Load<SkeletonRogueController>("Enemy/SkeletonRogue");
            Enemy skeletonRogue = Object.Instantiate(enemyPrefab);
            return skeletonRogue;
        }
    }
}
