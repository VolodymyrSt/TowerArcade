using UnityEngine;

namespace Game
{
    public class ArmoredWarriorFactory : EnemyFactory
    {
        public override IEnemy Create()
        {
            var armoredWarriorPrefab = Resources.Load<ArmoredWarriorController>("Enemy/ArmoredWarrior");
            IEnemy armoredWarrior = Object.Instantiate(armoredWarriorPrefab);
            return armoredWarrior;
        }
    }
}
