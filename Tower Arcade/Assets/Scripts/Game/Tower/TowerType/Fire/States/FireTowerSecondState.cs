using Sound;
using UnityEngine;

namespace Game
{
    public class FireTowerSecondState : TowerState
    {
        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private FireBallWeaponFactory _fireBallWeaponFactory;
        private LevelSoundHandler _soundHandler;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _fireBallWeaponFactory = LevelDI.Resolve<FireBallWeaponFactory>();
            _soundHandler = LevelDI.Resolve<LevelSoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null || IsPaused) return;

            _soundHandler.PlaySound(ClipName.FireShoot);

            _fireBallWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler, _soundHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            return;
        }
    }
}
