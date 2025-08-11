using Sound;
using UnityEngine;

namespace Game
{
    public class FireTowerThirdState : TowerState
    {
        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private MegaFireBallWeaponFactory _megaFireBallWeaponFactory;
        private SoundHandler _soundHandler;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _megaFireBallWeaponFactory = LevelDI.Resolve<MegaFireBallWeaponFactory>();
            _soundHandler = LevelDI.Resolve<SoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null || IsPaused) return;

            _soundHandler.PlaySound(ClipName.FireShoot);

            _megaFireBallWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler, _soundHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            return;
        }
    }
}
