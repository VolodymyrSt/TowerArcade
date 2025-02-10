using DG.Tweening;
using Sound;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class BallistaTowerThirdState : TowerState
    {
        [Header("TowerMovableParts")]
        [SerializeField] private GameObject _framePrefab;
        [SerializeField] private GameObject _bowPrefab;

        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private ArrowWeaponFactory _arrowBulletFactory;
        private LevelSoundHandler _soundHandler;

        private Vector3 _frameDirection;
        private Vector3 _bowDirection;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _arrowBulletFactory = LevelRegistrator.Resolve<ArrowWeaponFactory>();
            _soundHandler = LevelRegistrator.Resolve<LevelSoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _soundHandler.PlaySound(ClipName.BallistaShoot, transform.position);

            _arrowBulletFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler, _soundHandler);
        }

        public override IEnumerator PerformAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            StartCoroutine(base.PerformAttack(enemy, levelCurencyHandler));

            yield return new WaitForSecondsRealtime(Config.AttackSpeed);

            StartCoroutine(base.PerformAttack(enemy, levelCurencyHandler));
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            LookAtTarget(enemy, _frameDirection, _bowDirection, _framePrefab, _bowPrefab);

            var animationDuration = 0.05f;
            var targetPosition = _bowPrefab.transform.position - new Vector3(0, 0, 0.05f);

            PlayAnimation(_bowPrefab.transform, targetPosition, animationDuration, Ease.Linear);
        }
    }
}
