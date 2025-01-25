using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public abstract class Tower : MonoBehaviour, ITower, ITowerProperties
    {
        [Header("Config")]
        [SerializeField] protected TowerConfigSO Config;

        private TowerPlacementBlock _towerPlacementBlock;

        private int _currentLevel = 1;
        private int _maxLevel = 3;

        protected string Name;
        protected float AttackDamage;
        protected float AttackSpeed;
        protected float AttackCoolDown;
        protected float AttackRange;
        protected float UpgradeCost;

        private HashSet<Enemy> _enemiesInAttackRange = new HashSet<Enemy>();

        public virtual void Initialize(LevelCurencyHandler levelCurency)
        {
            Name = Config.Name;
            AttackDamage = Config.Damage;
            AttackSpeed = Config.AttackSpeed;
            AttackCoolDown = Config.AttackCoolDown;
            AttackRange = Config.AttackRange;
            UpgradeCost = Config.UpgradeCost;

            StartCoroutine(EnemyDetecte(levelCurency));
        }

        public abstract void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurency);
        public abstract void HandleLookAtEnemy(Enemy enemy);

        public virtual IEnumerator EnemyDetecte(LevelCurencyHandler levelCurency)
        {
            while (true)
            {
                DetectEnemiesInRange();

                if (_enemiesInAttackRange.Count > 0)
                {
                    var closestEnemy = GetClosestEnemy();

                    HandleLookAtEnemy(closestEnemy);
                    HandleAttack(closestEnemy, levelCurency);

                    yield return new WaitForSecondsRealtime(AttackCoolDown);
                }
                else
                {
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


        public void SetPosition(Transform spawnPosition) => transform.SetParent(spawnPosition, false);
        public void SetOccupiedBlock(TowerPlacementBlock placementBlock) => _towerPlacementBlock = placementBlock;

        public int GetLevel() => _currentLevel;
        public string GetName() => Name;
        public float GetDamage() => AttackDamage;
        public float GetAttackSpeed() => AttackSpeed;
        public float GetAttackCooldown() => AttackCoolDown;
        public float GetAttackRange() => AttackRange;
        public float GetUpgradeCost() => UpgradeCost;

        public void TryToUpgradeTower()
        {
            if (_currentLevel < _maxLevel)
            {
                _currentLevel++;
                UpgradeTower();
            }
        }

        private void UpgradeTower()
        {
            AttackDamage = 1;
            AttackSpeed = 0.1f;
            AttackCoolDown = 0.1f;
            AttackRange = 7;
        }

        public void DelateTower()
        {
            _towerPlacementBlock.SetOccupied(false);
            Destroy(gameObject);
        }

        protected void PerformSmoothLookAt(Vector3 direction, Transform rotation, float rotationSpeed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rotation.rotation = Quaternion.Slerp(rotation.rotation, targetRotation, rotationSpeed);
        }
    }
}
