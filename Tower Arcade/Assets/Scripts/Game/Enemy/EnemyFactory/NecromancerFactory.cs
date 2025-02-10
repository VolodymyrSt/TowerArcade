using UnityEngine;

namespace Game
{
    public class NecromancerFactory : EnemyFactory
    {
        public override IEnemy Create()
        {
            var necromancerPrefab = Resources.Load<NecromancerController>("Enemy/Necromancer");
            IEnemy necromancer = Object.Instantiate(necromancerPrefab);
            return necromancer;
        }
    }
}
