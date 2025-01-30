using UnityEngine;

namespace Game
{
    public class BigIceCrystalWeaponFactory : WeaponFactory
    {
        public override IWeapon CreateBullet()
        {
            var bigIceCrystalPrefab = Resources.Load<BigIceCrystalWeapon>("Weapon/BigIceCrystal");
            IWeapon bigIceCrystal = Object.Instantiate(bigIceCrystalPrefab);
            return bigIceCrystal;
        }
    }
}
