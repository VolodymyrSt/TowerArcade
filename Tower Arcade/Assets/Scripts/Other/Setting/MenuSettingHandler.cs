using Sound;

namespace Game
{
    public class MenuSettingHandler : IUpdatable
    {
        private readonly MenuSettingHandlerUI _menuSettingHandlerUI;
        private readonly SoundHandler _menuSoundHandler;

        private readonly SaveData _saveData;

        public MenuSettingHandler(MenuSettingHandlerUI menuSettingHandlerUI, SoundHandler menuSoundHandler, SaveData saveData)
        {
            _menuSettingHandlerUI = menuSettingHandlerUI;
            _menuSoundHandler = menuSoundHandler;

            _saveData = saveData;

            _menuSoundHandler.InitVoluem(_saveData);
            _menuSettingHandlerUI.InitSliders(_menuSoundHandler.GetMaxVoluem());
            _menuSettingHandlerUI.SetSoundSliderValue(_saveData.MenuVoluem);
        }

        public void Tick() => 
            _menuSoundHandler.ChangeVoluem(_menuSettingHandlerUI.GetSoundSliderValue()
                , _saveData);
    }
}
