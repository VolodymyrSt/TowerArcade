using UnityEngine;
using DI;

namespace Game 
{
    public class GameEntryPoint : CoroutineUsager
    {
        private readonly DIContainer _rootContainer = new();
        private SaveSystem _saveSystem;
        private SaveData _saveData;

        private void Awake()
        {
            _saveSystem = new SaveSystem();
            var sceneLoader = new SceneLoader(this);

            _saveData = _saveSystem.Load();

            _saveData ??= new SaveData();

            _rootContainer.RegisterInstance(_saveSystem);
            _rootContainer.RegisterInstance(_saveData);
            _rootContainer.RegisterInstance(sceneLoader);
            _rootContainer.RegisterInstance<CoroutineUsager>(this);

            Application.quitting += Application_quitting;

            DontDestroyOnLoad(this);

            sceneLoader.LoadWithLoadingScene(SceneLoader.Scene.Menu);
        }

        private void Application_quitting() => 
            _saveSystem.Save(_saveData);

        public DIContainer GetRootContainer() => _rootContainer;
    }
}

