using Sound;
using UnityEngine;

namespace Game
{
    public class MegaFireBallWeapon : Weapon
    {
        [SerializeField] private ParticleSystem _particleSystem;

        [Header("Fire Settings:")]
        [SerializeField] private int _fireCycles;
        [SerializeField] private float _fireDamage;

        public override void OnReachedTarget(Enemy enemy, float damage, LevelCurencyHandler levelCurencyHandler, SoundHandler soundHandler)
        {
            if (enemy == null)
                DestroySelf();
            else
            {
                float fireDuration = 1f;

                enemy.ApplyDamage(damage, levelCurencyHandler);

                enemy.PerformContinuousSelfDamage(_fireDamage, fireDuration, _fireCycles, levelCurencyHandler);

                EffectPerformer.PlayEffect(_particleSystem, enemy.transform.position);

                DestroySelf();
            }
        }
    }
}
