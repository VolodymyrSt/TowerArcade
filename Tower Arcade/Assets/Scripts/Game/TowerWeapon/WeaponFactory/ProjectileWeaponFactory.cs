using UnityEngine;

namespace Game
{
    public class ProjectileWeaponFactory : WeaponFactory
    {
        public override IWeapon CreateBullet()
        {
            var projectilePrefab = Resources.Load<ProjectileWeapon>("Weapon/Projectile");
            IWeapon projectile = Object.Instantiate(projectilePrefab);
            return projectile;
        }
    }
}
