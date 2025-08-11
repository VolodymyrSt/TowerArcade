using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

namespace Game
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour, IEnemy, IEnemyDescription
    {
        [SerializeField] protected EnemyConfigSO EnemyConfig;

        protected NavMeshAgent Agent;
        protected float CurrentHealth;
        protected float SoulCost;

        private bool _isIceCursed = false;

        public virtual void Initialize()
        {
            Agent = GetComponent<NavMeshAgent>();
            Agent.speed = EnemyConfig.MoveSpeed;
            CurrentHealth = EnemyConfig.MaxHealth;
            SoulCost = EnemyConfig.SoulCost;

            ActivateAbilitySystem();
        }

        public virtual void ApplyDamage(float damage, LevelCurencyHandler levelCurencyHandler)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                levelCurencyHandler.AddCurrencyCount(SoulCost);
                DestroySelf();
            }
        }

        public virtual void ActivateAbilitySystem() { }

        public void SetTargetDestination(Vector3 destination) => 
            Agent.SetDestination(destination);

        public void WarpAgent(Vector3 warpPosition) => 
            Agent.Warp(warpPosition);

        public float GetCurrentHealth() => CurrentHealth;

        public float GetSoulCost() => SoulCost;

        public string GetEnemyName() => EnemyConfig.EnemyName;

        public string GetEnemyDescription() => EnemyConfig.EnemyDescription;

        public EnemyType GetEnemyType() => EnemyConfig.EnemyType;

        public EnemyRank GetEnemyRank() => EnemyConfig.EnemyRank;

        public void DestroySelf() => Destroy(gameObject);

        public IEnumerator ReduceSpeed(float percentage, float duration)
        {
            if (Agent == null) yield return null;

            Agent.speed -= Agent.speed * (percentage / 100f);
            _isIceCursed = true;

            yield return new WaitForSecondsRealtime(duration);

            if (Agent == null) yield return null;

            ResetSpeedToOrigin();
            _isIceCursed = false;
        }

        public void ResetSpeedToOrigin() => Agent.speed = EnemyConfig.MoveSpeed;

        public bool IsIceCursed() => _isIceCursed;

        public void PerformContinuousSelfDamage(float fireDamage, float durationTime, int fireCycles, LevelCurencyHandler levelCurencyHandler)
        {
            StartCoroutine(PerformSelfDamage(fireDamage, durationTime, fireCycles, levelCurencyHandler));
        }
        
        public IEnumerator PerformSelfDamage(float fireDamage, float durationTime, int fireCycles, LevelCurencyHandler levelCurencyHandler)
        {
            yield return new WaitForSecondsRealtime(durationTime);

            for (int i = 0; i < fireCycles; i++)
            {
                if (this == null) break;

                ApplyDamage(fireDamage, levelCurencyHandler);

                yield return new WaitForSecondsRealtime(durationTime);
            }
        }

        public void IncreaseHealth(float percent)
        {
            if (percent < 0) return;

            CurrentHealth += (int)(CurrentHealth * percent / 100f);

            if (CurrentHealth > EnemyConfig.MaxHealth)
                CurrentHealth = EnemyConfig.MaxHealth;
        }
    }
}
