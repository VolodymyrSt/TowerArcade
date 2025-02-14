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

        private void Start()
        {
            _saveData = MenuRegistrator.Resolve<SaveData>();
            _saveSystem = MenuRegistrator.Resolve<SaveSystem>();

            _currentUnlockedEntrance = _saveSystem.Load().CurrentUnlockedEntrance;

            InitializeEntrances(MenuRegistrator.Resolve<SceneLoader>(), _saveData, _saveSystem, MenuRegistrator.Resolve<MenuSoundHandler>(), MenuRegistrator.Resolve<MassegeHandlerUI>());

            if (_currentUnlockedEntrance >= _levelEntranceControllers.Count)
            {
                _currentUnlockedEntrance = _levelEntranceControllers.Count - 1;
            }

            for (int i = 0; i <= _currentUnlockedEntrance; i++)
            {
                if (i < _levelEntranceControllers.Count)
                {
                    _levelEntranceControllers[i].UnlockLevel();
                    _levelEntranceControllers[i].ShowLockImage(false);
                }
            }
        }

        private void InitializeEntrances(SceneLoader sceneLoader, SaveData saveData, SaveSystem saveSystem, MenuSoundHandler menuSoundHandler, MassegeHandlerUI massegeHandler)
        {
            _levelEntranceControllers = new List<LevelEntranceController>();
            foreach (var location in _locations)
            {
                foreach (var entrance in location.GetEntrances())
                {
                    entrance.Init(sceneLoader, saveData, saveSystem, menuSoundHandler, massegeHandler);
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
