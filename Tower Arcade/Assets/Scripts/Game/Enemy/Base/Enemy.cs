using Game;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IEnemy, IEnemyDescription
{
    [SerializeField] protected EnemyConfigSO EnemyConfig;

    protected NavMeshAgent Agent;
    protected float CurrentHealth;
    protected float SoulCost;

    public virtual void Initialize()
    {
        Agent = GetComponent<NavMeshAgent>();

        Agent.speed = EnemyConfig.MoveSpeed;
        CurrentHealth = EnemyConfig.MaxHealth;
        SoulCost = EnemyConfig.SoulCost;
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

    public void SetTargetDestination(Vector3 destination) => Agent.SetDestination(destination);
    public void SetStartPosition(Transform parent) => transform.SetParent(parent, false);

    public float GetCurrentHealth() => CurrentHealth;
    public float GetSoulCost() => SoulCost;
    public string GetEnemyName() => EnemyConfig.EnemyName;
    public string GetEnemyDescription() => EnemyConfig.EnemyDescription;
    public EnemyType GetEnemyType() => EnemyConfig.EnemyType;
    public EnemyRank GetEnemyRank() => EnemyConfig.EnemyRank;

    public void DestroySelf() => Destroy(gameObject);
}
