using UnityEngine;
using DI;

namespace Game 
{
    public class GameEntryPoint : MonoBehaviour
    {
        private readonly DIContainer _rootContainer = new DIContainer();

        private void Awake()
        {
            _rootContainer.RegisterFactory(c => new SceneLoader()).AsSingle();

            DontDestroyOnLoad(this);

            _rootContainer.Resolve<SceneLoader>().LoadWithLoadingScene(SceneLoader.Scene.Menu);
        }

        public DIContainer GetRootContainer() => _rootContainer;
    }
}

