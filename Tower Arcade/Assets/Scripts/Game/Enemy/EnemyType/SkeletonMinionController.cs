using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class SkeletonMinionController : Enemy
    {
        [SerializeField] private EnemyConfigSO _config;

        public override void Initialize()
        {
            Agent = GetComponent<NavMeshAgent>();

            Agent.speed = _config.MoveSpeed;
            CurrentHealth = _config.MaxHealth;
            SoulCost = _config.SoulCost;
        }
    }
}
