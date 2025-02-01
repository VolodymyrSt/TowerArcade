namespace Game
{
    public class LevelSettingHandler : IUpdatable
    {
        private LevelSettingHandlerUI _levelSettingHandlerUI;
        private CameraMoveController _cameraMoveController;

        public LevelSettingHandler(LevelSettingHandlerUI levelSettingHandlerUI, CameraMoveController cameraMoveController)
        {
            _levelSettingHandlerUI = levelSettingHandlerUI;
            _cameraMoveController = cameraMoveController;

            _levelSettingHandlerUI.InitSliders(_cameraMoveController.GetMaxSensivity(), 10f);

            _levelSettingHandlerUI.SetMouseSensivitySliderValue(_cameraMoveController.GetCurrentSensivity());

        }

        public void Tick()
        {
            _cameraMoveController.ChangeSensivity(_levelSettingHandlerUI.GetMouseSensivitySliderValue());
        }
    }
}
