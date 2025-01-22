using UnityEngine;

namespace Game
{
    public class ArrowWeaponFactory : WeaponFactory
    {
        public override IWeapon CreateBullet()
        {
            var arrowPrefab = Resources.Load<ArrowWeapon>("Weapon/Arrow");
            IWeapon arrow = Object.Instantiate(arrowPrefab);
            return arrow;
        }
    }
}
