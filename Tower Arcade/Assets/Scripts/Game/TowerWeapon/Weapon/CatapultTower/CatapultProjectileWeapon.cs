using DG.Tweening;
using Sound;
using UnityEngine;

namespace Game
{
    public class CatapultProjectileWeapon : Weapon
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public override void Shoot(Enemy enemy, float attackSpeed, float damage, LevelCurencyHandler levelCurencyHandler, SoundHandler soundHandler)
        {
            if (enemy == null)
                DestroySelf();

            float jumpPower = 2.5f;
            int jumpCount = 1;

            Invoke(nameof(DestroySelf), 1f);

            transform.DOJump(enemy.transform.position, jumpPower, jumpCount, attackSpeed)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => OnReachedTarget(enemy, damage, levelCurencyHandler, soundHandler));
        }

        public override void OnReachedTarget(Enemy enemy, float damage, LevelCurencyHandler levelCurencyHandler, SoundHandler soundHandler)
        {
            float explodeRadious = 2f;

            if (enemy == null)
                DestroySelf();
            else
            {
                FindEnemiesInRangeAndApplyDamage(enemy, explodeRadious, damage, levelCurencyHandler);

                EffectPerformer.PlayEffect(_particleSystem, enemy.transform.position);

                soundHandler.PlaySound(ClipName.CatapultExplotion, transform.root.position);

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
