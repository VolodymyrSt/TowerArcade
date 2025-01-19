using UnityEngine;

namespace Game
{
    public abstract class EnemyFactory
    {
        public abstract IEnemy Create();

        public void SpawnEnemy(Vector3 startPosition, Vector3 destination)
        {
            IEnemy enemy = Create();
            enemy.Initialize();

            enemy.SetStartPosition(startPosition);
            enemy.SetTargetDestination(destination);
        }
    }
}
