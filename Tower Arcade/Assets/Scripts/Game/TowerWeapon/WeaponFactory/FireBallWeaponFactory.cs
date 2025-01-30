using UnityEngine;

namespace Game
{
    public class FireBallWeaponFactory : WeaponFactory
    {
        public override IWeapon CreateBullet()
        {
            var fireBallPrefab = Resources.Load<FireBallWeapon>("Weapon/FireBall");
            IWeapon fireBall = Object.Instantiate(fireBallPrefab);
            return fireBall;
        }
    }
}
