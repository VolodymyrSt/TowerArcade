using Game;
using UnityEngine;

public interface IWeapon
{
    public void Init(Transform parent);
    public void Shoot(Enemy enemy, float attackSpeed, float damage, LevelCurencyHandler levelCurency);
}
