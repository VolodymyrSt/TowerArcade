using DG.Tweening;
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

        protected bool IsPaused = false;

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

            zone.transform.localScale = new Vector3(Config.AttackRange, 0.05f, Config.AttackRange);

            LevelDI.Resolve<EventBus>().SubscribeEvent<OnGamePausedSignal>(IsGamePaused);
        }

        public abstract void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler);
        public abstract void HandleLookAtEnemy(Enemy enemy);

        protected IEnumerator EnemyDetecte(LevelCurencyHandler levelCurencyHandler)
        {
            float shootingPraparation = 1f;

            yield return new WaitForSeconds(shootingPraparation);

            while (true)
            {
                DetectEnemiesInRange();

                if (_enemiesInAttackRange.Count > 0)
                {
                    var closestEnemy = GetClosestEnemy();

                    StartCoroutine(PerformAttack(closestEnemy, levelCurencyHandler));

                    yield return new WaitForSeconds(Config.AttackCoolDown);
                }
                else
                {
                    yield return new WaitForSeconds(shootingPraparation);
                }
            }
        }

        private void DetectEnemiesInRange()
        {
            Collider[] enemyArray = Physics.OverlapSphere(transform.position, Config.AttackRange / 2);
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
            if (enemy == null || IsPaused) yield return null;

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
            if (enemy == null || IsPaused) return;

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

        protected void PlayAnimation(Transform targetTransform, Vector3 targetPosition, float duration, Ease ease)
        {
            Vector3 originalPosition = targetTransform.position;

            targetTransform.DOMove(targetPosition, duration)
                .SetEase(ease)
                .Play()
                .OnComplete(() =>
                {
                    targetTransform.DOMove(originalPosition, duration)
                        .SetEase(ease)
                        .Play();
                });
        }

        protected void PlayRotateAngleAnimation(Transform targetTransform, float targetAngle, float duration, Ease ease)
        {
            Quaternion originalRotation = targetTransform.rotation;

            Quaternion targetRotation = Quaternion.Euler(targetAngle, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z);

            targetTransform.DORotateQuaternion(targetRotation, duration)
                .SetEase(ease)
                .Play()
                .OnComplete(() =>
                {
                    targetTransform.DORotateQuaternion(originalRotation, duration)
                        .SetEase(ease)
                        .Play();
                });
        }

        private void IsGamePaused(OnGamePausedSignal signal)
        {
            if (signal != null)
                IsPaused = signal.OnGamePaused;
        }
    }
}
