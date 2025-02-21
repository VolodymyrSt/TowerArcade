using Sound;
using UnityEngine;

namespace Game
{
    public class IceTowerThirdState : TowerState
    {
        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private BigIceCrystalWeaponFactory _bigIceCrystalWeaponFactory;
        private LevelSoundHandler _soundHandler;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _bigIceCrystalWeaponFactory = LevelDI.Resolve<BigIceCrystalWeaponFactory>();
            _soundHandler = LevelDI.Resolve<LevelSoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null || IsPaused) return;

            _soundHandler.PlaySound(ClipName.IceShoot);

            _bigIceCrystalWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler, _soundHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            return;
        }
    }
}
