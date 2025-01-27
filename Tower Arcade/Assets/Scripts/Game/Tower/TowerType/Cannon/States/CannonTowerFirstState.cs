using UnityEngine;

namespace Game
{
    public class CannonTowerFirstState : TowerState
    {
        [SerializeField] private GameObject _framePrefab;
        [SerializeField] private GameObject _bowPrefab;

        [SerializeField] private Transform _weaponPointer;

        private ProjectileWeaponFactory _projectileWeaponFactory;

        private Vector3 _frameDirection;
        private Vector3 _bowDirection;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _projectileWeaponFactory = new ProjectileWeaponFactory();
            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            _projectileWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            float rotationSpeed = 180 * Time.deltaTime;

            _frameDirection = (enemy.transform.position - _framePrefab.transform.position).normalized;
            _bowDirection = (enemy.transform.position - _bowPrefab.transform.position).normalized;

            if (_frameDirection != Vector3.zero && _bowDirection != Vector3.zero)
            {
                PerformSmoothLookAt(new Vector3(_frameDirection.x, 0f, _frameDirection.z)
                    , _framePrefab.transform, rotationSpeed);

                PerformSmoothLookAt(_bowDirection, _bowPrefab.transform, rotationSpeed);
            }
        }
    }
}
