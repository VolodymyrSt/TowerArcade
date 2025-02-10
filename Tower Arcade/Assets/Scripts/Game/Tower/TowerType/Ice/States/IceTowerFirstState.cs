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
        private LevelSoundHandler _soundHandler;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _iceCrystalWeaponFactory = LevelRegistrator.Resolve<IceCrystalWeaponFactory>();
            _soundHandler = LevelRegistrator.Resolve<LevelSoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _soundHandler.PlaySound(ClipName.IceShoot, transform.position);

            _iceCrystalWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler, _soundHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            return;
        }
    }
}
