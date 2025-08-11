using DG.Tweening;
using Sound;
using UnityEngine;

namespace Game
{
    public class BallistaTowerFirstState : TowerState
    {
        [Header("TowerMovableParts")]
        [SerializeField] private GameObject _framePrefab;
        [SerializeField] private GameObject _bowPrefab;

        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private ArrowWeaponFactory _arrowBulletFactory;
        private SoundHandler _soundHandler;

        private Vector3 _frameDirection;
        private Vector3 _bowDirection;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _arrowBulletFactory = LevelDI.Resolve<ArrowWeaponFactory>();
            _soundHandler = LevelDI.Resolve<SoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null || IsPaused) return;

            _soundHandler.PlaySound(ClipName.BallistaShoot);

            _arrowBulletFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler, _soundHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            LookAtTarget(enemy, _frameDirection, _bowDirection, _framePrefab, _bowPrefab);

            var animationDuration = 0.1f;
            var targetPosition = _bowPrefab.transform.position - new Vector3(0, 0, 0.1f);

            PlayAnimation(_bowPrefab.transform, targetPosition, animationDuration, Ease.Linear);
        }
    }
}
