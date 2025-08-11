using UnityEngine;

namespace Game
{
    public class NecromancerFactory : EnemyFactory
    {
        public override IEnemy Create(Transform parent)
        {
            var necromancerPrefab = Resources.Load<NecromancerController>("Enemy/Necromancer");
            IEnemy necromancer = Object.Instantiate(necromancerPrefab, parent);
            return necromancer;
        }
    }
}
