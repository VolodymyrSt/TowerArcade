using UnityEngine;

namespace Game
{
    public class CannonTowerThirdState : TowerState
    {
        [SerializeField] private GameObject _framePrefab;
        [SerializeField] private GameObject _cannonPrefab;

        [SerializeField] private Transform _weaponPointer;

        private BlowProjectileWeaponFactory _blowProjectileWeapon;

        private Vector3 _frameDirection;
        private Vector3 _bowDirection;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _blowProjectileWeapon = LevelRegistrator.Resolve<BlowProjectileWeaponFactory>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _blowProjectileWeapon.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            LookAtTarget(enemy, _frameDirection, _bowDirection, _framePrefab, _cannonPrefab);
        }
    }
}
