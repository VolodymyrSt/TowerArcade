using UnityEngine;

namespace Game
{
    public class FireTowerThirdState : TowerState
    {
        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private MegaFireBallWeaponFactory _megaFireBallWeaponFactory;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _megaFireBallWeaponFactory = LevelRegistrator.Resolve<MegaFireBallWeaponFactory>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _megaFireBallWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            return;
        }
    }
}
