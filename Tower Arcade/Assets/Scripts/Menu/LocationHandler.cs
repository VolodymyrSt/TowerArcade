using DI;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LocationHandler : MonoBehaviour
    {
        [SerializeField] private List<LocationData> _locations = new List<LocationData>();
        private List<LevelEntranceController> _levelEntranceControllers;

        private int _currentUnlockedEntrance = 0;

        private void Start()
        {
            ItilializeEntrances(MenuRegistrator.Resolve<SceneLoader>());

            UnLockNextEntrance();
        }

        private void ItilializeEntrances(SceneLoader sceneLoader)
        {
            _levelEntranceControllers = new List<LevelEntranceController>();

            foreach (var location in _locations)
            {
                foreach (var entrance in location.GetEntrances())
                {
                    entrance.Init(sceneLoader);
                    _levelEntranceControllers.Add(entrance);
                }
            }
        }

        private void UnLockNextEntrance()
        {
            _levelEntranceControllers[_currentUnlockedEntrance].UnlockLevel();
            _currentUnlockedEntrance++;
        }

        private LevelEntranceController GetCurrentUnlockedEntrance() => _levelEntranceControllers[_currentUnlockedEntrance];
    }
}
