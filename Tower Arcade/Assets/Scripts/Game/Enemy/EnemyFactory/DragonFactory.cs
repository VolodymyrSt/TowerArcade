using UnityEngine;

namespace Game
{
    public class DragonFactory : EnemyFactory
    {
        public override IEnemy Create(Transform parent)
        {
            var dragonPrefab = Resources.Load<DragonController>("Enemy/Dragon");
            IEnemy dragon = Object.Instantiate(dragonPrefab, parent);
            return dragon;
        }
    }
}
