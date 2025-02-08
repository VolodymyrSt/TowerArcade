using Sound;
using UnityEngine;

namespace Game {
    public abstract class WeaponFactory
    {
        public abstract IWeapon CreateBullet();

        public void SpawnWeapon(Transform parent, Enemy enemy, float attackSpeed, float damage, LevelCurencyHandler levelCurencyHandler, SoundHandler soundHandler)
        {
            IWeapon bullet = CreateBullet();
            bullet.Init(parent);
            bullet.Shoot(enemy, attackSpeed, damage, levelCurencyHandler, soundHandler);
        }
    }
}
