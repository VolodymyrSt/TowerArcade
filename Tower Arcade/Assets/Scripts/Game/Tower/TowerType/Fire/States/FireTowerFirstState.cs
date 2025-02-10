using Sound;
using UnityEngine;

namespace Game
{
    public class FireTowerFirstState : TowerState
    {
        [Header("PointerForWeaponDirection")]
        [SerializeField] private Transform _weaponPointer;

        private FireBallWeaponFactory _fireBallWeaponFactory;
        private LevelSoundHandler _soundHandler;

        public override void Enter(LevelCurencyHandler levelCurencyHandler)
        {
            _fireBallWeaponFactory = LevelRegistrator.Resolve<FireBallWeaponFactory>();
            _soundHandler = LevelRegistrator.Resolve<LevelSoundHandler>();

            StartCoroutine(EnemyDetecte(levelCurencyHandler));
        }

        public override void HandleAttack(Enemy enemy, LevelCurencyHandler levelCurencyHandler)
        {
            if (enemy == null) return;

            _soundHandler.PlaySound(ClipName.FireShoot, transform.position);

            _fireBallWeaponFactory.SpawnWeapon(_weaponPointer, enemy, Config.AttackSpeed, Config.Damage, levelCurencyHandler, _soundHandler);
        }

        public override void HandleLookAtEnemy(Enemy enemy)
        {
            return;
        }
    }
}
