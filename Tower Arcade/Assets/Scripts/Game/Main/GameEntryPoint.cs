using UnityEngine;
using DI;
using UnityEditor.Overlays;

namespace Game 
{
    public class GameEntryPoint : MonoBehaviour
    {
        private readonly DIContainer _rootContainer = new DIContainer();

        private SaveData _saveData;
        private SaveSystem _saveSystem;

        private void Awake()
        {
            _rootContainer.RegisterFactory(c => new SceneLoader()).AsSingle();
            _rootContainer.RegisterFactory(c => new SaveData()).AsSingle();
            _rootContainer.RegisterFactory(c => new SaveSystem()).AsSingle();

            _saveData = _rootContainer.Resolve<SaveData>();
            _saveSystem = _rootContainer.Resolve<SaveSystem>();

            _saveData.ShopItems.Clear();
            _saveData.LevelEntances.Clear();
            _saveData.InventoryItems.Clear();
            _saveData.CoinCurrency = 200;
            _saveData.MouseSensivity = 0;
            _saveData.LevelVoluem = 0;
            _saveData.MenuVoluem = 0;
            _saveData.CurrentUnlockedEntrance = 0;

            _saveSystem.Save(_saveData);

            DontDestroyOnLoad(this);

            _rootContainer.Resolve<SceneLoader>().LoadWithLoadingScene(SceneLoader.Scene.Menu);
        }

        public DIContainer GetRootContainer() => _rootContainer;
    }
}

