namespace Game
{
    public class ArrowWeapon : Weapon
    {
        public override void OnReachedTarget(Enemy enemy, float damage, LevelCurencyHandler levelCurencyHandler)
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
