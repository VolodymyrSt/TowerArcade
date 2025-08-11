using UnityEngine;

namespace Game
{
    public class ArmoredWarriorFactory : EnemyFactory
    {
        public override IEnemy Create(Transform parent)
        {
            var armoredWarriorPrefab = Resources.Load<ArmoredWarriorController>("Enemy/ArmoredWarrior");
            IEnemy armoredWarrior = Object.Instantiate(armoredWarriorPrefab, parent);
            return armoredWarrior;
        }
    }
}
