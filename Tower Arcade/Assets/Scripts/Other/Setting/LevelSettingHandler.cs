using Sound;

namespace Game
{
    public class LevelSettingHandler : IUpdatable
    {
        //Dependencies
        private LevelSettingHandlerUI _levelSettingHandlerUI;
        private CameraMoveController _cameraMoveController;
        private LevelSoundHandler _levelSoundHandler;

        private SaveData _saveData;
        private SaveSystem _saveSystem;

        public LevelSettingHandler(LevelSettingHandlerUI levelSettingHandlerUI, CameraMoveController cameraMoveController, LevelSoundHandler levelSoundHandler
            , SaveSystem saveSystem, SaveData saveData)
        {
            _levelSettingHandlerUI = levelSettingHandlerUI;
            _cameraMoveController = cameraMoveController;
            _levelSoundHandler = levelSoundHandler;

            _saveData = saveData;
            _saveSystem = saveSystem;

            _cameraMoveController.InitMouseSensivity(_saveData, _saveSystem);
            _levelSoundHandler.InitVoluem(_saveData, _saveSystem);

            _levelSettingHandlerUI.InitSliders(_cameraMoveController.GetMaxSensivity(), _levelSoundHandler.GetMaxVoluem());

            _levelSettingHandlerUI.SetMouseSensivitySliderValue(_saveSystem.Load().MouseSensivity);
            _levelSettingHandlerUI.SetSoundSliderValue(_saveSystem.Load().LevelVoluem);

        }

        public void Tick()
        {
            _cameraMoveController.ChangeSensivity(_levelSettingHandlerUI.GetMouseSensivitySliderValue(), _saveSystem, _saveData);
            _levelSoundHandler.ChangeVoluem(_levelSettingHandlerUI.GetSoundSliderValue(), _saveData, _saveSystem);
        }
    }
}
