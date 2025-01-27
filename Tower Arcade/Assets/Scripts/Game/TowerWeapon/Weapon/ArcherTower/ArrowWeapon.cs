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

        public void Shoot(Enemy enemy, float attackSpeed, float damage, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) Destroy(gameObject);

            transform.DOMove(enemy.transform.position, attackSpeed)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => OnReachedTarget(enemy, damage, levelCurencyHandler));
        }

        private void OnReachedTarget(Enemy enemy, float damage, LevelCurencyHandler levelCurencyHandler)
        {
            enemy.ApplyDamage(damage, levelCurencyHandler);
            Destroy(gameObject);
        }
    }
}
