using UnityEngine;

namespace Game
{
    public class BlowProjectileWeapon : Weapon
    {
        public override void OnReachedTarget(Enemy enemy, float damage, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null)
                DestroySelf();
            else
            {
                Collider[] enemyArray = Physics.OverlapSphere(enemy.transform.position, 1f);
                
                foreach (var collider in enemyArray)
                {
                    if (collider.TryGetComponent(out Enemy target))
                    {
                        target.ApplyDamage(damage, levelCurencyHandler);
                    }
                    else continue;
                }
                
                DestroySelf();
            }
        }
    }
}
