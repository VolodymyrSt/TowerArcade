using Sound;

namespace Game
{
    public class ArrowWeapon : Weapon
    {
        public override void OnReachedTarget(Enemy enemy, float damage, LevelCurencyHandler levelCurencyHandler, SoundHandler soundHandler)
        {
            if (enemy == null)
                DestroySelf();
            else
            {
                enemy.ApplyDamage(damage, levelCurencyHandler);
                DestroySelf();
            }
        }
    }
}
