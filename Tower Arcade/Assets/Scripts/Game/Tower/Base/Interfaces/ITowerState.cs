using Game;
using UnityEngine;

public interface ITowerState
{
    public void Enter(LevelCurencyHandler levelCurencyHandler);
    public void Exit();
    public void InitializeStats(ref string name, ref float attackDamage, ref float attackSpeed
        , ref float attackCoolDown, ref float attackRange, ref float upgradeCost, GameObject zone);
}
