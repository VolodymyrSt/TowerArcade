using UnityEngine;

namespace Game
{
    public class DragonFactory : EnemyFactory
    {
        public override IEnemy Create()
        {
            var dragonPrefab = Resources.Load<DragonController>("Enemy/Dragon");
            IEnemy dragon = Object.Instantiate(dragonPrefab);
            return dragon;
        }
    }
}
