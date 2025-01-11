using UnityEngine;

namespace Game
{
    public class SkeletonMinionFactory : Factory
    {
        public override Enemy Create()
        {
            var skeletonPrefab = Resources.Load<SkeletonMinionController>("Enemy/SkeletonMinion");
            Enemy skeleton = Object.Instantiate(skeletonPrefab);
            return skeleton;
        }
    }
}
