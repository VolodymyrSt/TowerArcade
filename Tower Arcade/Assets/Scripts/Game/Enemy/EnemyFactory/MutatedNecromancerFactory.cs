using UnityEngine;

namespace Game
{
    public class MutatedNecromancerFactory : EnemyFactory
    {
        public override IEnemy Create()
        {
            var mutatedNecromancerPrefab = Resources.Load<MutatedNecromancerController>("Enemy/MutatedNecromancer");
            IEnemy mutatedNecromancer = Object.Instantiate(mutatedNecromancerPrefab);
            return mutatedNecromancer;
        }
    }
}
