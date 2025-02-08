using Sound;

namespace Game
{
    public class LevelSettingHandler : IUpdatable
    {
        private LevelSettingHandlerUI _levelSettingHandlerUI;
        private CameraMoveController _cameraMoveController;
        private SoundHandler _soundHandler;

        public LevelSettingHandler(LevelSettingHandlerUI levelSettingHandlerUI, CameraMoveController cameraMoveController, SoundHandler soundHandler)
        {
            _levelSettingHandlerUI = levelSettingHandlerUI;
            _cameraMoveController = cameraMoveController;
            _soundHandler = soundHandler;

            _levelSettingHandlerUI.InitSliders(_cameraMoveController.GetMaxSensivity(), _soundHandler.GetMaxVoluem());

            _levelSettingHandlerUI.SetMouseSensivitySliderValue(_cameraMoveController.GetCurrentSensivity());
            _levelSettingHandlerUI.SetSoundSliderValue(_soundHandler.GetVoluem());

        }

        public void Tick()
        {
            _cameraMoveController.ChangeSensivity(_levelSettingHandlerUI.GetMouseSensivitySliderValue());
            _soundHandler.ChangeVoluem(_levelSettingHandlerUI.GetSoundSliderValue());
        }
    }
}
