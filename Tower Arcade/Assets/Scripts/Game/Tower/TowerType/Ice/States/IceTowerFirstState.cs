using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class IceTowerFirstState : TowerState
    {
        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private IceCrystalWeaponFactory _iceCrystalWeaponFactory;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _iceCrystalWeaponFactory = LevelRegistrator.Resolve<IceCrystalWeaponFactory>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _iceCrystalWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            return;
        }
    }
}
