using DG.Tweening;
using Sound;
using UnityEngine;

namespace Game
{
    public class CatapultTowerFirstState : TowerState
    {
        [Header("TowerMovableParts")]
        [SerializeField] private GameObject _framePrefab;
        [SerializeField] private GameObject _catapultPrefab;

        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private CatapultProjectileWeaponFactory _catapultProjectileWeaponFactory;
        private SoundHandler _soundHandler;

        private Vector3 _frameDirection;
        private Vector3 _bowDirection;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _catapultProjectileWeaponFactory = LevelDI.Resolve<CatapultProjectileWeaponFactory>();
            _soundHandler = LevelDI.Resolve<SoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null || IsPaused) return;

            _soundHandler.PlaySound(ClipName.CatapultShoot);

            _catapultProjectileWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler, _soundHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            LookAtTarget(enemy, _frameDirection, _bowDirection, _framePrefab, _catapultPrefab);

            var animationDuration = 0.3f;
            float targetXRotation = _catapultPrefab.transform.rotation.eulerAngles.x + 35f;

            PlayRotateAngleAnimation(_catapultPrefab.transform, targetXRotation, animationDuration, Ease.Linear);
        }
    }
}
