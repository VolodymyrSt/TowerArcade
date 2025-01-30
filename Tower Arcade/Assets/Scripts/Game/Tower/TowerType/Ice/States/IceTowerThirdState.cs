using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class IceTowerThirdState : TowerState
    {
        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private BigIceCrystalWeaponFactory _bigIceCrystalWeaponFactory;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _bigIceCrystalWeaponFactory = LevelRegistrator.Resolve<BigIceCrystalWeaponFactory>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _bigIceCrystalWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            return;
        }
    }
}
