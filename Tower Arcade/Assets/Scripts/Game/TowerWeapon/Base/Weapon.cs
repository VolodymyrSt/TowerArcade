using DG.Tweening;
using Sound;
using UnityEngine;

namespace Game
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        protected EffectPerformer EffectPerformer;

        public void Init(Transform parent)
        {
            transform.SetParent(parent, false);
            EffectPerformer = LevelDI.Resolve<EffectPerformer>();
        }

        public virtual void Shoot(Enemy enemy, float attackSpeed, float damage, LevelCurencyHandler levelCurencyHandler, SoundHandler soundHandler)
        {
            if (enemy == null)
                DestroySelf();

            Invoke(nameof(DestroySelf), 1f);

            transform.DOMove(enemy.transform.position, attackSpeed)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => OnReachedTarget(enemy, damage, levelCurencyHandler, soundHandler));
        }

        public abstract void OnReachedTarget(Enemy enemy, float damage, LevelCurencyHandler levelCurencyHandler, SoundHandler soundHandler);

        protected void DestroySelf() => Destroy(gameObject);
    }
}
