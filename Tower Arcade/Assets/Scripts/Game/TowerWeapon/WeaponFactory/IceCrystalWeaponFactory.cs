using UnityEngine;

namespace Game
{
    public class IceCrystalWeaponFactory : WeaponFactory
    {
        public override IWeapon CreateBullet()
        {
            var iceCrystalPrefab = Resources.Load<IceCrystalWeapon>("Weapon/IceCrystal");
            IWeapon iceCrystal = Object.Instantiate(iceCrystalPrefab);
            return iceCrystal;
        }
    }
}
