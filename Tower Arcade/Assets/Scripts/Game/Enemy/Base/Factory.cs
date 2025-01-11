using UnityEngine;

namespace Game
{
    public abstract class Factory
    {
        public abstract Enemy Create();

        public void SpawnEnemy(Vector3 startPosition, Vector3 destination)
        {
            Enemy enemy = Create();
            enemy.Initialize();

            enemy.transform.position = startPosition;
            enemy.SetTargetDestination(destination);
        }
    }
}
