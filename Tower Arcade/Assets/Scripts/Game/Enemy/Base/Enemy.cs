using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    protected NavMeshAgent Agent;
    protected float CurrentHealth;
    protected float SoulCost;

    public abstract void Initialize();

    public virtual void ApplyDamage(float damage/*, SoulsAnalitic analitic*/)
    {
        CurrentHealth -= damage;

        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
            //analitic.soul += SoulCost;
        }
    }

    public void SetTargetDestination(Vector3 destination)
    {
        Agent.SetDestination(destination);
    }
}
