using Sound;
using UnityEngine;

namespace Game
{
    public class BlowProjectileWeapon : Weapon
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public override void OnReachedTarget(Enemy enemy, float damage, LevelCurencyHandler levelCurencyHandler, SoundHandler soundHandler)
        {
            if (enemy == null)
                DestroySelf();
            else
            {
                float explodeRadious = 1.5f;

                FindEnemiesInRangeAndApplyDamage(enemy, explodeRadious, damage, levelCurencyHandler);

                EffectPerformer.PlayEffect(_particleSystem, enemy.transform.position);

                soundHandler.PlaySound(ClipName.CannonExplotion, transform.root.position);

                DestroySelf();
            }
        }

        private void FindEnemiesInRangeAndApplyDamage(Enemy enemy, float explodeRadious, float damage, LevelCurencyHandler levelCurencyHandler)
        {
            Collider[] enemyArray = Physics.OverlapSphere(enemy.transform.position, explodeRadious);

            foreach (var collider in enemyArray)
            {
                if (collider.TryGetComponent(out Enemy target))
                {
                    target.ApplyDamage(damage, levelCurencyHandler);
                }
                else continue;
            }
        }
    }
}
