using Sound;
using UnityEditor.Overlays;
using UnityEngine;

namespace Game
{
    public class MenuSettingHandler : IUpdatable
    {
        private MenuSettingHandlerUI _menuSettingHandlerUI;
        private MenuSoundHandler _menuSoundHandler;

        private SaveData _saveData;
        private SaveSystem _saveSystem;

        public MenuSettingHandler(MenuSettingHandlerUI menuSettingHandlerUI, MenuSoundHandler menuSoundHandler, SaveSystem saveSystem, SaveData saveData)
        {
            _menuSettingHandlerUI = menuSettingHandlerUI;

            _menuSoundHandler = menuSoundHandler;

            _saveData = saveData;
            _saveSystem = saveSystem;

            _menuSoundHandler.InitVoluem(_saveData, _saveSystem);

            _menuSettingHandlerUI.InitSliders(_menuSoundHandler.GetMaxVoluem());

            _menuSettingHandlerUI.SetSoundSliderValue(_saveSystem.Load().MenuVoluem);
        }

        public void Tick()
        {
            _menuSoundHandler.ChangeVoluem(_menuSettingHandlerUI.GetSoundSliderValue(), _saveData, _saveSystem);
        }
    }
}
