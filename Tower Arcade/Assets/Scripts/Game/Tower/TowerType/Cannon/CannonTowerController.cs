using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class CannonTowerController : Tower
    {
        [Header("General")]
        [SerializeField] private GameObject _framePrefab;
        [SerializeField] private GameObject _cannonPrefab;

        [SerializeField] private Transform _weaponPointer;

        private ProjectileWeaponFactory _projectileWeaponFactory;

        private Vector3 _frameDirection;
        private Vector3 _bowDirection;

        public override void Initialize()
        {
            _projectileWeaponFactory = new ProjectileWeaponFactory();

            base.Initialize();
        }

        public override void HandleAttack(Enemy enemy)
        {
            if (enemy == null) return;

            _projectileWeaponFactory.SpawnWeapon(_weaponPointer, enemy, AttackSpeed, AttackDamage);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            if (enemy == null) return;

            float rotationSpeed = 180 * Time.deltaTime;

            _frameDirection = (enemy.transform.position - _framePrefab.transform.position).normalized;
            _bowDirection = (enemy.transform.position - _cannonPrefab.transform.position).normalized;

            if (_frameDirection != Vector3.zero && _bowDirection != Vector3.zero)
            {
                PerformSmoothLookAt(new Vector3(_frameDirection.x, 0f, _frameDirection.z)
                    , _framePrefab.transform, rotationSpeed);

                PerformSmoothLookAt(_bowDirection, _cannonPrefab.transform, rotationSpeed);
            }

            PlayThrowAnimation();
        }

        private void PlayThrowAnimation()
        {
            _cannonPrefab.transform.DOMoveZ(_cannonPrefab.transform.position.z - 0.1f, 0.1f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                _cannonPrefab.transform.DOMoveZ(_cannonPrefab.transform.position.z + 0.1f, 0.1f)
                    .SetEase(Ease.Linear)
                    .Play();
            })
            .Play();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 5f);
        }
    }
}
