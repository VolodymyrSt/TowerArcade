using Game;
using System.Collections;
using System.Threading.Tasks;
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

    private bool _isIceCursed = false;

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

    public async void ReduceSpeed(float percentage, float duration)
    {
        if (Agent == null) return;

        Agent.speed -= Agent.speed * (percentage / 100f);
        _isIceCursed = true;

        await Task.Delay((int)(duration * 1000));

        if (Agent == null) return;

        ResetSpeedToOrigin();
        _isIceCursed = false;
    }

    public void ResetSpeedToOrigin() => Agent.speed = EnemyConfig.MoveSpeed;
    public bool IsIceCursed() => _isIceCursed;
    public async void PerformContinuousSelfDamage(float fireDamage, float durationTime, int fireCycles, LevelCurencyHandler levelCurencyHandler)
    {
        await Task.Delay((int)(durationTime * 1000));

        for (int i = 0; i < fireCycles; i++)
        {
            if (this == null) break;

            ApplyDamage(fireDamage, levelCurencyHandler);

            await Task.Delay((int)(durationTime * 1000));
        }
    }

    public void DestroySelf() => Destroy(gameObject);
}
