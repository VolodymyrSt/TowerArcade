using UnityEngine;

namespace Game
{
    public class CannonTowerFirstState : TowerState
    {
        [SerializeField] private GameObject _framePrefab;
        [SerializeField] private GameObject _cannonPrefab;

        [SerializeField] private Transform _weaponPointer;

        private ProjectileWeaponFactory _projectileWeaponFactory;

        private Vector3 _frameDirection;
        private Vector3 _bowDirection;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _projectileWeaponFactory = LevelRegistrator.Resolve<ProjectileWeaponFactory>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _projectileWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            LookAtTarget(enemy, _frameDirection, _bowDirection, _framePrefab, _cannonPrefab);
        }
    }
}
