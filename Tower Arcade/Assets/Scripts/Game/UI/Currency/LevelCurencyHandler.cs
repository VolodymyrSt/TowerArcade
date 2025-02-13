namespace Game
{
    public class LevelCurencyHandler
    {
        private LevelConfigurationSO _levelConfiguration;
        private EventBus _eventBus;

        private float _currentCountOfCurrency;

        public LevelCurencyHandler(LevelConfigurationSO levelConfiguration, EventBus eventBus)
        {
            _levelConfiguration = levelConfiguration;
            _eventBus = eventBus;

            _currentCountOfCurrency = _levelConfiguration.StartedCurrencyCount;
        }

        public float GetStartedCurrencyCount() => _levelConfiguration.StartedCurrencyCount;

        public float GetCurrentCurrencyCount() => _currentCountOfCurrency;

        public void AddCurrencyCount(float amount)
        {
            _currentCountOfCurrency += amount;
            _eventBus.Invoke<OnCurrencyCountChangedSignal>(new OnCurrencyCountChangedSignal(amount));
        }
        
        public void SubtactCurrencyCount(float amount)
        {
            _currentCountOfCurrency -= amount;
            _eventBus.Invoke<OnCurrencyCountChangedSignal>(new OnCurrencyCountChangedSignal(-amount));
        }
    }
}
