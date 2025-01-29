using UnityEngine;

namespace Game
{
    public class CatapultProjectileWeaponFactory : WeaponFactory
    {
        public override IWeapon CreateBullet()
        {
            var projectilePrefab = Resources.Load<CatapultProjectileWeapon>("Weapon/CatapultProjectile");
            IWeapon projectile = Object.Instantiate(projectilePrefab);
            return projectile;
        }
    }
}
