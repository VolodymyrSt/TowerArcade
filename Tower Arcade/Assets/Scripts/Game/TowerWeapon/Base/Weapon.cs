using DG.Tweening;
using UnityEngine;

namespace Game
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        protected EffectPerformer EffectPerformer;

        public void Init(UnityEngine.Transform parent)
        {
            transform.SetParent(parent, false);
            EffectPerformer = LevelRegistrator.Resolve<EffectPerformer>();
        }

        public virtual void Shoot(Enemy enemy, float attackSpeed, float damage, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null)
                DestroySelf();

            Invoke(nameof(DestroySelf), 1f);

            transform.DOMove(enemy.transform.position, attackSpeed)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => OnReachedTarget(enemy, damage, levelCurencyHandler));
        }

        public abstract void OnReachedTarget(Enemy enemy, float damage, LevelCurencyHandler levelCurencyHandler);

        protected void DestroySelf() => Destroy(gameObject);
    }
}
