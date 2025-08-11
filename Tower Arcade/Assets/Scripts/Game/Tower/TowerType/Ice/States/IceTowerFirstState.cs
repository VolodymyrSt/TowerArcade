using DG.Tweening;
using Sound;
using UnityEngine;

namespace Game
{
    public class IceTowerFirstState : TowerState
    {
        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private IceCrystalWeaponFactory _iceCrystalWeaponFactory;
        private SoundHandler _soundHandler;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _iceCrystalWeaponFactory = LevelDI.Resolve<IceCrystalWeaponFactory>();
            _soundHandler = LevelDI.Resolve<SoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null || IsPaused) return;

            _soundHandler.PlaySound(ClipName.IceShoot);

            _iceCrystalWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler, _soundHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            return;
        }
    }
}
