using DI;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class LevelEntryPoint : MonoBehaviour
    {
        private DIContainer _levelContainer = new();
        private DIContainer _rootContainer;

        [Header("Enemy")]
        [SerializeField] private Transform _enemyTargetDestination;
        [SerializeField] private Transform _enemyStartPosition;
        [Space(10f)]
        [SerializeField] private EnemyDescriptionCardUI _enemyCardHandlerUI;

        [Header("Tower")]
        [SerializeField] private TowerDescriptionCardUI _towerDescriptionCardHandlerUI;

        [Header("TowerPlacement")]
        [SerializeField] private TowerPlacementBlocksHolder _towerPlacementBlocksHolder;
        [SerializeField] private GameInventoryHandler _gameInventoryHandler;

        [Header("LevelConfiguration")]
        [SerializeField] private LevelSystemSO _levelSystemConfig;
        [SerializeField] private LevelConfigurationSO _levelConfiguration;

        [Header("Effects")]
        [SerializeField] private EffectPerformer _effectPerformer;

        [Header("UI")]
        [SerializeField] private HealthBarHandlerUI _healthBarHandler;

        [Header("Setting")]
        [SerializeField] private LevelSettingHandlerUI _levelSettingHandlerUI;
        
        [Header("Camera")]
        [SerializeField] private CameraMoveController _cameraMoveController;
        
        [Header("Massege")]
        [SerializeField] private MassegeHandlerUI _massegeHandler;

        private readonly List<IUpdatable> _updatable = new();
        private EventBus _eventBus;
        private CoroutineUsager _coroutineUsager;
        private SaveData _saveData;

        private void Awake()
        {
            _rootContainer = FindFirstObjectByType<GameEntryPoint>().GetRootContainer();

            _levelContainer = new DIContainer(_rootContainer);
            var gameInput = new GameInput();
            _eventBus = new EventBus();

            _coroutineUsager = _levelContainer.Resolve<CoroutineUsager>();
            _saveData = _levelContainer.Resolve<SaveData>();

            _levelContainer.RegisterInstance<EventBus>(_eventBus);
            _levelContainer.RegisterInstance<GameInput>(gameInput);
            _levelContainer.RegisterInstance<LevelSystemSO>(_levelSystemConfig);
            _levelContainer.RegisterInstance<EnemyDescriptionCardUI>(_enemyCardHandlerUI);
            _levelContainer.RegisterInstance<TowerDescriptionCardUI>(_towerDescriptionCardHandlerUI);
            _levelContainer.RegisterInstance<TowerPlacementBlocksHolder>(_towerPlacementBlocksHolder);
            _levelContainer.RegisterInstance<LevelConfigurationSO>(_levelConfiguration);
            _levelContainer.RegisterInstance<EffectPerformer>(_effectPerformer);
            _levelContainer.RegisterInstance<HealthBarHandlerUI>(_healthBarHandler);
            _levelContainer.RegisterInstance<LevelSettingHandlerUI>(_levelSettingHandlerUI);
            _levelContainer.RegisterInstance<CameraMoveController>(_cameraMoveController);
            _levelContainer.RegisterInstance<MassegeHandlerUI>(_massegeHandler);
            _levelContainer.RegisterInstance<GameInventoryHandler>(_gameInventoryHandler);

            LevelDI.Register(_levelContainer);

            _gameInventoryHandler.InitializeInventorySlots(_levelContainer, _saveData.TowerGenerals);

            AddUpdatables();
        }

        private void Start() => 
            _eventBus.SubscribeEvent<OnLevelSystemStartedSignal>(StartLevelSystem);

        private void StartLevelSystem(OnLevelSystemStartedSignal signal)
        {
            StartCoroutine(_levelSystemConfig.StartLevelSystem(_levelContainer,
                _coroutineUsager, _enemyStartPosition, _enemyTargetDestination.position));
        }

        private void Update()
        {
            foreach(var updatable in _updatable)
                updatable.Tick();
        }

        private void AddUpdatables()
        {
            _updatable.Clear();

            _updatable.Add(_levelContainer.Resolve<EnemyDescriptionCardHandler>());
            _updatable.Add(_levelContainer.Resolve<TowerDescriptionCardHandler>());
            _updatable.Add(_levelContainer.Resolve<LevelSettingHandler>());
            _updatable.Add(_levelContainer.Resolve<LevelSystemSO>());
            _updatable.Add(_levelContainer.Resolve<PlacementBlockColorHandler>());
        }

        private void OnDestroy()
        {
            _levelContainer.Dispose();

            _rootContainer.UnRegister(_rootContainer.Resolve<CoinBalanceUI>());
            _rootContainer.UnRegister(_rootContainer.Resolve<LocationHandler>());
            _rootContainer.UnRegister(_rootContainer.Resolve<LevelEntranceController>());

            _eventBus?.UnSubscribeEvent<OnLevelSystemStartedSignal>(StartLevelSystem);
        }
    }
}
