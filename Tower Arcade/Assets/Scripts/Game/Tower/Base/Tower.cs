using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public abstract class Tower : MonoBehaviour, ITower
    {
        [SerializeField] protected TowerConfigSO Config;

        protected float AttackDamage;
        protected float AttackSpeed;
        protected float AttackCoolDown;
        protected float AttackRange;

        protected bool PlayAnimation;

        private HashSet<Enemy> _enemiesInAttackRange = new HashSet<Enemy>();

        public virtual void Initialize()
        {
            AttackDamage = Config.Damage;
            AttackSpeed = Config.AttackSpeed;
            AttackCoolDown = Config.AttackCoolDown;
            AttackRange = Config.AttackRange;

            StartCoroutine(EnemyDetecte());
        }

        public abstract void HandleAttack(Enemy enemy);
        public abstract void HandleLookAtEnemy(Enemy enemy);

        public virtual IEnumerator EnemyDetecte()
        {
            while (true)
            {
                DetectEnemiesInRange();

                if (_enemiesInAttackRange.Count > 0)
                {
                    PlayAnimation = true;
                    var closestEnemy = GetClosestEnemy();

                    HandleLookAtEnemy(closestEnemy);
                    HandleAttack(closestEnemy);

                    yield return new WaitForSecondsRealtime(AttackCoolDown);
                }
                else
                {
                    PlayAnimation = false;
                    yield return new WaitForSecondsRealtime(AttackCoolDown);
                }
            }
        }

        private void DetectEnemiesInRange()
        {
            Collider[] enemyArray = Physics.OverlapSphere(transform.position, AttackRange);
            _enemiesInAttackRange.Clear();

            foreach (var unit in enemyArray)
            {
                if (unit.TryGetComponent(out Enemy enemy))
                {
                    _enemiesInAttackRange.Add(enemy);
                }
            }
        }

        private Enemy GetClosestEnemy()
        {
            return _enemiesInAttackRange.OrderBy(enemy =>
                    Vector3.Distance(transform.position, enemy.transform.position)).First();
        }

        protected void PerformSmoothLookAt(Vector3 direction, Transform rotation, float rotationSpeed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rotation.rotation = Quaternion.Slerp(rotation.rotation, targetRotation, rotationSpeed);
        }

        public void SetPosition(Transform spawnPosition) => transform.SetParent(spawnPosition, false);
    }
}
