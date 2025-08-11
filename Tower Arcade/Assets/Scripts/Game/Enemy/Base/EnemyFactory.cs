using UnityEngine;

namespace Game
{
    public abstract class EnemyFactory
    {
        public abstract IEnemy Create(Transform parent);

        public  void SpawnEnemy(Transform parent, Vector3 destination)
        {
            IEnemy enemy = Create(parent);
            var warpPosition = parent.position;

            enemy.Initialize();
            enemy.WarpAgent(warpPosition);
            enemy.SetTargetDestination(destination);
        }
    }
}
