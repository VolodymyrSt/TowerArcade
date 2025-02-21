namespace Game
{
    public class BallistaTowerController : Tower
    {
        public override void Initialize(LevelCurencyHandler levelCurencyHandler, TowerDescriptionCardHandler towerDescriptionCardHandler)
        {
            LevelCurrencyHandler = levelCurencyHandler;

            StateFactory = LevelDI.Resolve<BallistaStateFactory>();
            EnterInState(StateFactory.EnterInFirstState(LevelCurrencyHandler, transform));

            towerDescriptionCardHandler.UpdateActiveTower(this); //
        }

        public override void UpgradeTower()
        {
            switch (CurrentLevel)
            {
                case 2:
                    EnterInState(StateFactory.EnterInSecondState(LevelCurrencyHandler, transform));
                    break;
                case 3:
                    EnterInState(StateFactory.EnterInThirdtState(LevelCurrencyHandler, transform));
                    break;
                default:
                    return;
            }
        }
    }
}
