using UnityEngine;

namespace Game
{
    public class ArmoredMinionFactory : EnemyFactory
    {
        public override IEnemy Create(Transform parent)
        {
            var armoredMinionPrefab = Resources.Load<ArmoredMinionController>("Enemy/ArmoredMinion");
            IEnemy armoredMinion = Object.Instantiate(armoredMinionPrefab, parent);
            return armoredMinion;
        }
    }
}
