
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LocationData : MonoBehaviour
    {
        [SerializeField] private string _locationName;

        [SerializeField] private List<LevelEntranceController> _levelEntranceControllers = new List<LevelEntranceController>();

        public List<LevelEntranceController> GetEntrances() => _levelEntranceControllers;
        public string GetLocationName() => _locationName;
    }
}
