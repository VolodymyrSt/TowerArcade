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
        public void InitializeStats(ref string name, ref float attackDamage, ref float attackSpeed, ref float attackCoolDown, ref float attackRange, ref float upgradeCost)
        {
            name = Config.Name;
            attackDamage = Config.Damage;
            attackSpeed = Config.AttackSpeed;
            attackCoolDown = Config.AttackCoolDown;
            attackRange = Config.AttackRange;
            upgradeCost = Config.UpgradeCost;
        }

        public abstract void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler);
        public abstract void HandleLookAtEnemy(Enemy enemy);

        public virtual IEnumerator EnemyDetecte(LevelCurencyHandler levelCurencyHandler)
        {
            while (true)
            {
                DetectEnemiesInRange();

                if (_enemiesInAttackRange.Count > 0)
                {
                    var closestEnemy = GetClosestEnemy();

                    HandleLookAtEnemy(closestEnemy);
                    HandleAttack(closestEnemy, levelCurencyHandler);

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
    }
}
