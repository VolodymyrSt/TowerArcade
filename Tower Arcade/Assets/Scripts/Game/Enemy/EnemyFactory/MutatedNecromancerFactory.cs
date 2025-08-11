using UnityEngine;

namespace Game
{
    public class MutatedNecromancerFactory : EnemyFactory
    {
        public override IEnemy Create(Transform parent)
        {
            var mutatedNecromancerPrefab = Resources.Load<MutatedNecromancerController>("Enemy/MutatedNecromancer");
            IEnemy mutatedNecromancer = Object.Instantiate(mutatedNecromancerPrefab, parent);
            return mutatedNecromancer;
        }
    }
}
