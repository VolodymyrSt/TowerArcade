using DI;
using System.Collections.Generic;
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

        private List<IUpdatable> _updatable = new List<IUpdatable>();

        private void Awake()
        {
            _rootContainer = FindFirstObjectByType<GameEntryPoint>().GetRootContainer();

            _levelContainer = new DIContainer(_rootContainer);

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

            _gameInventoryHandler.InitializeInventorySlots(_levelContainer, _levelContainer.Resolve<SaveData>().TowerGenerals);

            InitializeUtilScripts();
        }

        private void Start()
        {
            _levelContainer.Resolve<EventBus>().SubscribeEvent<OnLevelSystemStartedSignal>(StartLevelSystem);
        }

        private void StartLevelSystem(OnLevelSystemStartedSignal signal)
        {
            StartCoroutine(_levelSystemConfig.StartLevelSystem(_levelContainer,
                FindFirstObjectByType<CoroutineUsager>(), _enemyStartPosition, _enemyTargetDestination.position));
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
        }
    }
}
