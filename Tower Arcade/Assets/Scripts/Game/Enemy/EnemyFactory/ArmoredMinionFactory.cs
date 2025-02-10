using UnityEngine;

namespace Game
{
    public class ArmoredMinionFactory : EnemyFactory
    {
        public override IEnemy Create()
        {
            var armoredMinionPrefab = Resources.Load<ArmoredMinionController>("Enemy/ArmoredMinion");
            IEnemy armoredMinion = Object.Instantiate(armoredMinionPrefab);
            return armoredMinion;
        }
    }
}
