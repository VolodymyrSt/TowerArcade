using DG.Tweening;
using Sound;
using UnityEngine;

namespace Game
{
    public class CannonTowerSecondState : TowerState
    {
        [Header("TowerMovableParts")]
        [SerializeField] private GameObject _framePrefab;
        [SerializeField] private GameObject _cannonPrefab;

        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private BlowProjectileWeaponFactory _blowProjectileWeaponFactory;
        private SoundHandler _soundHandler;

        private Vector3 _frameDirection;
        private Vector3 _bowDirection;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _blowProjectileWeaponFactory = LevelDI.Resolve<BlowProjectileWeaponFactory>();
            _soundHandler = LevelDI.Resolve<SoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null || IsPaused) return;

            _soundHandler.PlaySound(ClipName.CannonShoot);

            _blowProjectileWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler, _soundHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            LookAtTarget(enemy, _frameDirection, _bowDirection, _framePrefab, _cannonPrefab);

            var animationDuration = 0.1f;
            var targetPosition = _cannonPrefab.transform.position - new Vector3(0, 0, 0.1f);

            PlayAnimation(_cannonPrefab.transform, targetPosition, animationDuration, Ease.Linear);
        }
    }
}
