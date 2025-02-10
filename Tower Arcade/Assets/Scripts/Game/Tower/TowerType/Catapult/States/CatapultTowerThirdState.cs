using DG.Tweening;
using Sound;
using UnityEngine;

namespace Game
{
    public class CatapultTowerThirdState : TowerState
    {
        [Header("TowerMovableParts")]
        [SerializeField] private GameObject _framePrefab;
        [SerializeField] private GameObject _catapultPrefab;

        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private CatapultProjectileWeaponFactory _catapultProjectileWeaponFactory;
        private LevelSoundHandler _soundHandler;

        private Vector3 _frameDirection;
        private Vector3 _bowDirection;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _catapultProjectileWeaponFactory = LevelRegistrator.Resolve<CatapultProjectileWeaponFactory>();
            _soundHandler = LevelRegistrator.Resolve<LevelSoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _soundHandler.PlaySound(ClipName.CatapultShoot, transform.position);

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
