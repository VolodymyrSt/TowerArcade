using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class BallistaTowerFirstState : TowerState
    {
        [SerializeField] private GameObject _framePrefab;
        [SerializeField] private GameObject _bowPrefab;

        [SerializeField] private Transform _weaponPointer;

        private ArrowWeaponFactory _arrowBulletFactory;

        private Vector3 _frameDirection;
        private Vector3 _bowDirection;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _arrowBulletFactory = LevelRegistrator.Resolve<ArrowWeaponFactory>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _arrowBulletFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            LookAtTarget(enemy, _frameDirection, _bowDirection, _framePrefab, _bowPrefab);

            PlayThrowAnimation();
        }

        private void PlayThrowAnimation()
        {
            _bowPrefab.transform.DOMoveZ(_bowPrefab.transform.position.z - 0.1f, 0.1f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                _bowPrefab.transform.DOMoveZ(_bowPrefab.transform.position.z + 0.1f, 0.1f)
                    .SetEase(Ease.Linear)
                    .Play();
            })
            .Play();
        }
    }
}
