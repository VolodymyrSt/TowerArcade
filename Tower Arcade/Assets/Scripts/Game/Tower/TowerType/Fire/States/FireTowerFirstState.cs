using UnityEngine;

namespace Game
{
    public class FireTowerFirstState : TowerState
    {
        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private FireBallWeaponFactory _fireBallWeaponFactory;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _fireBallWeaponFactory = LevelRegistrator.Resolve<FireBallWeaponFactory>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _fireBallWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            return;
        }
    }
}
