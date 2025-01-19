using UnityEngine;

public interface IEnemy
{
    public void Initialize();
    public void ApplyDamage(float damage);
    public void SetTargetDestination(Vector3 destination);
    public void SetStartPosition(Vector3 position);
}
