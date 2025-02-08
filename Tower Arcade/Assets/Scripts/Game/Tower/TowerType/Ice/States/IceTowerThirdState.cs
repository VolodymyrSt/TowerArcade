using DG.Tweening;
using Sound;
using UnityEngine;

namespace Game
{
    public class IceTowerThirdState : TowerState
    {
        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private BigIceCrystalWeaponFactory _bigIceCrystalWeaponFactory;
        private SoundHandler _soundHandler;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _bigIceCrystalWeaponFactory = LevelRegistrator.Resolve<BigIceCrystalWeaponFactory>();
            _soundHandler = LevelRegistrator.Resolve<SoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _soundHandler.PlaySound(ClipName.IceShoot, transform.position);

            _bigIceCrystalWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler, _soundHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            return;
        }
    }
}
