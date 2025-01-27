namespace Game
{
    public class BallistaTowerController : Tower
    {
        public override void Initialize(LevelCurencyHandler levelCurencyHandler)
        {
            LevelCurrencyHandler = levelCurencyHandler;

            StateFactory = new BallistaTowerStateFactory();
            EnterInState(StateFactory.EnterInFirstState(LevelCurrencyHandler, transform));
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
