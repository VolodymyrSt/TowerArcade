namespace Game
{
    public interface ITowerProperties
    {
        public int GetLevel();
        public string GetName();
        public float GetDamage();
        public float GetAttackSpeed();
        public float GetAttackCooldown();
        public float GetAttackRange();
        public float GetUpgradeCost();

        public void TryToUpgradeTower(LevelCurencyHandler levelCurencyHandler, EffectPerformer effectPerformer, MassegeHandlerUI massegeHandlerUI);
        public void DelateTower(EffectPerformer effectPerformer);
        public bool IsMaxLevel();
        public void TuggleZone(bool value);
    }
}