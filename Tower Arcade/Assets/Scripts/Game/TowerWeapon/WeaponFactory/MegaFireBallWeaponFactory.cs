using UnityEngine;

namespace Game
{
    public class MegaFireBallWeaponFactory : WeaponFactory
    {
        public override IWeapon CreateBullet()
        {
            var megaFireBallPrefab = Resources.Load<MegaFireBallWeapon>("Weapon/MegaFireBall");
            IWeapon megaFireBall = Object.Instantiate(megaFireBallPrefab);
            return megaFireBall;
        }
    }
}

