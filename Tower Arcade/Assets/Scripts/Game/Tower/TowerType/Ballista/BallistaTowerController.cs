using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class BallistaTowerController : Tower
    {
        [Header("General")]
        [SerializeField] private GameObject _framePrefab;
        [SerializeField] private GameObject _bowPrefab;

        [SerializeField] private Transform _weaponPointer;

        private ArrowWeaponFactory _arrowBulletFactory;

        private Vector3 _frameDirection;
        private Vector3 _bowDirection;

        public override void Initialize()
        {
            _arrowBulletFactory = new ArrowWeaponFactory();

            base.Initialize();
        }

        public override void HandleAttack(Enemy enemy)
        {
            if (enemy == null) return;

            _arrowBulletFactory.SpawnWeapon(_weaponPointer, enemy, AttackSpeed, AttackDamage);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            if (enemy == null) return;

            float rotationSpeed = 180 * Time.deltaTime;

            _frameDirection = (enemy.transform.position - _framePrefab.transform.position).normalized;
            _bowDirection = (enemy.transform.position - _bowPrefab.transform.position).normalized;

            if (_frameDirection != Vector3.zero && _bowDirection != Vector3.zero)
            {
                PerformSmoothLookAt(new Vector3(_frameDirection.x, 0f, _frameDirection.z)
                    , _framePrefab.transform, rotationSpeed);

                PerformSmoothLookAt(_bowDirection, _bowPrefab.transform, rotationSpeed);
            }

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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 5f);
        }
    }
}
