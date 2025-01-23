using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ArrowWeapon : MonoBehaviour, IWeapon
    {
        public void Init(Transform parent)
        {
            transform.SetParent(parent, false);
        }

        public void Shoot(Enemy enemy, float attackSpeed, float damage)
        {
            if (enemy == null) return;

            transform.DOMove(enemy.transform.position, attackSpeed)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => OnReachedTarget(enemy, damage));
        }

        private void OnReachedTarget(Enemy enemy, float damage)
        {
            enemy.ApplyDamage(damage);
            Destroy(gameObject);
        }
    }
}
