using Sound;

namespace Game
{
    public class ProjectileWeapon : Weapon
    {
        public override void OnReachedTarget(Enemy enemy, float damage, LevelCurencyHandler levelCurencyHandler, LevelSoundHandler soundHandler)
        {
            if (enemy == null)
                DestroySelf();
            else
            {
                enemy.ApplyDamage(damage, levelCurencyHandler);
                soundHandler.PlaySound(ClipName.CannonExplotion, transform.root.position);

                DestroySelf();
            }
        }
    }
}
