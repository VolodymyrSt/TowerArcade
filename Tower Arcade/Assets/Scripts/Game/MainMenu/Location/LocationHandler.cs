using DI;
using Sound;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LocationHandler : MonoBehaviour
    {
        [SerializeField] private List<LocationData> _locations = new List<LocationData>();
        private List<LevelEntranceController> _levelEntranceControllers;

        private int _currentUnlockedEntrance = 0;
        private SaveData _saveData;
        private SaveSystem _saveSystem;
        private SceneLoader _sceneLoader;
        private SoundHandler _soundHandler;
        private MassegeHandlerUI _massegeHandlerUI;

        private void Awake()
        {
            _saveData = MenuDI.Resolve<SaveData>();
            _saveSystem = MenuDI.Resolve<SaveSystem>();
            _sceneLoader = MenuDI.Resolve<SceneLoader>();
            _soundHandler = MenuDI.Resolve<SoundHandler>();
            _massegeHandlerUI = MenuDI.Resolve<MassegeHandlerUI>();
        }

        private void Start()
        {
            _currentUnlockedEntrance = _saveData.CurrentUnlockedEntrance;

            InitializeEntrances(_sceneLoader, _saveData, _saveSystem);

            if (_currentUnlockedEntrance >= _levelEntranceControllers.Count)
                _currentUnlockedEntrance = _levelEntranceControllers.Count - 1;

            for (int i = 0; i <= _currentUnlockedEntrance; i++)
            {
                if (i < _levelEntranceControllers.Count)
                {
                    _levelEntranceControllers[i].UnlockLevel();
                    _levelEntranceControllers[i].ShowLockImage(false);
                }
            }
        }

        private void InitializeEntrances(SceneLoader sceneLoader, SaveData saveData, SaveSystem saveSystem)
        {
            _levelEntranceControllers = new List<LevelEntranceController>();
            foreach (var location in _locations)
            {
                foreach (var entrance in location.GetEntrances())
                {
                    entrance.Init(sceneLoader, saveData, saveSystem, _soundHandler, _massegeHandlerUI);
                    _levelEntranceControllers.Add(entrance);
                }
            }
        }

        public void UnLockNextEntrance()
        {
            int nextEntranceIndex = _currentUnlockedEntrance + 1;

            if (nextEntranceIndex < _levelEntranceControllers.Count)
            {
                _levelEntranceControllers[nextEntranceIndex].UnlockLevel();
                _currentUnlockedEntrance = nextEntranceIndex;

                _saveData.CurrentUnlockedEntrance = _currentUnlockedEntrance;
                _saveSystem.Save(_saveData);
            }
            else
            {
                return;
            }
        }

        public LevelEntranceController GetNextLockedEntrance()
        {
            var nextEntrance = _currentUnlockedEntrance + 1;

            if (nextEntrance < _levelEntranceControllers.Count)
            {
                return _levelEntranceControllers[nextEntrance];
            }
            else
            {
                return null;
            }
        }

        public int GetCurrentUnlockedEntranceIndex() => _currentUnlockedEntrance;

        public int GetMaxEntranceCount() => _levelEntranceControllers.Count;
    }
}
