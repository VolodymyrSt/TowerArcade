using Sound;

namespace Game
{
    public class LevelSettingHandler : IUpdatable
    {
        private readonly LevelSettingHandlerUI _levelSettingHandlerUI;
        private readonly CameraMoveController _cameraMoveController;
        private readonly SoundHandler _levelSoundHandler;

        private readonly SaveData _saveData;
        private readonly SaveSystem _saveSystem;

        public LevelSettingHandler(LevelSettingHandlerUI levelSettingHandlerUI, CameraMoveController cameraMoveController, SoundHandler levelSoundHandler
            , SaveSystem saveSystem, SaveData saveData)
        {
            _levelSettingHandlerUI = levelSettingHandlerUI;
            _cameraMoveController = cameraMoveController;
            _levelSoundHandler = levelSoundHandler;

            _saveData = saveData;
            _saveSystem = saveSystem;

            _cameraMoveController.InitMouseSensivity(_saveData);
            _levelSoundHandler.InitVoluem(_saveData);

            _levelSettingHandlerUI.InitSliders(_cameraMoveController.GetMaxSensivity(), _levelSoundHandler.GetMaxVoluem());

            _levelSettingHandlerUI.SetMouseSensivitySliderValue(_saveSystem.Load().MouseSensivity);
            _levelSettingHandlerUI.SetSoundSliderValue(_saveSystem.Load().LevelVoluem);

        }

        public void Tick()
        {
            _cameraMoveController.ChangeSensivity(_levelSettingHandlerUI.GetMouseSensivitySliderValue(), _saveData);
            _levelSoundHandler.ChangeVoluem(_levelSettingHandlerUI.GetSoundSliderValue(), _saveData);
        }
    }
}
