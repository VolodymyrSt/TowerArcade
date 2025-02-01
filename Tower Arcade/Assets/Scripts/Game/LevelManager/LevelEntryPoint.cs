using DI;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LevelEntryPoint : MonoBehaviour
    {
        private DIContainer _container = new DIContainer();

        [Header("Enemy")]
        [SerializeField] private UnityEngine.Transform _enemyTargetDestination;
        [SerializeField] private UnityEngine.Transform _enemyStartPosition;
        [Space(10f)]
        [SerializeField] private EnemyDescriptionCardUI _enemyCardHandlerUI;

        [Header("Tower")]
        [SerializeField] private TowerDescriptionCardUI _towerDescriptionCardHandlerUI;
        [SerializeField] private List<TowerSO> towerSOs = new List<TowerSO>();

        [Header("TowerPlacement")]
        [SerializeField] private TowerPlacementBlocksHolder _towerPlacementBlocksHolder;
        [SerializeField] private GameInventoryHandler _gameInventoryHandler;

        [Header("LevelSystem")]
        [SerializeField] private LevelSystemSO _levelSystemConfig;

        [Header("LevelConfiguration")]
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
        private CoroutineUsager _coroutineUsage;

        private void Awake()
        {
            _container.RegisterInstance<LevelSystemSO>(_levelSystemConfig);
            _container.RegisterInstance<EnemyDescriptionCardUI>(_enemyCardHandlerUI);
            _container.RegisterInstance<TowerDescriptionCardUI>(_towerDescriptionCardHandlerUI);
            _container.RegisterInstance<TowerPlacementBlocksHolder>(_towerPlacementBlocksHolder);
            _container.RegisterInstance<LevelConfigurationSO>(_levelConfiguration);
            _container.RegisterInstance<EffectPerformer>(_effectPerformer);
            _container.RegisterInstance<HealthBarHandlerUI>(_healthBarHandler);
            _container.RegisterInstance<LevelSettingHandlerUI>(_levelSettingHandlerUI);
            _container.RegisterInstance<CameraMoveController>(_cameraMoveController);
            _container.RegisterInstance<MassegeHandlerUI>(_massegeHandler);

            LevelRegistrator.Register(_container);

            _gameInventoryHandler.InitializeInventorySlots(_container, towerSOs);

            InitializeUtilScripts();
        }

        private void Start()
        {
            _container.Resolve<EventBus>().SubscribeEvent<OnLevelSystemStartedSignal>(StartLevelSystem);
        }

        private void StartLevelSystem(OnLevelSystemStartedSignal signal)
        {
            StartCoroutine(_levelSystemConfig.StartLevelSystem(_container,
                _coroutineUsage, _enemyStartPosition, _enemyTargetDestination.position));
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
            _updatable.Add(_container.Resolve<TowerDescriptionCardHandler>());
            _updatable.Add(_container.Resolve<LevelSettingHandler>());
            _updatable.Add(_container.Resolve<LevelSystemSO>());
        }
    }
}
