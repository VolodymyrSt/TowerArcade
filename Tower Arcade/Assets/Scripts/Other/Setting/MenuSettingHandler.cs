using UnityEngine;

namespace Game
{
    public class MenuSettingHandler : IUpdatable
    {
        private MenuSettingHandlerUI _menuSettingHandlerUI;

        public MenuSettingHandler(MenuSettingHandlerUI menuSettingHandlerUI)
        {
            _menuSettingHandlerUI = menuSettingHandlerUI;

        }

        public void Tick()
        {
            
        }
    }
}
