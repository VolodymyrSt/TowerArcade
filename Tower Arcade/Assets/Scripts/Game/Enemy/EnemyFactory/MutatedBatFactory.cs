using UnityEngine;

namespace Game
{
    public class MutatedBatFactory : EnemyFactory
    {
        public override IEnemy Create(Transform parent)
        {
            var mutatedBatPrefab = Resources.Load<MutatedBatController>("Enemy/MutatedBat");
            IEnemy mutatedBat = Object.Instantiate(mutatedBatPrefab, parent);
            return mutatedBat;
        }
    }
}
