using UnityEngine;
using DI;

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
            _saveData.CoinCurrency = 0;
            _saveData.MouseSensivity = 10;
            _saveData.LevelVoluem = 1;
            _saveData.MenuVoluem = 1;
            _saveData.CurrentUnlockedEntrance = 0;
            _saveData.TowerGenerals = null;

            _saveSystem.Save(_saveData);

            DontDestroyOnLoad(this);

            _rootContainer.Resolve<SceneLoader>().LoadWithLoadingScene(SceneLoader.Scene.Menu);
        }

        public DIContainer GetRootContainer() => _rootContainer;
    }
}

