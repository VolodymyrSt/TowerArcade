using UnityEngine;

namespace Game
{
    public class BlowProjectileWeaponFactory : WeaponFactory
    {
        public override IWeapon CreateBullet()
        {
            var blowProjectilePrefab = Resources.Load<BlowProjectileWeapon>("Weapon/BlowProjectile");
            BlowProjectileWeapon blowProjectileWeapon = Object.Instantiate(blowProjectilePrefab);
            return blowProjectileWeapon;
        }
    }
}
