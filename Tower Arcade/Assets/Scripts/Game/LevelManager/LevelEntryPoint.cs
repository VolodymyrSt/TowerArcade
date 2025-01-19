using DI;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LevelEntryPoint : MonoBehaviour
    {
        private DIContainer _container = new DIContainer();

        [Header("Enemy")]
        [SerializeField] private Transform _enemyTargetDestination;
        [SerializeField] private Transform _enemyStartPosition;
        [Space(10f)]
        [SerializeField] private EnemyDescriptionCardUI _enemyCardHandlerUI;

        [Header("LevelSystem")]
        [SerializeField] private LevelSystemSO _levelSystemConfig;

        [Header("UI")]
        [SerializeField] private LevelSystemActivatorUI _levelSystemActivatorUI;
        [SerializeField] private WaveInfoHandlerUI _waveInfoHandlerUI;
        [SerializeField] private WaveAnnouncementHandlerUI _waveAnnouncementHandlerUI;

        [Header("TowerPlacement")]
        [SerializeField] private TowerPlacementBlocksHolder _towerPlacementBlocksHolder;
        [SerializeField] private GameInventoryHandler _gameInventoryHandler;

        private List<IUpdatable> _updatable = new List<IUpdatable>();
        private CoroutineUsager _coroutineUsage;

        private void Awake()
        {
            LevelRegistrator.Register(_container);

            _container.RegisterInstance<LevelSystemSO>(_levelSystemConfig);
            _container.RegisterInstance<EnemyDescriptionCardUI>(_enemyCardHandlerUI);
            _container.RegisterInstance<TowerPlacementBlocksHolder>(_towerPlacementBlocksHolder);

            _levelSystemActivatorUI.Initialize(_container.Resolve<LevelSystemSO>(), _container.Resolve<EventBus>());
            _waveInfoHandlerUI.Initialize(_container.Resolve<LevelSystemSO>(), _container.Resolve<EventBus>());
            _waveAnnouncementHandlerUI.Initialize(_container.Resolve<LevelSystemSO>(), _container.Resolve<EventBus>());

            _gameInventoryHandler.Initialize(_container.Resolve<TowerPlacementBlocksHolder>());

            InitializeUtilScripts();
        }

        private void Start()
        {
            _container.Resolve<EventBus>().SubscribeEvent<OnLevelSystemStartedSignal>(StartLevelSystem);
        }

        private void StartLevelSystem(OnLevelSystemStartedSignal signal)
        {
            StartCoroutine(_levelSystemConfig.StartLevelSystem(_container,
                _coroutineUsage, _enemyStartPosition.position, _enemyTargetDestination.position));
        }

        private void Update()
        {
            foreach(var updatable in _updatable)
            {
                updatable.Tick();
            }
        }

        private void InitializeUtilScripts()
        {
            CoroutineUsager coroutinePrefab = Resources.Load<CoroutineUsager>("Utils/CoroutineUsager");
            _coroutineUsage = Instantiate(coroutinePrefab);

            _updatable.Add(_container.Resolve<EnemyDescriptionCardHandler>());
        }
    }
}
