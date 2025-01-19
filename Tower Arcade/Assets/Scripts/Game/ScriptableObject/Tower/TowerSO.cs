using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Tower", menuName = "Scriptable Object/Tower")]
    public class TowerSO : ScriptableObject
    {
        public string TowerName;
        public GameObject TowerPrefab;
    }
}
