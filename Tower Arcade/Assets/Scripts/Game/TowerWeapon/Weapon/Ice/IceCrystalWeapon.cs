using Sound;
using UnityEngine;

namespace Game
{
    public class IceCrystalWeapon : Weapon
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField, Range(0f, 100f)] private float _speedPercentageReduction = 70f;
        [SerializeField, Range(0f, 7f)] private float _effectDuration = 3f;

        public override void OnReachedTarget(Enemy enemy, float damage, LevelCurencyHandler levelCurencyHandler, SoundHandler soundHandler)
        {
            if (enemy == null)
                DestroySelf();
            else
            {
                enemy.ApplyDamage(damage, levelCurencyHandler);

                if (!enemy.IsIceCursed())
                   StartCoroutine(enemy.ReduceSpeed(_speedPercentageReduction, _effectDuration));

                EffectPerformer.PlayEffect(_particleSystem, enemy.transform.position);

                DestroySelf();
            }
        }
    }
}
