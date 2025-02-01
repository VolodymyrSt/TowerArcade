using UnityEngine;

namespace Game
{
    public class EnemyDestinationHandler : MonoBehaviour
    {
        private HealthBarHandlerUI _healthBarHandler;

        private void Start()
        {
            _healthBarHandler = LevelRegistrator.Resolve<HealthBarHandlerUI>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out IEnemyDescription enemy))
            {
                _healthBarHandler.ChangeHealth(enemy.GetCurrentHealth());

                Destroy(other.gameObject);
            }
            else return;
        }
    }
}
