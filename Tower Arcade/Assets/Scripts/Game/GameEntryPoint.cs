using UnityEngine;
using DI;

namespace Game 
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;

        private readonly DIContainer _rootContainer = new DIContainer();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Run()
        {
            _instance = new GameEntryPoint();
        }
    }
}

