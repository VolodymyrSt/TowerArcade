using DI;
using UnityEngine;

namespace Game
{
    public class LevelEntryPoint : MonoBehaviour
    {
        private DIContainer _container;
        private FactoryHandler _factoryHandler;

        private CoroutineUsager _coroutineUsage;

        [SerializeField] private Transform _enemyTargetDestination;
        [SerializeField] private Transform _enemyStartPosition;

        [SerializeField] private WaveSpawnerSystemSO _waveSpawnerSystem;

        private void Awake()
        {
            _container = new DIContainer();
            _factoryHandler = new FactoryHandler();

            InitUtilScripts();

            RegisterFactories();

            StartCoroutine(_waveSpawnerSystem.StartWaveSpawnerSystem(_container, _factoryHandler,
                _coroutineUsage, _enemyStartPosition.position, _enemyTargetDestination.position));
        }

        private void InitUtilScripts()
        {
            CoroutineUsager coroutinePrefab = Resources.Load<CoroutineUsager>("Utils/CoroutineUsager");
            _coroutineUsage = Instantiate(coroutinePrefab);
        }

        private void RegisterFactories()
        {
            _container.RegisterFactory<SkeletonMinionFactory>(c => new SkeletonMinionFactory()).AsSingle();
            _container.RegisterFactory<SkeletonRogueFactory>(c => new SkeletonRogueFactory()).AsSingle();
            _container.RegisterFactory<SkeletonWarriorFactory>(c => new SkeletonWarriorFactory()).AsSingle();
        }
    }
}
