using UnityEngine;

namespace Game
{
    public abstract class EnemyFactory
    {
        public abstract IEnemy Create();

        public void SpawnEnemy(UnityEngine.Transform parent, Vector3 destination)
        {
            IEnemy enemy = Create();
            enemy.Initialize();

            enemy.SetStartPosition(parent);
            enemy.SetTargetDestination(destination);
        }
    }
}
