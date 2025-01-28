using DG.Tweening;
using UnityEngine;

namespace Game
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public void Init(Transform parent)
        {
            transform.SetParent(parent, false);
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
