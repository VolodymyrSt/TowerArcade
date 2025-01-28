using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public abstract class TowerState : MonoBehaviour, ITowerState
    {
        [Header("Config")]
        [SerializeField] protected TowerConfigSO Config;

        private HashSet<Enemy> _enemiesInAttackRange = new HashSet<Enemy>();

        public abstract void Enter(LevelCurencyHandler levelCurencyHandler);
        public void Exit() => Destroy(gameObject);
        public void InitializeStats(ref string name, ref float attackDamage, ref float attackSpeed, ref float attackCoolDown, ref float attackRange, ref float upgradeCost, GameObject zone)
        {
            name = Config.Name;
            attackDamage = Config.Damage;
            attackSpeed = Config.AttackSpeed;
            attackCoolDown = Config.AttackCoolDown;
            attackRange = Config.AttackRange;
            upgradeCost = Config.UpgradeCost;

            zone.transform.localScale = new Vector3(Config.AttackRange * 2, 0.05f, Config.AttackRange * 2);
        }

        public abstract void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler);
        public abstract void HandleLookAtEnemy(Enemy enemy);

        protected IEnumerator EnemyDetecte(LevelCurencyHandler levelCurencyHandler)
        {
            yield return new WaitForSecondsRealtime(Config.AttackCoolDown);

            while (true)
            {
                DetectEnemiesInRange();

                if (_enemiesInAttackRange.Count > 0)
                {
                    var closestEnemy = GetClosestEnemy();

                    StartCoroutine(PerformAttack(closestEnemy, levelCurencyHandler));

                    yield return new WaitForSecondsRealtime(Config.AttackCoolDown);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(Config.AttackCoolDown);
                }
            }
        }

        private void DetectEnemiesInRange()
        {
            Collider[] enemyArray = Physics.OverlapSphere(transform.position, Config.AttackRange);
            _enemiesInAttackRange.Clear();

            foreach (var unit in enemyArray)
            {
                if (unit.TryGetComponent(out Enemy enemy))
                {
                    _enemiesInAttackRange.Add(enemy);
                }
            }
        }

        public virtual IEnumerator PerformAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) yield return null;

            HandleLookAtEnemy(enemy);
            HandleAttack(enemy, levelCurencyHandler);
            yield return null;
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

        protected void LookAtTarget(Enemy enemy, Vector3 frameDirection, Vector3 gunDirection, GameObject framePrefab, GameObject gunPrefab)
        {
            if (enemy == null) return;

            float rotationSpeed = 180 * Time.deltaTime;

            frameDirection = (enemy.transform.position - framePrefab.transform.position).normalized;
            gunDirection = (enemy.transform.position - gunPrefab.transform.position).normalized;

            if (frameDirection != Vector3.zero && gunDirection != Vector3.zero)
            {
                PerformSmoothLookAt(new Vector3(frameDirection.x, 0f, frameDirection.z)
                    , framePrefab.transform, rotationSpeed);

                PerformSmoothLookAt(gunDirection, gunPrefab.transform, rotationSpeed);
            }
        }
    }
}
