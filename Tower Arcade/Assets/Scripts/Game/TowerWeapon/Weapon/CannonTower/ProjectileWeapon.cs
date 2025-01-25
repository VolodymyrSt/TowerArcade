using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ProjectileWeapon : MonoBehaviour, IWeapon
    {
        public void Init(Transform parent)
        {
            transform.SetParent(parent, false);
        }

        public void Shoot(Enemy enemy, float attackSpeed, float damage, LevelCurencyHandler levelCurency)
        {
            if(enemy == null) return;

            transform.DOMove(enemy.transform.position, attackSpeed)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => OnReachedTarget(enemy, damage, levelCurency));
        }

        private void OnReachedTarget(Enemy enemy, float damage, LevelCurencyHandler levelCurency)
        {
            enemy.ApplyDamage(damage, levelCurency);
            Destroy(gameObject);
        }
    }
}
