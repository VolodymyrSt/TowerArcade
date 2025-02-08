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
            _megaFireBallWeaponFactory = LevelRegistrator.Resolve<MegaFireBallWeaponFactory>();
            _soundHandler = LevelRegistrator.Resolve<SoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _soundHandler.PlaySound(ClipName.FireShoot, transform.position);

            _megaFireBallWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler, _soundHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            return;
        }
    }
}
